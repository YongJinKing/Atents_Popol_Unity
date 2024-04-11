using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PercentCalculate : MonoBehaviour
{
    private void Start() {
        Debug.Log(Calculate(500, 50));
    }
    public static float Calculate(int ReferenceValue, int Value)
    {
        float CalculatePercent = 0.00f;
        CalculatePercent = (((float)Value / (float)ReferenceValue));
        return CalculatePercent;
    }
}
