using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;

public class MyFirstWindow : EditorWindow
{
    int rows, columns;
    float cellSize, yOffset;

    bool paintMode;

    //[MenuItem("Window/Custom/My First Window %w")]
    public static void ShowWindow()
    {
        MyFirstWindow mfw = GetWindow<MyFirstWindow>();

        mfw.Init();
        mfw.Show();
    }
    
    public void Init()
    {
        // code d'initialisation
        this.minSize = new Vector2(160, 90);
        //this.maxSize = new Vector2(800, 450);

        rows = 8;
        columns = 8;
        cellSize = 32;
        yOffset = 100;
        paintMode = false;
    }

    public void OnGUI()
    {
        // Layout options
        rows = EditorGUILayout.IntField("Rows", rows);
        columns = EditorGUILayout.IntField("Columns", columns);
        cellSize = EditorGUILayout.FloatField("Cell Size", cellSize);
        yOffset = EditorGUILayout.FloatField("Y Offset", yOffset);

        // Process events
        Event e = Event.current;
        if (e.type == EventType.MouseDown) paintMode = true;
        if (e.type == EventType.MouseUp) paintMode = false;
        EditorGUILayout.LabelField($"X: {e.mousePosition.x} // Y: {e.mousePosition.y}");

        // Grid display
        if (rows > 0 && columns > 0)
        {
            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    Rect cell = new Rect(i*cellSize, j*cellSize + yOffset, cellSize, cellSize);

                    if (i%2 == j%2)
                        EditorGUI.DrawRect(cell, Color.gray);

                    if (cell.Contains(e.mousePosition) && paintMode)
                        EditorGUI.DrawRect(cell, Color.yellow);
                }
            }
        }

        //if (e.type == EventType.MouseDown || e.type == EventType.MouseUp || (e.type == EventType.MouseMove && paintMode))
        Repaint();
    }
}
