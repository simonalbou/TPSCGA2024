using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SampleComponent))]
public class SampleCustomInspector : Editor
{
    SampleComponent myComponent;

    // appelé au moment où l'inspector s'initialise (quand il est ouvert)
    void OnEnable()
    {
        myComponent = target as SampleComponent;

        Debug.Log(myComponent.speed);
    }

    // décrit le contenu de l'inspector
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        float result = EditorGUILayout.FloatField("My Speed", myComponent.speed);
        myComponent.speed = result;
        if (myComponent.speed < 0) myComponent.speed = 0;

        EditorUtility.SetDirty(myComponent);

        EditorGUILayout.LabelField("Hello world!");
    }
}
