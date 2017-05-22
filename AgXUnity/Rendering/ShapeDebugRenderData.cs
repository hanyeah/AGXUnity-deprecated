﻿using System;
using UnityEngine;
using AgXUnity.Collide;
using AgXUnity.Utils;

namespace AgXUnity.Rendering
{
  /// <summary>
  /// Debug rendering component which is added to all game objects
  /// containing Collide.Shape components. DebugRenderManager manages
  /// these objects.
  /// </summary>
  [AddComponentMenu( "" )]
  public class ShapeDebugRenderData : DebugRenderData
  {
    /// <summary>
    /// Find the debug rendering mesh filters for a given shape (if debug rendered).
    /// </summary>
    public static MeshFilter[] GetMeshFilters( Shape shape )
    {
      ShapeDebugRenderData debugRenderData = null;
      if ( shape == null || ( debugRenderData = shape.GetComponent<ShapeDebugRenderData>() ) == null )
        return new MeshFilter[] { };

      return debugRenderData.MeshFilters;
    }

    /// <summary>
    /// Sets scale to capsule debug rendering prefab, assuming three children:
    ///   1: (half) sphere upper
    ///   2: cylinder
    ///   3: (half) sphere lower
    /// </summary>
    /// <param name="node">Capsule prefab node with three children.</param>
    /// <param name="radius">Radius of the capsule.</param>
    /// <param name="height">Height of the capsule.</param>
    /// <param name="unscaleParentLossyScale">
    /// True to use parent lossy scale to unscale size and position for persistent size
    /// of this capsule.
    /// </param>
    public static void SetCapsuleSize( GameObject node, float radius, float height, bool unscaleParentLossyScale = true )
    {
      if ( node == null )
        return;

      if ( node.transform.childCount != 3 )
        throw new Exception( "Capsule debug rendering node doesn't contain three children." );

      var additionalScale = Vector3.one;
      if ( unscaleParentLossyScale && node.transform.parent != null ) {
        var ls = node.transform.parent.lossyScale;
        additionalScale = new Vector3( 1.0f / ls.x, 1.0f / ls.y, 1.0f / ls.z );
      }

      Transform sphereUpper = node.transform.GetChild( 0 );
      Transform cylinder = node.transform.GetChild( 1 );
      Transform sphereLower = node.transform.GetChild( 2 );

      cylinder.localScale = Vector3.Scale( new Vector3( 2.0f * radius, height, 2.0f * radius ), additionalScale );

      sphereUpper.localScale    = Vector3.Scale( 2.0f * radius * Vector3.one, additionalScale );
      sphereUpper.localPosition = Vector3.Scale( 0.5f * height * Vector3.up, additionalScale );

      sphereLower.localScale    = Vector3.Scale( 2.0f * radius * Vector3.one, additionalScale );
      sphereLower.localPosition = Vector3.Scale( 0.5f * height * Vector3.down, additionalScale );
    }

    /// <summary>
    /// Type name is shape type - prefabs in Resources folder has been
    /// named to fit these names.
    /// </summary>
    /// <returns></returns>
    public override string GetTypeName()
    {
      return GetShape().GetType().Name;
    }

    /// <returns>The Collide.Shape component.</returns>
    public Shape GetShape() { return GetComponent<Shape>(); }

    /// <summary>
    /// Lossy scale (of the shape) stored to know when to rescale the
    /// debug rendered mesh of Collide.Mesh objects.
    /// </summary>
    [SerializeField]
    private Vector3 m_storedLossyScale = Vector3.one;

    /// <summary>
    /// Creates debug rendering node if it doesn't already exist and
    /// synchronizes the rendered object transform to be the same as the shape.
    /// </summary>
    /// <param name="manager"></param>
    public override void Synchronize( DebugRenderManager manager )
    {
      try {
        Shape shape      = GetShape();
        bool nodeCreated = TryInitialize( shape );

        if ( Node == null )
          return;

        // Node created - set properties and extra components.
        if ( nodeCreated ) {
          Node.hideFlags           = HideFlags.DontSave;
          Node.transform.hideFlags = HideFlags.DontSave | HideFlags.HideInInspector;

          Node.GetOrCreateComponent<OnSelectionProxy>().Component = shape;
          foreach ( Transform child in Node.transform )
            child.gameObject.GetOrCreateComponent<OnSelectionProxy>().Component = shape;
        }

        // Forcing the debug render node to be parent to the static DebugRenderManger.
        if ( Node.transform.parent != manager.gameObject.transform )
          manager.gameObject.AddChild( Node );

        Node.transform.position = shape.transform.position;
        Node.transform.rotation = shape.transform.rotation;

        SynchronizeScale( shape );
      }
      catch ( System.Exception ) {
      }
    }

    /// <summary>
    /// Synchronize the scale/size of the debug render object to match the shape size.
    /// Scaling is ignore if the node hasn't been created (i.e., this method doesn't
    /// create the render node).
    /// </summary>
    /// <param name="shape">Shape this component belongs to.</param>
    public void SynchronizeScale( Shape shape )
    {
      if ( Node == null )
        return;

      Node.transform.localScale = shape.GetScale();

      if ( shape is Collide.Mesh ) {
        if ( m_storedLossyScale != transform.lossyScale ) {
          RescaleRenderedMesh( shape as Collide.Mesh, Node.GetComponent<MeshFilter>() );
          m_storedLossyScale = transform.lossyScale;
        }
      }
      else if ( shape is Capsule ) {
        Capsule capsule = shape as Capsule;
        SetCapsuleSize( Node, capsule.Radius, capsule.Height );
      }
    }

    /// <summary>
    /// If no "Node" instance, this method tries to create one
    /// given the Collide.Shape component in this game object.
    /// </summary>
    /// <returns>True if the node was created - otherwise false.</returns>
    private bool TryInitialize( Shape shape )
    {
      if ( Node != null )
        return false;

      Collide.Mesh mesh       = shape as Collide.Mesh;
      HeightField heightField = shape as HeightField;
      if ( mesh != null )
        Node = InitializeMesh( mesh );
      else if ( heightField != null )
        Node = InitializeHeightField( heightField );
      else {
        Node = PrefabLoader.Instantiate<GameObject>( PrefabName );
        Node.transform.localScale = GetShape().GetScale();
      }

      return Node != null;
    }

    /// <summary>
    /// Initializes and returns a game object if the Collide.Shape type
    /// is of type mesh. Fails if the shape type is different from mesh.
    /// </summary>
    /// <returns>Game object with mesh renderer.</returns>
    private GameObject InitializeMesh( Collide.Mesh mesh )
    {
      return InitializeMeshGivenSourceObject( mesh );
    }

    /// <summary>
    /// Initializes debug render object given the source object of the
    /// Collide.Mesh component.
    /// </summary>
    private GameObject InitializeMeshGivenSourceObject( Collide.Mesh mesh )
    {
      if ( mesh.SourceObject == null )
        throw new AgXUnity.Exception( "Mesh has no source." );

      GameObject meshData = new GameObject( "MeshData" );
      MeshRenderer renderer = meshData.AddComponent<MeshRenderer>();
      MeshFilter filter = meshData.AddComponent<MeshFilter>();

      filter.sharedMesh = new UnityEngine.Mesh();

      RescaleRenderedMesh( mesh, filter );

      renderer.sharedMaterial = Resources.Load<UnityEngine.Material>( "Materials/DebugRendererMaterial" );
      m_storedLossyScale = mesh.transform.lossyScale;

      return meshData;
    }

    /// <summary>
    /// Debug rendering of HeightField is currently not supported.
    /// </summary>
    private GameObject InitializeHeightField( HeightField hf )
    {
      return new GameObject( "HeightFieldData" );
    }

    private void RescaleRenderedMesh( Collide.Mesh mesh, MeshFilter filter )
    {
      UnityEngine.Mesh source = mesh.SourceObject;
      if ( source == null )
        throw new AgXUnity.Exception( "Source object is null during rescale." );

      Vector3[] vertices = filter.sharedMesh.vertices;
      if ( vertices == null || vertices.Length == 0 )
        vertices = new Vector3[ source.vertexCount ];

      int[] triangles = filter.sharedMesh.triangles;
      if ( triangles == null || triangles.Length == 0 )
        triangles = (int[])source.triangles.Clone();

      if ( vertices.Length != source.vertexCount )
        throw new AgXUnity.Exception( "Shape debug render mesh mismatch." );

      Matrix4x4 scaledToWorld  = mesh.transform.localToWorldMatrix;
      Vector3[] sourceVertices = mesh.SourceObject.vertices;

      // Transforms each vertex from local to world given scales, then
      // transforms each vertex back to local again - unscaled.
      for ( int i = 0; i < vertices.Length; ++i ) {
        Vector3 worldVertex = scaledToWorld * sourceVertices[ i ];
        vertices[ i ]       = mesh.transform.InverseTransformDirection( worldVertex );
      }

      filter.sharedMesh.vertices  = vertices;
      filter.sharedMesh.triangles = triangles;

      filter.sharedMesh.RecalculateBounds();
      filter.sharedMesh.RecalculateNormals();
    }
  }
}
