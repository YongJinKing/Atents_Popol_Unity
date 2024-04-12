using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpAndEnergy : MonoBehaviour
{
    public Image HPbar;
    public Image Energybar;
   /*  public int maxHp = 100;
    private void Start() {
        HpGageTrigger(maxHp, -30);
    } */
    public void HpGageTrigger(int ReferenceValue, int Value)
    {
        this.HPbar.fillAmount = (float)Value / (float)ReferenceValue;
    }
    public void EnergyGageTrigger(int ReferenceValue, int Value)
    {
        this.Energybar.fillAmount = (float)Value / (float)ReferenceValue;
    }
    
}
