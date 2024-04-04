using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    public GameObject windowToOpen; // ������ �ϴ� â

    void Start()
    {
        Button btn = GetComponent<Button>(); // ��ư ������Ʈ ��������
        btn.onClick.AddListener(OpenWindow); // ��ư Ŭ�� �� OpenWindow �޼��� ȣ��
    }

    void OpenWindow()
    {
        windowToOpen.SetActive(true); // â�� Ȱ��ȭ�Ͽ� ���̵��� ����
    }
}
