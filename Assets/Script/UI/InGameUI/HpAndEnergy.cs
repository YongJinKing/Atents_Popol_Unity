using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class HpAndEnergy : MonoBehaviour
{
    public Image HPbar;
    public Image Energybar;
    public UnityEvent UseEnergy;
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
        UseEnergy?.Invoke();

    }
    
}
