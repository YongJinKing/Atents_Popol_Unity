using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionPopUp : MonoBehaviour
{
    public GameObject settingsPanel; // ���� �˾� â
    public Button startButton;
    public Button reStartButton;
    public Button loobyButton;

    void Start()
    {
        settingsPanel.SetActive(false); // ������ �� ���� �˾� â�� ��Ȱ��ȭ
        startButton.onClick.AddListener(CloseSettings);
    }

    void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }

    public void ToggleSettings()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf); // ���� �˾� â�� Ȱ��ȭ/��Ȱ��ȭ�� ��ȯ
    }
}
