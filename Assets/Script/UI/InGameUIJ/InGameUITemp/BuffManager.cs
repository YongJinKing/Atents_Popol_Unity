using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffManager : MonoBehaviour
{
    public static BuffManager Instance; // �̱��� �ν��Ͻ�

    public GameObject buffWindow; // ���� â
    public Image buffIcon; // ���� ������

    private void Awake()
    {
        // �̱��� �ν��Ͻ� ����
        if (Instance == null)
            Instance = this;
        //else
            //Destroy(gameObject);
    }

    public void ApplyBuff(Sprite icon)
    {
        buffIcon.sprite = icon;
        buffWindow.SetActive(true);
    }

    public void RemoveBuff()
    {
        buffWindow.SetActive(false);
    }
}
