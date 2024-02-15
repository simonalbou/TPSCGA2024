using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GridData))]
public class GridDataInspector : Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Click here to open the Grid Editor", GUILayout.MinHeight(60)))
        {
            GridPainterWindow gpw = EditorWindow.GetWindow<GridPainterWindow>();

            // initialiser la fenêtre en y chargeant les informations de ce scriptable object
            gpw.Init();
            gpw.LoadGridData(target as GridData);
            gpw.Show();
        }
    }
}
