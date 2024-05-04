using UnityEngine;
using UnityEngine.Events;

public class InGameSetting : MonoBehaviour
{
    public GameObject GameSettingPopup;
    public UnityEvent<float> ChangeTime;
    public void PressedSettingBtn()
    {
        GameSettingPopup.SetActive(true);
        Time.timeScale = 0.0f;
        ChangeTime?.Invoke(0.0f);
        
    }
}
