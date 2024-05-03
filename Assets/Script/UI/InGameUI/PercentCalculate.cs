using UnityEngine;

public class PercentCalculate : MonoBehaviour
{
    
    public static float Calculate(int ReferenceValue, int Value)
    {
        float CalculatePercent = 0.00f;
        CalculatePercent = (((float)Value / (float)ReferenceValue));
        return CalculatePercent;
    }
}
