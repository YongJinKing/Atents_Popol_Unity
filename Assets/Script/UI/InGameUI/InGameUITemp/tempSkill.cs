using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class tempSkill : MonoBehaviour
{
    public Image qSkillImage;
    public Image wSkillImage;
    public Image eSkillImage;
    public Image rSkillImage;

    private Dictionary<KeyCode, Image> skillImages;
    private Dictionary<KeyCode, Coroutine> cooldownCoroutines;

    void Start()
    {
        skillImages = new Dictionary<KeyCode, Image>()
        {
            { KeyCode.Q, qSkillImage },
            { KeyCode.W, wSkillImage },
            { KeyCode.E, eSkillImage },
            { KeyCode.R, rSkillImage }
        };

        cooldownCoroutines = new Dictionary<KeyCode, Coroutine>();

    }

    void Update()
    {
        foreach (var kvp in skillImages)
        {
            if (Input.GetKeyDown(kvp.Key) && !cooldownCoroutines.ContainsKey(kvp.Key))
            {
                StartCooldown(kvp.Key);
            }
        }
    }

    void StartCooldown(KeyCode key)
    {
        if (cooldownCoroutines.ContainsKey(key))
        {
            StopCoroutine(cooldownCoroutines[key]);
            cooldownCoroutines[key] = null;
        }

        cooldownCoroutines[key] = StartCoroutine(CoolTime(key, 3.0f)); // 3.0f는 쿨타임 시간입니다.
    }

    IEnumerator CoolTime(KeyCode key, float cooldownTime)
    {
        Image skillImage = skillImages[key];
        float playTime = 0.0f;

        while (playTime < cooldownTime)
        {
            playTime += Time.deltaTime;
            skillImage.fillAmount = playTime / cooldownTime;
            yield return null;
        }

        cooldownCoroutines.Remove(key);
    }
}