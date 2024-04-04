using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffWindow : MonoBehaviour
{
    public Sprite[] skillIcons; // 각 스킬에 대한 아이콘 배열
    private int currentSkillIndex = -1; // 현재 사용 중인 스킬의 인덱스

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

        // 스킬을 사용하는 코드 작성

        // 스킬을 사용한 후에 버프 창에 아이콘을 표시
        BuffManager.Instance.ApplyBuff(skillIcons[index]);
    }
}
