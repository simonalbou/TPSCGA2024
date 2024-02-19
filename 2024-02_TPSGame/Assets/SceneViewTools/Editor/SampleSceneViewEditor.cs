using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SampleSceneViewTool))]
public class SampleSceneViewEditor : Editor
{
    SerializedProperty radius, degrees, waypoints;

    Transform objectTransform;

    void OnEnable()
    {
        radius = serializedObject.FindProperty("sphereRadius");
        degrees = serializedObject.FindProperty("degrees");
        waypoints = serializedObject.FindProperty("waypoints");

        objectTransform = (target as SampleSceneViewTool).transform;

        Undo.undoRedoPerformed += MyFunctionToCallOnUndoRedo;
    }

    void OnDisable()
    {
        Undo.undoRedoPerformed -= MyFunctionToCallOnUndoRedo;
    }

    void MyFunctionToCallOnUndoRedo()
    {
        Debug.Log("an undo or redo has been performed!");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }

    void OnSceneGUI()
    {
        #region non-interactive

        Handles.BeginGUI();
        EditorGUILayout.LabelField("Hello world");
        if (GUILayout.Button("Print something!", GUILayout.Width(100))) Debug.Log(target.name);
        Handles.EndGUI();

        Handles.matrix = objectTransform.localToWorldMatrix;
        Handles.color = new Color(0, 0, 1, 0.9f);
        //Handles.DrawSolidArc(Vector3.zero, Vector3.up, Vector3.forward, degrees.floatValue, radius.floatValue);
        Handles.color = new Color(0, 1, 0, 0.9f);

        #endregion

        #region interactive

        serializedObject.Update();

        //radius.floatValue = Handles.RadiusHandle(Quaternion.identity, Vector3.zero, radius.floatValue);
        radius.floatValue = Handles.ScaleValueHandle(
            radius.floatValue,
            Vector3.zero,
            Quaternion.LookRotation(Vector3.forward),
            HandleUtility.GetHandleSize(Vector3.zero) * 10,
            Handles.ArrowHandleCap,
            0);

        Handles.Label(Vector3.up, $"{radius.floatValue}");

        if (waypoints.arraySize > 0)
        {
            for (int i = 0; i < waypoints.arraySize; i++)
            {
                SerializedProperty currentPoint = waypoints.GetArrayElementAtIndex(i);
                //currentPoint.vector3Value = Handles.PositionHandle(currentPoint.vector3Value, Quaternion.identity);
                float constantSizeAdjuster = HandleUtility.GetHandleSize(currentPoint.vector3Value);
                currentPoint.vector3Value = Handles.FreeMoveHandle(currentPoint.vector3Value, 0.5f * constantSizeAdjuster, Vector3.zero, Handles.CubeHandleCap);
                Handles.Label(currentPoint.vector3Value + Vector3.up, $"Point number {i}");
            }
        }

        serializedObject.ApplyModifiedProperties();

        #endregion

        // Transform Handle : 
        /**/

        Handles.matrix = Matrix4x4.identity;
        //objectTransform.rotation = Handles.RotationHandle(objectTransform.rotation, objectTransform.position + Vector3.right);
        //objectTransform.localScale = Handles.ScaleHandle(objectTransform.localScale, objectTransform.position + Vector3.right, objectTransform.rotation);
        
        
        Vector3 newPosition = objectTransform.position;
        Quaternion newRotation = objectTransform.rotation;
        Vector3 newScale = objectTransform.localScale;
        Handles.TransformHandle(ref newPosition, ref newRotation, ref newScale);

        Undo.RecordObject(objectTransform, "Changed transform with Handle");

        objectTransform.position = newPosition;
        objectTransform.rotation = newRotation;
        objectTransform.localScale = newScale;
        /**/
    }
}
