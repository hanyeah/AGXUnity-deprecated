﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using GUI = AgXUnityEditor.Utils.GUI;

namespace AgXUnityEditor
{
  public class EditorData : ScriptableObject
  {
    public static EditorData Instance { get { return GetOrCreateInstance(); } }

    public double SecondsSinceLastGC { get { return EditorApplication.timeSinceStartup - m_lastGC; } }

    public int NumEntries { get { return m_data.Count; } }

    public int NumCachedEntries { get { return m_dataCache.Count; } }

    public EditorDataEntry GetStaticData( string identifier, Action<EditorDataEntry> onCreate = null )
    {
      return GetData( null, identifier, onCreate );
    }

    public EditorDataEntry GetData( UnityEngine.Object target, string identifier, Action<EditorDataEntry> onCreate = null )
    {
      var key = EditorDataEntry.CalculateKey( target, identifier );
      int dataIndex = -1;
      if ( !m_dataCache.TryGetValue( key, out dataIndex ) ) {
        dataIndex = m_data.FindIndex( data => data.Key == key );
        if ( dataIndex < 0 ) {
          EditorDataEntry instance = new EditorDataEntry( target, key );
          dataIndex = m_data.Count;

          m_data.Add( instance );

          onCreate?.Invoke( instance );
        }

        m_dataCache.Add( key, dataIndex );
      }

      return m_data[ dataIndex ];
    }

    public void GC()
    {
      m_dataCache.Clear();

      int index = 0;
      while ( index < m_data.Count ) {
        var data = m_data[ index ];
        if ( data == null || ( !data.IsStatic && EditorUtility.InstanceIDToObject( data.InstanceId ) == null ) )
          m_data.RemoveAt( index );
        else
          ++index;
      }

      m_lastGC = EditorApplication.timeSinceStartup;
    }

    [SerializeField]
    private List<EditorDataEntry> m_data = new List<EditorDataEntry>();
    private Dictionary<uint, int> m_dataCache = new Dictionary<uint, int>();

    [SerializeField]
    private double m_lastGC = 0.0;

    private static EditorData m_instance = null;
    private static EditorData GetOrCreateInstance()
    {
      if ( m_instance != null )
        return m_instance;

      return ( m_instance = EditorSettings.GetOrCreateEditorDataFolderFileInstance<EditorData>( "/Data.asset" ) );
    }
  }

  [CustomEditor( typeof( EditorData ) )]
  public class EditorDataEditor : BaseEditor<EditorData>
  {
    protected override bool OverrideOnInspectorGUI( EditorData target, GUISkin skin )
    {
      using ( GUI.AlignBlock.Center )
        GUILayout.Label( GUI.MakeLabel( "Editor data", 18, true ), skin.label );

      GUI.Separator3D();

      const float firstLabelWidth = 190;

      GUILayout.BeginHorizontal();
      {
        TimeSpan span = TimeSpan.FromSeconds( target.SecondsSinceLastGC );
        GUILayout.Label( GUI.MakeLabel( "Seconds since last GC:" ), skin.label, GUILayout.Width( firstLabelWidth ) );
        GUILayout.Label( GUI.MakeLabel( string.Format( "{0:D2}m:{1:D2}s", span.Minutes, span.Seconds ), true ), skin.label );
      }
      GUILayout.EndHorizontal();

      GUILayout.BeginHorizontal();
      {
        GUILayout.Label( GUI.MakeLabel( "Number of data entries:" ), skin.label, GUILayout.Width( firstLabelWidth ) );
        GUILayout.Label( GUI.MakeLabel( target.NumEntries.ToString(), true ), skin.label );
      }
      GUILayout.EndHorizontal();

      GUILayout.BeginHorizontal();
      {
        GUILayout.Label( GUI.MakeLabel( "Number of cached data entries:" ), skin.label, GUILayout.Width( firstLabelWidth ) );
        GUILayout.Label( GUI.MakeLabel( target.NumCachedEntries.ToString(), true ), skin.label );
      }
      GUILayout.EndHorizontal();

      GUI.Separator();
      using ( new GUI.ColorBlock( Color.Lerp( UnityEngine.GUI.color, Color.green, 0.25f ) ) )
      using ( GUI.AlignBlock.Center ) {
        if ( GUILayout.Button( GUI.MakeLabel( "Collect garbage" ), skin.button, GUILayout.Width( 110 ) ) )
          target.GC();
      }
      GUI.Separator();

      EditorUtility.SetDirty( target );

      return true;
    }
  }
}
