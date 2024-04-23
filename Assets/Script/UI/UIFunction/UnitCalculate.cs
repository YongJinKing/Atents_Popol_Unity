
using UnityEngine;
public class UnitCalculate
{
    private static UnitCalculate instance;
    
    private UnitCalculate()
    {

    }
    public static UnitCalculate GetInstance()
    {
        if(UnitCalculate.instance == null)
            UnitCalculate.instance = new UnitCalculate();
        return UnitCalculate.instance;
    }

    public string Calculate(int Value)
    {
        string TranslateValue = "";
        TranslateValue = Value.ToString();
        if(Value >= 1000)
        {
            float TempValue;
            TempValue = Mathf.Floor(((float)Value / 1000.0f) * 100.0f) / 100.0f;
            TranslateValue = TempValue.ToString() + "k";
            if(Value >= 1000000)
            {
                TempValue = Mathf.Floor(((float)Value / 1000000.0f) * 100.0f) / 100.0f;
                TranslateValue = TempValue.ToString() + "m";
            }
        }
        return TranslateValue;
    }
}
