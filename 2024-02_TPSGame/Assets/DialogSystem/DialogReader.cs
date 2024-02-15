using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogReader : MonoBehaviour
{
    public TextMeshProUGUI dialogText, speakerNameText;
    public DialogData dialogData;
    [HideInInspector] public Color backgroundColor;
    int currentLine;

    // soit la variable est privée, soit elle est publique
    // soit la variable est sérialisée, soit elle ne l'est pas
    // soit on affiche la variable, soit pas


    void Start()
    {
        currentLine = -1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ReadNextLine();
        }
    }

    void ReadNextLine()
    {
        currentLine++;
        
        if (currentLine == dialogData.dialogLines.Length) currentLine = 0;
        // équivalent à :
        //currentLine %= dialogData.dialogLines.Length;
        
        dialogText.text = dialogData.dialogLines[currentLine].englishText;
        speakerNameText.text = dialogData.dialogLines[currentLine].speakerName;
    }
}
