using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(RecipeComponent))]
public class RecipeComponentInspector : Editor
{
    SerializedProperty ingredients;

    void OnEnable()
    {
        ingredients = serializedObject.FindProperty("ingredients");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Ingredients:", EditorStyles.boldLabel);
        if (GUILayout.Button("+1")) ingredients.arraySize++;
        if (GUILayout.Button("-1")) ingredients.arraySize--;
        EditorGUILayout.EndHorizontal();
        
        if (ingredients.arraySize > 0)
        {
            for (int i = 0; i < ingredients.arraySize; i++)
            {
                SerializedProperty prop = ingredients.GetArrayElementAtIndex(i);
                EditorGUILayout.PropertyField(prop, GUIContent.none);
            }
        }

        serializedObject.ApplyModifiedProperties();
    }
}
