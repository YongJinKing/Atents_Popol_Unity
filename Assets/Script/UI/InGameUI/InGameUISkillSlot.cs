using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;
using UnityEngine.UI;

public class InGameUISkillSlot : MonoBehaviour
{
    SkillManager[] InGameSkillList = new SkillManager[4];
    public GameObject HpAndEnergy;
    void Start()
    {
        var SkillSlotList = transform.GetChild(0).GetChild(0).GetChild(0);//Skillslot//Bg/Paper/GridLine
        
        for(int i = 0; i < SkillSlotList.childCount; i++)
        {
            GameObject SkillGameObject = Resources.Load<GameObject>
            ($"Player/SkillEffect/{ItemTypeIntToString.IntToStringSkillFileName(DataManager.instance.playerData.WeaponType)}/{DataManager.instance.playerData.InGameSkill[i]}");
            
            if(SkillGameObject != null)
            {
                SkillSlotList.GetChild(i).Find("SkillImage").GetComponent<Image>().sprite
                = SkillGameObject.GetComponent<SkillManager>().uiSkillStatus.uiSkillSprite;
                InGameSkillList[i] = SkillGameObject.GetComponent<SkillManager>();
                
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
    private void Update() 
    {
        PlayerEnergyCheck();
    }
    public void PlayerEnergyCheck() 
    {
        var SkillSlotList = transform.GetChild(0).GetChild(0).GetChild(0);//Skillslot//Bg/Paper/GridLine
        //Debug.Log($"에너지 체크 : {(int)(HpAndEnergy.GetComponent<HpAndEnergy>().Energybar.fillAmount) * 100}");
        for(int i = 0; i < SkillSlotList.childCount; i++)//Skillslot//Bg/Paper/GridLine
        {
            
            //Debug.Log($"왼쪽 : {InGameSkillList[i]?.EnergyGage}, 오른쪽 : {(HpAndEnergy.GetComponent<HpAndEnergy>().Energybar.fillAmount) * 100.0f}");
            if(InGameSkillList[i]?.EnergyGage <= ((HpAndEnergy.GetComponent<HpAndEnergy>().Energybar.fillAmount) * 100.0f))
            {
                
                SkillSlotList.GetChild(i).GetChild(2).gameObject.SetActive(false);
            }
            else
            {
                
                if(InGameSkillList[i] != null)
                    SkillSlotList.GetChild(i).GetChild(2).gameObject.SetActive(true);//Skillslot//Bg/Paper/GridLine/SkillSlot/EnergyCheck
            }
        }
    }
    public void UseSkill(int index)
    {
        var SkillSlotList = transform.GetChild(0).GetChild(0).GetChild(0);//Skillslot//Bg/Paper/GridLine
        SkillSlotList.GetChild(index).Find("CoolTime").GetComponent<Image>().fillAmount = 1.0f;
        StartCoroutine(SkillCoolTime(index));
    }

    IEnumerator SkillCoolTime(int index)
    {
        var SkillSlotList = transform.GetChild(0).GetChild(0).GetChild(0);//Skillslot//Bg/Paper/GridLine
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
