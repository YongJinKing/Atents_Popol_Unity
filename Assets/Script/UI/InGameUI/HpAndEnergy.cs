using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpAndEnergy : MonoBehaviour
{
    
   /*  public int maxHp = 100;
    private void Start() {
        HpGageTrigger(maxHp, -30);
    } */
    public void HpGageTrigger(int ReferenceValue, int Value)
    {
        transform.Find("Hp").GetComponent<Image>().fillAmount +=
            PercentCalculate.Calculate(ReferenceValue, Value);
    }
    public void EnergyGageTrigger(int ReferenceValue, int Value)
    {
        transform.Find("Energy").GetComponent<Image>().fillAmount +=
            PercentCalculate.Calculate(ReferenceValue, Value);
    }
    
}
