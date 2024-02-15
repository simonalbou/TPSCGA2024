using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
//[CustomEditor(typeof(SampleComponent))]
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

        EditorGUILayout.LabelField("Approche 1 : bruteforce");

        EditorGUI.BeginChangeCheck();

        EditorGUILayout.Space(8);
        EditorGUILayout.LabelField("Parameters", EditorStyles.boldLabel); // GUIStyle : définit l'aspect du texte

        EditorGUILayout.BeginVertical("box");

        GUIContent speedText = new GUIContent("My Speed", "Number of units travelled in one second. Look, a tooltip!");
        float result = EditorGUILayout.FloatField(speedText, myComponent.speed);
        if (result >= 0) myComponent.speed = result;

        myComponent.jumpsAllowed = EditorGUILayout.IntField("Jumps Allowed", myComponent.jumpsAllowed);
        myComponent.someSlider = EditorGUILayout.Slider("Slider Parameter", myComponent.someSlider, 1, 10);
        myComponent.acceleration = EditorGUILayout.CurveField("Acceleration", myComponent.acceleration);
        myComponent.color = EditorGUILayout.ColorField("Color", myComponent.color);
        EditorGUILayout.EndVertical();

        EditorGUILayout.Space(28);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("References", EditorStyles.boldLabel); // GUIStyle : définit l'aspect du texte
        Color previousColor = GUI.color;
        GUI.color = Color.green; // (0, 1, 0)
        if (GUILayout.Button("Auto-set")) AutoSetReferences();
        GUI.color = previousColor;
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginVertical("box");
        myComponent.selfTransform = EditorGUILayout.ObjectField("Self Transform", myComponent.selfTransform, typeof(Transform), true) as Transform;
        myComponent.selfCollider = EditorGUILayout.ObjectField("Self Collider", myComponent.selfCollider, typeof(Collider), true) as Collider;
        myComponent.animator = EditorGUILayout.ObjectField("Animator", myComponent.animator, typeof(Animator), true) as Animator;
        EditorGUILayout.EndVertical();

        bool userChangedSomething = EditorGUI.EndChangeCheck();

        if (userChangedSomething) EditorUtility.SetDirty(myComponent);

        //EditorGUILayout.LabelField("Hello world!");
    }

    void AutoSetReferences()
    {
        myComponent.selfTransform = myComponent.transform;
        Collider coll = myComponent.GetComponent<Collider>();
        if (coll) myComponent.selfCollider = coll;
        Animator anim = myComponent.GetComponent<Animator>();
        if (anim) myComponent.animator = anim;
    }
}
