using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionPopUp : MonoBehaviour
{
    public GameObject settingsPanel; // 설정 팝업 창
    public Button startButton;
    public Button reStartButton;
    public Button loobyButton;

    void Start()
    {
        settingsPanel.SetActive(false); // 시작할 때 설정 팝업 창을 비활성화
        startButton.onClick.AddListener(CloseSettings);
    }

    void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }

    public void ToggleSettings()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf); // 설정 팝업 창의 활성화/비활성화를 전환
    }
}
