using UnityEngine;
using UnityEngine.Events;

public class InGameSetting : MonoBehaviour
{
    public GameObject GameSettingPopup;
    public UnityEvent<float> ChangeTime;
    public void PressedSettingBtn()
    {
        GameSettingPopup.SetActive(true);
        ChangeTime?.Invoke(0.0f);
        
    }
}
