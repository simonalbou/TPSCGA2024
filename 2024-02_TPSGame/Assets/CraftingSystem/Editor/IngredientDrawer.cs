using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(Ingredient))]
public class IngredientDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float numberOfLines = 2;
        float lineHeight = EditorGUIUtility.singleLineHeight + 1;
        return numberOfLines * lineHeight;
        //return base.GetPropertyHeight(property, label);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        position.height = EditorGUIUtility.singleLineHeight;

        //EditorGUILayout.LabelField("Hello world 1");
        //EditorGUILayout.LabelField("Hello world 2");

        Rect labelRect = new Rect(position.x, position.y, EditorGUIUtility.labelWidth, position.height);
        EditorGUI.LabelField(labelRect, label);
        if (label != GUIContent.none)
            position = new Rect(position.x + labelRect.width, position.y, position.width - labelRect.width, position.height);

        // Children Serialized Properties
        SerializedProperty ingredientType = property.FindPropertyRelative("ingredientType");
        SerializedProperty enabledProp = property.FindPropertyRelative("enabled");
        SerializedProperty everInitialized = property.FindPropertyRelative("everInitialized");
        SerializedProperty colorProp = property.FindPropertyRelative("color");

        // First initialization ever
        if (!everInitialized.boolValue)
        {
            colorProp.colorValue = Color.white;
            everInitialized.boolValue = true;
        }

        // Tweakable parameters
        float enabledWidth = 24f;
        float ingredientTypeWidth = 60f;
        float colorWidth = 60f;
        float space = 5f;

        // Dynamic
        float remainingSpace = position.width - (enabledWidth + ingredientTypeWidth + colorWidth + 4*space);
        float nameWidth = remainingSpace * 0.5f;
        float amountOrVolumeWidth = remainingSpace * 0.5f;

        // TODO : déterminer la position et la taille, puis utiliser DrawRect pour le zoning
        float currentX = position.x;
        Rect enabledRect = new Rect(currentX, position.y, enabledWidth, position.height);
        currentX += enabledWidth + space;
        Rect ingredientTypeRect = new Rect(currentX, position.y, ingredientTypeWidth, position.height);
        currentX += ingredientTypeWidth + space;
        Rect nameRect = new Rect(currentX, position.y, nameWidth, position.height);
        currentX += nameWidth + space;
        Rect amountOrVolumeRect = new Rect(currentX, position.y, amountOrVolumeWidth, position.height);
        currentX += amountOrVolumeWidth + space;
        Rect colorRect = new Rect(currentX, position.y + position.height, colorWidth, position.height);

        //EditorGUI.DrawRect(position, Color.yellow);
        //EditorGUI.DrawRect(enabledRect, Color.cyan);
        //EditorGUI.DrawRect(ingredientTypeRect, Color.green);
        //EditorGUI.DrawRect(nameRect, Color.yellow);
        //EditorGUI.DrawRect(amountOrVolumeRect, new Color(1, 0.5f, 0f, 1f));
        //EditorGUI.DrawRect(colorRect, Color.magenta);

        EditorGUI.PropertyField(enabledRect, enabledProp, GUIContent.none);

        EditorGUI.BeginDisabledGroup(enabledProp.boolValue == false);

        EditorGUI.PropertyField(ingredientTypeRect, ingredientType, GUIContent.none);
        
        float prevWidth = EditorGUIUtility.labelWidth;
        EditorGUIUtility.labelWidth = 60;
        EditorGUI.PropertyField(nameRect, property.FindPropertyRelative("name"));

        SerializedProperty chosenMetric = null;
        if (ingredientType.enumValueIndex == (int)IngredientType.Solid)
        {
            chosenMetric = property.FindPropertyRelative("amount");
        }
        else
        {
            chosenMetric = property.FindPropertyRelative("volume");
        }
        EditorGUI.PropertyField(amountOrVolumeRect, chosenMetric);

        EditorGUIUtility.labelWidth = prevWidth;

        EditorGUI.PropertyField(colorRect, colorProp, GUIContent.none);

        EditorGUI.EndDisabledGroup();
    }
}
