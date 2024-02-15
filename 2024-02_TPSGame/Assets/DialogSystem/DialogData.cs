using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public struct DialogLine
{
    public string key;
    public string speakerName;
    public string englishText, frenchText, spanishText;
    public string portraitID, eventID;
}

[CreateAssetMenu(fileName = "NewDialogData.asset", menuName = "Custom/Dialog Data", order = 100)]
public class DialogData : ScriptableObject
{

#if UNITY_EDITOR
    [SerializeField]
    bool foldoutState;
#endif

    #region parameters

    public TextAsset csvFile;
    public DialogLine[] dialogLines;

    public Color color;
    public AnimationCurve someCurve;
    public Vector3 someVector;
    public Gradient someGradient;

    public Ingredient someIngredient;

    #endregion
}
