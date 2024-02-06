using System.Collections;
using System.Collections.Generic;
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
    public TextAsset csvFile;
    public DialogLine[] dialogLines;

    public Color color;
    public AnimationCurve someCurve;
    public Vector3 someVector;
    public Gradient someGradient;
}
