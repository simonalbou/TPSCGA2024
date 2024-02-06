using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(DialogData))]
public class DialogDataInspector : Editor
{
    bool buttonToggle;
    DialogData dialogData;

    void OnEnable()
    {
        dialogData = target as DialogData;    
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.Space(32);

        EditorGUI.BeginDisabledGroup(dialogData.csvFile == null);
        if (GUILayout.Button("Copy contents from CSV to this Object"))
        {
            CopyCSVContents();
        }
        EditorGUI.EndDisabledGroup();

        EditorGUILayout.Space(32);
        if (GUILayout.Button("Convert to JSON"))
        {
            string jsonRepresentation = JsonUtility.ToJson(dialogData, true);
            Debug.Log(jsonRepresentation);
        }
    }

    void CopyCSVContents()
    {
        TextAsset csv = dialogData.csvFile;
        if (csv == null)
        {
            Debug.LogError("Attention, la référence du CSV est manquante");
            return;
        }

        // copie du CSV : récupérer le contenu du fichier texte, ligne par ligne
        string rawContent = csv.text;
        string[] separators = new string[] { "\n" };
        string[] lines = rawContent.Split(separators, StringSplitOptions.None);

        // filet de sécurité : vérifier que le tableau de lignes n'est ni nul, ni vide
        if (lines == null) return;
        if (lines.Length < 2) return;

        // mettre à jour le scriptable object avec le bon nombre de lignes
        dialogData.dialogLines = new DialogLine[lines.Length - 1]; // -1 car on exclut le header

        separators = new string[] { "\t" };
        for (int i = 1; i < lines.Length; i++) // i=1 car on exclut le header
        {
            string[] cells = lines[i].Split(separators, StringSplitOptions.None);

            if (cells.Length != 7)
            {
                //Debug.LogError("Line "+(i-1).ToString()+" has "+cells.Length.ToString()+" cells!");
                Debug.LogError($"Line {i-1} has {cells.Length} cells!");
                continue;
            }

            dialogData.dialogLines[i - 1].key = cells[0];
            dialogData.dialogLines[i - 1].speakerName = cells[1];
            dialogData.dialogLines[i - 1].portraitID = cells[2];
            dialogData.dialogLines[i - 1].eventID = cells[3];
            dialogData.dialogLines[i - 1].englishText = cells[4];
            dialogData.dialogLines[i - 1].frenchText = cells[5];
            dialogData.dialogLines[i - 1].spanishText = cells[6];
        }

        // valider / sauvegarder l'asset
        EditorUtility.SetDirty(dialogData);
    }

    // old version - demo
    /**
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.Space(32);

        EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Hello world");
            bool hasBeenPressed = GUILayout.Button("Copy contents from CSV to this Object");
        EditorGUILayout.EndHorizontal();

        if (hasBeenPressed)
        {
            Debug.Log("I pressed the button");

            buttonToggle = !buttonToggle;
        }

        if (buttonToggle)
        {
            EditorGUILayout.LabelField("I pressed the button!");            
        }
    }
    /**/
}
