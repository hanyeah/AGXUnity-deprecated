﻿using System;
using UnityEngine;
using UnityEditor;
using AgXUnity;

namespace AgXUnityEditor
{
  public static class TopMenu
  {
    #region Shapes
    [MenuItem( "AgXUnity/Collide/Box" )]
    public static GameObject Box()
    {
      GameObject go = Factory.Create<AgXUnity.Collide.Box>();
      if ( go != null )
        Undo.RegisterCreatedObjectUndo( go, "shape" );
      return Selection.activeGameObject = go;
    }

    [MenuItem( "AgXUnity/Collide/Sphere" )]
    public static GameObject Sphere()
    {
      GameObject go = Factory.Create<AgXUnity.Collide.Sphere>();
      if ( go != null )
        Undo.RegisterCreatedObjectUndo( go, "shape" );
      return Selection.activeGameObject = go;
    }

    [MenuItem( "AgXUnity/Collide/Capsule" )]
    public static GameObject Capsule()
    {
      GameObject go = Factory.Create<AgXUnity.Collide.Capsule>();
      if ( go != null )
        Undo.RegisterCreatedObjectUndo( go, "shape" );
      return Selection.activeGameObject = go;
    }

    [MenuItem( "AgXUnity/Collide/Cylinder" )]
    public static GameObject Cylinder()
    {
      GameObject go = Factory.Create<AgXUnity.Collide.Cylinder>();
      if ( go != null )
        Undo.RegisterCreatedObjectUndo( go, "shape" );
      return Selection.activeGameObject = go;
    }

    [MenuItem( "AgXUnity/Collide/Plane" )]
    public static GameObject Plane()
    {
      GameObject go = Factory.Create<AgXUnity.Collide.Plane>();
      if ( go != null )
        Undo.RegisterCreatedObjectUndo( go, "shape" );
      return Selection.activeGameObject = go;
    }

    [MenuItem( "AgXUnity/Collide/Mesh" )]
    public static GameObject Mesh()
    {
      GameObject go = Factory.Create<AgXUnity.Collide.Mesh>();
      if ( go != null )
        Undo.RegisterCreatedObjectUndo( go, "shape" );
      return Selection.activeGameObject = go;
    }
    #endregion

    #region Rigid bodies
    [MenuItem( "AgXUnity/Rigid body/Empty" )]
    public static GameObject RigidBodyEmpty()
    {
      GameObject go = Factory.Create<AgXUnity.RigidBody>();
      if ( go != null )
        Undo.RegisterCreatedObjectUndo( go, "body" );
      return Selection.activeGameObject = go;
    }

    [MenuItem( "AgXUnity/Rigid body/Box" )]
    public static GameObject RigidBodyBox()
    {
      GameObject go = Factory.Create<AgXUnity.RigidBody>( Factory.Create<AgXUnity.Collide.Box>() );
      if ( go != null )
        Undo.RegisterCreatedObjectUndo( go, "body" );
      return Selection.activeGameObject = go;
    }

    [MenuItem( "AgXUnity/Rigid body/Sphere" )]
    public static GameObject RigidBodySphere()
    {
      GameObject go = Factory.Create<AgXUnity.RigidBody>( Factory.Create<AgXUnity.Collide.Sphere>() );
      if ( go != null )
        Undo.RegisterCreatedObjectUndo( go, "body" );
      return Selection.activeGameObject = go;
    }

    [MenuItem( "AgXUnity/Rigid body/Capsule" )]
    public static GameObject RigidBodyCapsule()
    {
      GameObject go = Factory.Create<AgXUnity.RigidBody>( Factory.Create<AgXUnity.Collide.Capsule>() );
      if ( go != null )
        Undo.RegisterCreatedObjectUndo( go, "body" );
      return Selection.activeGameObject = go;
    }

    [MenuItem( "AgXUnity/Rigid body/Cylinder" )]
    public static GameObject RigidBodyCylinder()
    {
      GameObject go = Factory.Create<AgXUnity.RigidBody>( Factory.Create<AgXUnity.Collide.Cylinder>() );
      if ( go != null )
        Undo.RegisterCreatedObjectUndo( go, "body" );
      return Selection.activeGameObject = go;
    }

    [MenuItem( "AgXUnity/Rigid body/Mesh" )]
    public static GameObject RigidBodyMesh()
    {
      GameObject go = Factory.Create<AgXUnity.RigidBody>( Factory.Create<AgXUnity.Collide.Mesh>() );
      if ( go != null )
        Undo.RegisterCreatedObjectUndo( go, "body" );
      return Selection.activeGameObject = go;
    }
    #endregion

    #region Constraint
    [MenuItem( "AgXUnity/Constraints/Hinge" )]
    public static GameObject ConstraintHinge()
    {
      GameObject go = Factory.Create( ConstraintType.Hinge );
      if ( go != null )
        Undo.RegisterCreatedObjectUndo( go, "constraint" );
      return Selection.activeGameObject = go;
    }

    [MenuItem( "AgXUnity/Constraints/Prismatic" )]
    public static GameObject ConstraintPrismatic()
    {
      GameObject go = Factory.Create( ConstraintType.Prismatic );
      if ( go != null )
        Undo.RegisterCreatedObjectUndo( go, "constraint" );
      return Selection.activeGameObject = go;
    }

    [MenuItem( "AgXUnity/Constraints/Lock Joint" )]
    public static GameObject ConstraintLockJoint()
    {
      GameObject go = Factory.Create( ConstraintType.LockJoint );
      if ( go != null )
        Undo.RegisterCreatedObjectUndo( go, "constraint" );
      return Selection.activeGameObject = go;
    }

    [MenuItem( "AgXUnity/Constraints/Cylindrical Joint" )]
    public static GameObject ConstraintCylindricalJoint()
    {
      GameObject go = Factory.Create( ConstraintType.CylindricalJoint );
      if ( go != null )
        Undo.RegisterCreatedObjectUndo( go, "constraint" );
      return Selection.activeGameObject = go;
    }

    [MenuItem( "AgXUnity/Constraints/Ball Joint" )]
    public static GameObject ConstraintBallJoint()
    {
      GameObject go = Factory.Create( ConstraintType.BallJoint );
      if ( go != null )
        Undo.RegisterCreatedObjectUndo( go, "constraint" );
      return Selection.activeGameObject = go;
    }

    [MenuItem( "AgXUnity/Constraints/Distance Joint" )]
    public static GameObject ConstraintDistanceJoint()
    {
      GameObject go = Factory.Create( ConstraintType.DistanceJoint );
      if ( go != null )
        Undo.RegisterCreatedObjectUndo( go, "constraint" );
      return Selection.activeGameObject = go;
    }

    [MenuItem( "AgXUnity/Constraints/Angular Lock Joint" )]
    public static GameObject ConstraintAngularLockJoint()
    {
      GameObject go = Factory.Create( ConstraintType.AngularLockJoint );
      if ( go != null )
        Undo.RegisterCreatedObjectUndo( go, "constraint" );
      return Selection.activeGameObject = go;
    }

    [MenuItem( "AgXUnity/Constraints/Plane Joint" )]
    public static GameObject ConstraintPlaneJoint()
    {
      GameObject go = Factory.Create( ConstraintType.PlaneJoint );
      if ( go != null )
        Undo.RegisterCreatedObjectUndo( go, "constraint" );
      return Selection.activeGameObject = go;
    }
    #endregion

    #region Wire
    [MenuItem( "AgXUnity/Wire/Empty" )]
    public static GameObject WireEmpty()
    {
      GameObject go = Factory.Create<Wire>();
      if ( go != null )
        Undo.RegisterCreatedObjectUndo( go, "wire" );
      return Selection.activeGameObject = go;
    }
    #endregion

    #region Cable
    [MenuItem( "AgXUnity/Cable/Empty" )]
    public static GameObject CableEmpty()
    {
      GameObject go = Factory.Create<Cable>();
      if ( go != null )
        Undo.RegisterCreatedObjectUndo( go, "cable" );

      return Selection.activeGameObject = go;
    }

    [MenuItem( "AgXUnity/Cable/Test 1" )]
    public static GameObject CableTest1()
    {
      Cable cable = Factory.CreateCable();
      cable.Route.Add( CableRouteNode.Create( Cable.NodeType.FreeNode, null, new Vector3( -5, 0, 0 ) ) );
      cable.Route.Add( CableRouteNode.Create( Cable.NodeType.FreeNode, null, new Vector3( 0, 3, 0 ) ) );
      cable.Route.Add( CableRouteNode.Create( Cable.NodeType.FreeNode, null, new Vector3( 5, 0, 0 ) ) );
      Undo.RegisterCreatedObjectUndo( cable.gameObject, "New test cable" );

      return Selection.activeGameObject = cable.gameObject;
    }
    #endregion

    #region Managers
    [ MenuItem( "AgXUnity/Debug Render Manager" ) ]
    public static GameObject DebugRenderer()
    {
      return Selection.activeGameObject = GetOrCreateUniqueGameObject<AgXUnity.Rendering.DebugRenderManager>().gameObject;
    }

    [MenuItem( "AgXUnity/Simulation" )]
    public static GameObject Simulation()
    {
      return Selection.activeGameObject = GetOrCreateUniqueGameObject<Simulation>().gameObject;
    }

    [MenuItem( "AgXUnity/Collision Groups Manager" )]
    public static GameObject CollisionGroupsManager()
    {
      return Selection.activeGameObject = GetOrCreateUniqueGameObject<CollisionGroupsManager>().gameObject;
    }

    [MenuItem( "AgXUnity/Contact Material Manager" )]
    public static GameObject ContactMaterialManager()
    {
      return Selection.activeGameObject = GetOrCreateUniqueGameObject<ContactMaterialManager>().gameObject;
    }

    [MenuItem( "AgXUnity/Wind and Water Manager" )]
    public static GameObject WindAndWaterManager()
    {
      return Selection.activeGameObject = GetOrCreateUniqueGameObject<WindAndWaterManager>().gameObject;
    }

    [MenuItem( "AgXUnity/Pick Handler (Game View)" )]
    public static GameObject PickHandler()
    {
      return Selection.activeGameObject = GetOrCreateUniqueGameObject<PickHandler>().gameObject;
    }
    #endregion

    #region Utils
    [MenuItem( "AgXUnity/Utils/Generate Custom Editors" )]
    public static void GenerateEditors()
    {
      Utils.CustomEditorGenerator.Generate();
    }

    public static T GetOrCreateUniqueGameObject<T>()
      where T : ScriptComponent
    {
      bool hadInstance = UniqueGameObject<T>.HasInstance;
      if ( UniqueGameObject<T>.Instance == null )
        UniqueGameObject<T>.ResetDestroyedState();

      T obj = UniqueGameObject<T>.Instance;
      if ( !hadInstance && obj != null )
        Undo.RegisterCreatedObjectUndo( obj.gameObject, "Created " + obj.name );

      return obj;
    }
    #endregion
  }
}
