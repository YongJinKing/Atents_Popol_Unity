using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillIcon : MonoBehaviour, IPointerClickHandler
{
    public Image myImage;
    public void OnPointerClick(PointerEventData eventData)
    {
        StartCoroutine(CoolTime(3.0f));
    }

    IEnumerator CoolTime(float t)
    {
        float playTime = 0.0f;
        while(playTime < t)
        {
            playTime += Time.deltaTime;
            myImage.fillAmount = playTime / t;
            yield return null;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
