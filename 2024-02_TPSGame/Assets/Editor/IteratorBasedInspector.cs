using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
//[CustomEditor(typeof(SampleComponent))]
public class IteratorBasedInspector : Editor
{
    SerializedProperty currentProp;

    // appelé au moment où l'inspector s'initialise (quand il est ouvert)
    void OnEnable()
    {
        currentProp = serializedObject.GetIterator();
    }

    // décrit le contenu de l'inspector
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        EditorGUILayout.LabelField("Approche 3 : itérateur");

        // état du vrai objet copié dans sa représentation
        serializedObject.Update();

        currentProp.NextVisible(true);
        while (currentProp.NextVisible(false))
        {
            EditorGUILayout.PropertyField(currentProp);
        }

        currentProp.Reset();

        // état de la représentation copié dans le vrai objet
        serializedObject.ApplyModifiedProperties();
    }

    // TODO : change this
    void AutoSetReferences()
    {
        
    }
}
