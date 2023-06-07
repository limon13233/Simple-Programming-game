using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ArrayLayout
{
    public int length;
    [System.Serializable]
    public struct rowData
    {
        public string[] row;

    }
    public rowData[] rows = new rowData[3];
}
