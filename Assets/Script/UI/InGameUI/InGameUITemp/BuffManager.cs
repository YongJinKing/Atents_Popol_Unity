using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffManager : MonoBehaviour
{
    public static BuffManager Instance; // 싱글톤 인스턴스

    public GameObject buffWindow; // 버프 창
    public Image buffIcon; // 버프 아이콘

    private void Awake()
    {
        // 싱글톤 인스턴스 설정
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
