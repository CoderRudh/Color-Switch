using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColorSet { Blue, Yellow, Pink, Purple };
public enum ManualColorSet { Pink, Yellow, Purple, Blue };

public class Colors : MonoBehaviour
{
    public Dictionary<string, ColorSet> colorDict = new Dictionary<string, ColorSet>();

    [System.NonSerialized]
    public List<string> colorChoice = new List<string>();

    public Color colorBlue;
    public Color colorYellow;
    public Color colorPink;
    public Color colorPurple;

    //[System.NonSerialized]
    //public Color manualColor;

    //[System.NonSerialized]
    //public int colorSetValue;

    public void Init()
    {
        //manualColor = colorPink;
        // = 0;
        colorDict["Blue"] = ColorSet.Blue;
        colorDict["Yellow"] = ColorSet.Yellow;
        colorDict["Pink"] = ColorSet.Pink;
        colorDict["Purple"] = ColorSet.Purple;
        foreach (string cs in Enum.GetNames(typeof(ColorSet)))
        {
            colorChoice.Add(cs);
        }
    }
}
