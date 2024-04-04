using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffWindow : MonoBehaviour
{
    public Sprite[] skillIcons; // �� ��ų�� ���� ������ �迭
    private int currentSkillIndex = -1; // ���� ��� ���� ��ų�� �ε���

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            UseSkill(0);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            UseSkill(1);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            UseSkill(2);
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            UseSkill(3);
        }
    }

    void UseSkill(int index)
    {
        if (index < 0 || index >= skillIcons.Length)
            return;

        currentSkillIndex = index;

        // ��ų�� ����ϴ� �ڵ� �ۼ�

        // ��ų�� ����� �Ŀ� ���� â�� �������� ǥ��
        BuffManager.Instance.ApplyBuff(skillIcons[index]);
    }
}
