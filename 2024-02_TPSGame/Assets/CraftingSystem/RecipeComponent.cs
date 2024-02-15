using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum IngredientType
{
    Solid,
    Liquid
}

[System.Serializable]
public struct Ingredient
{
    public bool enabled;
    public IngredientType ingredientType;
    public string name;
    public int amount; // solids only
    public float volume; // liquids only
    public Color color;

    #if UNITY_EDITOR
    public float ingredientEnumWidth;
    public bool everInitialized;
    #endif
}

public class RecipeComponent : MonoBehaviour
{
    public Ingredient[] ingredients;
}
