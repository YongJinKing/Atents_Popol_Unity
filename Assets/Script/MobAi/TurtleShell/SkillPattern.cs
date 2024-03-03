using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SkillPattern : MonoBehaviour
{
    public List<AiSkill> skills = new List<AiSkill>();
    // Start is called before the first frame update
    void Start()
    {
        // 여기에 사용할 스킬들을 추가합니다.
        skills.Add(new AiSkill("Skill1", UseSkill1));
        skills.Add(new AiSkill("Skill2", UseSkill2));
        skills.Add(new AiSkill("Skill3", UseSkill3));


        // 코루틴을 시작하여 반복적으로 스킬을 사용합니다.
        StartCoroutine(UseSkillsRepeatedly());
    }

    IEnumerator UseSkillsRepeatedly()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f); // 스킬 사용 간격을 조절할 수 있습니다.

            // 스킬을 랜덤하게 선택하여 사용합니다.
            UseRandomSkill();
        }
    }

    // 스킬을 랜덤하게 사용합니다.
    void UseRandomSkill()
    {
        if (skills.Count == 0)
        {
            Debug.LogWarning("There are no skills available.");
            return;
        }

        // 사용 가능한 스킬 중에서 랜덤하게 선택합니다.
        int randomIndex = Random.Range(0, skills.Count);
        AiSkill randomSkill = skills[randomIndex];
        randomSkill.Use();
    }

    // 아래는 각각의 스킬을 사용하는 메서드들입니다.
    void UseSkill1()
    {
        Debug.Log("Using Skill 1");
    }

    void UseSkill2()
    {
        Debug.Log("Using Skill 2");
    }

    void UseSkill3()
    {
        Debug.Log("Using Skill 3");
    }
}

// 스킬 클래스 정의
public class AiSkill
{
    public string skillName;
    public UnityAction useAction;

    public AiSkill(string name, UnityAction useAction)
    {
        skillName = name;
        this.useAction = useAction;
    }

    public void Use()
    {
        useAction.Invoke();
    }
}