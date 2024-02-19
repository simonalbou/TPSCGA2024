using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;

public class GridPainterWindow : EditorWindow
{
    float cellSize;
    Vector2 gridDisplayOffset;
    Color currentColor;
    bool paintMode;
    GridData boundData;
    
    SerializedObject serializedObject;
    SerializedProperty rows, columns, cellContents;

    [MenuItem("Window/Custom/Grid Painter Window %w")]
    public static void ShowWindow()
    {
        GridPainterWindow gpw = GetWindow<GridPainterWindow>();

        gpw.Init();
        gpw.Show();
    }
    
    public void Init()
    {
        // code d'initialisation

        cellSize = 32;
        gridDisplayOffset = new Vector2(50, 200);
        currentColor = Color.white;
        paintMode = false;
    }

    public void LoadGridData(GridData data)
    {
        boundData = data;
        if (data == null) return;
        serializedObject = new SerializedObject(boundData);
        rows = serializedObject.FindProperty("rows");
        columns = serializedObject.FindProperty("columns");
        cellContents = serializedObject.FindProperty("cellContents");
    }

    public void OnGUI()
    {
        // Link GridData
        EditorGUI.BeginChangeCheck();
        boundData = EditorGUILayout.ObjectField("Grid Data", boundData, typeof(GridData), false) as GridData;
        bool hasChanged = EditorGUI.EndChangeCheck();
        if (hasChanged) LoadGridData(boundData);

        // Exception: no grid is loaded
        if (boundData == null)
        {
            EditorGUILayout.LabelField("Please load a GridData to get started.");
            return;
        }

        serializedObject.Update();

        // Layout options
        EditorGUILayout.PropertyField(rows);
        EditorGUILayout.PropertyField(columns);
        cellContents.arraySize = rows.intValue * columns.intValue;

        cellSize = EditorGUILayout.FloatField("Cell Size", cellSize);
        currentColor = EditorGUILayout.ColorField("Color", currentColor);
        gridDisplayOffset = EditorGUILayout.Vector2Field("Grid Display Offset", gridDisplayOffset);

        // Process events
        Event e = Event.current;
        if (e.type == EventType.MouseDown) paintMode = true;
        if (e.type == EventType.MouseUp) paintMode = false;
        EditorGUILayout.LabelField($"X: {e.mousePosition.x} // Y: {e.mousePosition.y}");

        // Grid display
        if (rows.intValue > 0 && columns.intValue > 0)
        {
            for (int i = 0; i < columns.intValue; i++)
            {
                for (int j = 0; j < rows.intValue; j++)
                {
                    Rect cellRect = new Rect(i*cellSize + gridDisplayOffset.x, j*cellSize + gridDisplayOffset.y, cellSize, cellSize);
                    SerializedProperty cellProp = GetCell(i, j);
                    SerializedProperty cellColor = cellProp.FindPropertyRelative("color");

                    EditorGUI.DrawRect(cellRect, cellColor.colorValue);

                    if (cellRect.Contains(e.mousePosition) && paintMode)
                    {
                        // si le script arrive à cet endroit, alors la case [i][j] est en train d'être modifiée
                        cellColor.colorValue = currentColor;
                    }
                }
            }
        }

        serializedObject.ApplyModifiedProperties();

        //if (e.type == EventType.MouseDown || e.type == EventType.MouseUp || (e.type == EventType.MouseMove && paintMode))
        
        Repaint();
    }

    SerializedProperty GetCell(int x, int y)
    {
        return cellContents.GetArrayElementAtIndex(x * columns.intValue + y);
    }
}
