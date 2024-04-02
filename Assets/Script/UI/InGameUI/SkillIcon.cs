using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Unity.VisualScripting;


public class SkillIcon : MonoBehaviour, IPointerClickHandler
{
    public Image myImage;
    public float uiSkillCoolTime;

    private bool isCooling = false;
    public GameObject objectToThrow; // ���ε�

    GameObject Slash;
    SkillManager sm;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isCooling)
        {
            StartCoroutine(CoolTime(3.0f));
        }
    }

    void Start()
    {
        Slash = GameObject.Find("Slash");
        sm = Slash.GetComponent<SkillManager>();
        uiUnitSkillStatus uiSkillSprite = new uiUnitSkillStatus();
        gameObject.GetComponent<SpriteRenderer>().sprite = uiSkillSprite.uiSkillSprite;
    }

    void Update()
    {
        
    }

    IEnumerator CoolTime(float t)
    {
        isCooling = true;
        float playTime = 0.0f;
        while (playTime < t)
        {
            playTime += Time.deltaTime;
            myImage.fillAmount = playTime / t;
            yield return null;
        }
        isCooling = false;
    }
}