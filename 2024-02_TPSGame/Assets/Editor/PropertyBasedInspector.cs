using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(SampleComponent))]
public class PropertyBasedInspector : Editor
{
    SerializedProperty speed;
    SerializedProperty jumpsAllowed;
    SerializedProperty someSlider;
    SerializedProperty acceleration;
    SerializedProperty color;
    SerializedProperty textValues;

    SerializedProperty selfTransform;
    SerializedProperty selfCollider;
    SerializedProperty animator;

    string arrayReplacementString;


    // appelé au moment où l'inspector s'initialise (quand il est ouvert)
    void OnEnable()
    {
        speed = serializedObject.FindProperty("speed");
        jumpsAllowed = serializedObject.FindProperty("jumpsAllowed");
        someSlider = serializedObject.FindProperty("someSlider");
        acceleration = serializedObject.FindProperty("acceleration");
        color = serializedObject.FindProperty("color");
        textValues = serializedObject.FindProperty("textValues");

        selfTransform = serializedObject.FindProperty("selfTransform");
        selfCollider = serializedObject.FindProperty("selfCollider");
        animator = serializedObject.FindProperty("animator");
    }

    // décrit le contenu de l'inspector
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        EditorGUILayout.LabelField("Approche 2 : SerializedProperties séparées");

        // état du vrai objet copié dans sa représentation
        serializedObject.Update();

        EditorGUILayout.LabelField("Parameters", EditorStyles.boldLabel);
        EditorGUILayout.BeginVertical("box");
        GUIContent speedText = new GUIContent("My Speed", "Number of units travelled in one second. Look, a tooltip!");
        EditorGUILayout.PropertyField(speed, speedText);
        if (speed.floatValue < 0) speed.floatValue = 0;
        EditorGUILayout.PropertyField(jumpsAllowed);
        EditorGUILayout.Slider(someSlider, 1, 10);
        EditorGUILayout.PropertyField(acceleration);
        EditorGUILayout.PropertyField(color);
        EditorGUILayout.PropertyField(textValues);
        EditorGUILayout.EndVertical();

        EditorGUILayout.Space(16);
        EditorGUILayout.LabelField("Array info", EditorStyles.boldLabel);
        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField($"Array size: {textValues.arraySize}");
        if (GUILayout.Button("+1"))
        {
            textValues.arraySize++;
        }
        if (GUILayout.Button("-1"))
        {
            textValues.arraySize--;
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        arrayReplacementString = EditorGUILayout.TextField("String to replace", arrayReplacementString);
        if (GUILayout.Button("Assign"))
        {
            SerializedProperty stringProp = textValues.GetArrayElementAtIndex(2);
            stringProp.stringValue = arrayReplacementString;
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();

        EditorGUILayout.Space(16);
        EditorGUILayout.LabelField("References", EditorStyles.boldLabel);
        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.PropertyField(selfTransform);
        EditorGUILayout.PropertyField(selfCollider);
        EditorGUILayout.PropertyField(animator);
        EditorGUILayout.EndVertical();

        // état de la représentation copié dans le vrai objet
        serializedObject.ApplyModifiedProperties();
    }

    // TODO : change this
    void AutoSetReferences()
    {
        
    }
}
