using UnityEngine;
using UnityEngine.Events;

public class InGameSetting : MonoBehaviour
{
    public GameObject GameSettingPopup;
    public UnityEvent PressedEsc;
    public void PressedSettingBtn()
    {
        PressedEsc?.Invoke();
        
    }
}
