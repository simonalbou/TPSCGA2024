using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class EditorStartUpdateStructure
{
    static double timestamp;

    static EditorStartUpdateStructure()
    {
        Debug.Log("Editor is starting");

        EditorApplication.update += MyUpdate;

        timestamp = 0;
    }

    static void MyUpdate()
    {
        if (EditorApplication.timeSinceStartup > timestamp + 5)
        {
            //EditorStyles.label.normal.textColor = Color.green;
            //Debug.Log($"Unity has been up since {EditorApplication.timeSinceStartup} seconds.");
            //timestamp = EditorApplication.timeSinceStartup;
        }
    }
}
