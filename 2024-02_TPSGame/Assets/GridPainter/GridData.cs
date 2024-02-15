using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

[System.Serializable]
public struct CellContent
{
    public Color color;
    public int power;
}

[CreateAssetMenu(fileName = "New Grid Data.asset", menuName = "Custom/Grid Data", order = 100)]
public class GridData : ScriptableObject
{
    public int rows, columns;
    public CellContent[] cellContents; // flattened array
}