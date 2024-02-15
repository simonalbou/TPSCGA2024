using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(Ingredient))]
public class IngredientDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return base.GetPropertyHeight(property, label);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //EditorGUILayout.LabelField("Hello world 1");
        //EditorGUILayout.LabelField("Hello world 2");

        // TODO : déterminer la position et la taille, puis utiliser DrawRect pour le zoning
        Rect enabledRect = new Rect(0, 0, 1, 1);
        Rect ingredientTypeRect = new Rect(0, 0, 1, 1);
        Rect nameRect = new Rect(0, 0, 1, 1);
        Rect amountOrVolumeRect = new Rect(0, 0, 1, 1);
        Rect colorRect = new Rect(0, 0, 1, 1);

        EditorGUI.DrawRect(position, Color.yellow);

        EditorGUI.LabelField(position, "Hello world");
    }
}
