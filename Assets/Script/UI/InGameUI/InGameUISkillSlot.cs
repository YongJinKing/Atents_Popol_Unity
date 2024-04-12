using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;
using UnityEngine.UI;

public class InGameUISkillSlot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var SkillSlotList = transform.Find("Bg").Find("Paper").Find("GridLine");
        
        for(int i = 0; i < SkillSlotList.childCount; i++)
        {
            GameObject gameObject = Resources.Load<GameObject>
            ($"Player/SkillEffect/{ItemTypeIntToString.IntToStringSkillFileName(DataManager.instance.playerData.WeaponType)}/{DataManager.instance.playerData.InGameSkill[i]}");
            
            if(gameObject != null)
            {
                SkillSlotList.GetChild(i).Find("SkillImage").GetComponent<Image>().sprite
                = gameObject.GetComponent<SkillManager>().uiSkillStatus.uiSkillSprite;
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

   
}
