using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameSetting : MonoBehaviour
{
    public GameObject GameSettingPopup;
    
    public void PressedSettingBtn()
    {
        GameSettingPopup.SetActive(true);
    }
}
