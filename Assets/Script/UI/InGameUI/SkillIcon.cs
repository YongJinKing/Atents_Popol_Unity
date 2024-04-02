using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Unity.VisualScripting;


public class SkillIcon : MonoBehaviour
{
    public Image myImage;
    public KeyCode skillKey; // 스킬을 사용할 키

    public float maxCoolTime = 3.0f;
    private float currentCoolTime = 0.0f;
    private bool isCooling = false;

    void Start()
    {
        ResetSkillIcon();
    }

    void Update()
    {
        if (isCooling)
        {
            currentCoolTime += Time.deltaTime;
            myImage.fillAmount = 1.0f - (currentCoolTime / maxCoolTime);
            if (currentCoolTime >= maxCoolTime)
            {
                isCooling = false;
                ResetSkillIcon();
            }
        }

        // 키 입력을 감지하여 스킬 사용
        if (Input.GetKeyDown(skillKey) && !isCooling)
        {
            StartCoolTime();
        }
    }

    void StartCoolTime()
    {
        isCooling = true;
        currentCoolTime = 0.0f;
    }

    void ResetSkillIcon()
    {
        myImage.fillAmount = 1.0f;
    }
}