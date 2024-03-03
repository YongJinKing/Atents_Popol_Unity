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
        // ���⿡ ����� ��ų���� �߰��մϴ�.
        skills.Add(new AiSkill("Skill1", UseSkill1));
        skills.Add(new AiSkill("Skill2", UseSkill2));
        skills.Add(new AiSkill("Skill3", UseSkill3));


        // �ڷ�ƾ�� �����Ͽ� �ݺ������� ��ų�� ����մϴ�.
        StartCoroutine(UseSkillsRepeatedly());
    }

    IEnumerator UseSkillsRepeatedly()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f); // ��ų ��� ������ ������ �� �ֽ��ϴ�.

            // ��ų�� �����ϰ� �����Ͽ� ����մϴ�.
            UseRandomSkill();
        }
    }

    // ��ų�� �����ϰ� ����մϴ�.
    void UseRandomSkill()
    {
        if (skills.Count == 0)
        {
            Debug.LogWarning("There are no skills available.");
            return;
        }

        // ��� ������ ��ų �߿��� �����ϰ� �����մϴ�.
        int randomIndex = Random.Range(0, skills.Count);
        AiSkill randomSkill = skills[randomIndex];
        randomSkill.Use();
    }

    // �Ʒ��� ������ ��ų�� ����ϴ� �޼�����Դϴ�.
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

// ��ų Ŭ���� ����
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