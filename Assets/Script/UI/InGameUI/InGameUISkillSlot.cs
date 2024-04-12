using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;
using UnityEngine.UI;

public class InGameUISkillSlot : MonoBehaviour
{
    SkillManager[] InGameSkillList = new SkillManager[4];
    void Start()
    {
        var SkillSlotList = transform.Find("Bg").Find("Paper").Find("GridLine");
        
        for(int i = 0; i < SkillSlotList.childCount; i++)
        {
            GameObject SkillGameObject = Resources.Load<GameObject>
            ($"Player/SkillEffect/{ItemTypeIntToString.IntToStringSkillFileName(DataManager.instance.playerData.WeaponType)}/{DataManager.instance.playerData.InGameSkill[i]}");
            
            if(SkillGameObject != null)
            {
                SkillSlotList.GetChild(i).Find("SkillImage").GetComponent<Image>().sprite
                = SkillGameObject.GetComponent<SkillManager>().uiSkillStatus.uiSkillSprite;
                InGameSkillList[i] = SkillGameObject.GetComponent<SkillManager>();
                Debug.Log(SkillGameObject.GetComponent<SkillManager>().uiSkillStatus.uiSkillSprite);
                Color color = SkillSlotList.GetChild(i).Find("SkillImage").GetComponent<Image>().color;
                color.a = 1.0f;
                SkillSlotList.GetChild(i).Find("SkillImage").GetComponent<Image>().color = color;
            }
            else
            {
                Color color = SkillSlotList.GetChild(i).Find("SkillImage").GetComponent<Image>().color;
                color.a = 0.0f;
                SkillSlotList.GetChild(i).Find("SkillImage").GetComponent<Image>().color = color;
            }
        }    
    }
    public void UseSkill(int index)
    {
        var SkillSlotList = transform.Find("Bg").Find("Paper").Find("GridLine");
        SkillSlotList.GetChild(index).Find("CoolTime").GetComponent<Image>().fillAmount = 1.0f;
        StartCoroutine(SkillCoolTime(index));
    }

    IEnumerator SkillCoolTime(int index)
    {
        var SkillSlotList = transform.Find("Bg").Find("Paper").Find("GridLine");
        var CurrentCoolTime = InGameSkillList[index].CoolTime;
        InGameSkillList[index].CoolTimeCheck = true;
        while(CurrentCoolTime > 0)
        {   
            CurrentCoolTime -= Time.deltaTime;
            if(CurrentCoolTime < 0)
            {
                InGameSkillList[index].CoolTimeCheck = false;
                CurrentCoolTime = 0.0f;
            }
            SkillSlotList.GetChild(index).Find("CoolTime").GetComponent<Image>().fillAmount = 
            (float)CurrentCoolTime / InGameSkillList[index].CoolTime;
            yield return null;
        }
    }


   
}
