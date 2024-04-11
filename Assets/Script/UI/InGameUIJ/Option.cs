using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    public GameObject windowToOpen; // 열고자 하는 창

    void Start()
    {
        Button btn = GetComponent<Button>(); // 버튼 컴포넌트 가져오기
        btn.onClick.AddListener(OpenWindow); // 버튼 클릭 시 OpenWindow 메서드 호출
    }

    void OpenWindow()
    {
        windowToOpen.SetActive(true); // 창을 활성화하여 보이도록 설정
    }
}
