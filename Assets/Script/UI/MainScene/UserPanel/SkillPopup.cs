using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillPopup : MonoBehaviour
{
    
    public GameObject Content;
    public GameObject PlayerSkill;
    public GameObject DragImage;
    GameObject[] AllSlots;
    public bool[] isMouseInSlot = new bool[4];

    private void Start() 
    {
        SlotUpdate();
        SortSlot();
    }
    void SlotUpdate()
    {
        AllSlots = Resources.LoadAll<GameObject>("Player/SkillEffect");
        for(int i = 0; i < AllSlots.Length; i++)
        {
            Instantiate(Resources.Load<GameObject>("UI/UserSkill/SlotBg"), Content.transform);
            Content.transform.GetChild(i).GetComponent<UserSkillSlot>().WeaponType
            = AllSlots[i].GetComponent<SkillManager>().WeaponType;
            Content.transform.GetChild(i).GetComponent<UserSkillSlot>().SkillLevel
            = AllSlots[i].GetComponent<SkillManager>().Level;
            var go = Content.transform.GetChild(i).Find("Paper");
            go.Find("Image").GetComponent<Image>().sprite = 
            AllSlots[i].GetComponent<SkillManager>().uiSkillStatus.uiSkillSprite;
            go.Find("SkillName").GetChild(0).GetComponent<TMP_Text>().text = //Skill Name
            AllSlots[i].GetComponent<SkillManager>().uiSkillStatus.uiSkillName;
            go.Find("SkillType").GetChild(0).GetComponent<TMP_Text>().text = //SkillType
            ItemTypeIntToString.IntToStringUISkillType(AllSlots[i].GetComponent<SkillManager>().WeaponType);
            go.Find("SkillDesc").GetChild(0).GetComponent<TMP_Text>().text = //SkillType
            AllSlots[i].GetComponent<SkillManager>().uiSkillStatus.uiSkillDesc;
            go.Find("SkillLevel").GetChild(0).GetComponent<TMP_Text>().text = 
            AllSlots[i].GetComponent<SkillManager>().Level.ToString();
        }
    }
    public void SortSlot()
    {
        var inst = DataManager.instance.playerData;
        for(int i = 0; i < Content.transform.childCount; i++)
        {
            if(Content.transform.GetChild(i).GetComponent<UserSkillSlot>().WeaponType
                == DataManager.instance.playerData.WeaponType)
            {
                Content.transform.GetChild(i).gameObject.SetActive(true);
                if(Content.transform.GetChild(i).GetComponent<UserSkillSlot>().SkillLevel > DataManager.instance.playerData.Character_CurrentLevel)
                {
                    Content.transform.GetChild(i).GetChild(1).gameObject.SetActive(true);
                }
                
                else
                {
                    Content.transform.GetChild(i).GetChild(1).gameObject.SetActive(false);
                }
            }
            else
            {
                Content.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        for(int i = 0; i < PlayerSkill.transform.Find("GridLine").childCount; i++)//Road SkillSlot About RiggingWeapon
        {
            if(inst.UiSkillList[i + (10 * inst.WeaponType)] != "")
            {
                //Debug.Log($"인덱스 체크  : {i + (10 * inst.WeaponType)} 배열 체크 :{inst.UiSkillList[i + (10 * inst.WeaponType)]} ");
                GameObject gameObject = Resources.Load<GameObject>($"Player/SkillEffect/{ItemTypeIntToString.IntToStringSkillFileName(inst.WeaponType)}/{inst.UiSkillList[i + (10 * inst.WeaponType)]}");
                if(gameObject != null)
                {
                    PlayerSkill.transform.Find("GridLine").GetChild(i).GetComponent<Image>().sprite =
                    gameObject.GetComponent<SkillManager>().uiSkillStatus.uiSkillSprite;
                }
            }
            else
            {
                PlayerSkill.transform.Find("GridLine").GetChild(i).GetComponent<Image>().sprite =
                Resources.Load<Sprite>("UI/UserSkill/Grey");
            }
        }
        for(int i = 0; i < Content.transform.childCount - 1; i++)//skill level sort
        {
            for(int j = 0; j < Content.transform.childCount - 1; j++)
            {
                if(Content.transform.GetChild(j).GetComponent<UserSkillSlot>().SkillLevel > 
                Content.transform.GetChild(j + 1).GetComponent<UserSkillSlot>().SkillLevel)
                {
                    Content.transform.GetChild(j).SetSiblingIndex(j + 1);
                }
            }
        }
        transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = // Displayed WeaponType
        ItemTypeIntToString.IntToStringUIDesc(DataManager.instance.playerData.WeaponType);
    }
    public void SkillChange(string SkillName, int index)
    {
        var inst = DataManager.instance.playerData;
        for(int i = 0; i < PlayerSkill.transform.Find("GridLine").childCount; i++)
        {
            var go = PlayerSkill.transform.Find("GridLine").GetChild(i);
            if(go.GetComponent<Image>().sprite.name == SkillName)
            {
                go.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/UserSkill/Grey");
            }
        }
        PlayerSkill.transform.Find("GridLine").GetChild(index).GetComponent<Image>().sprite =
        DragImage.GetComponent<Image>().sprite;
        inst.InGameSkill = new string[4];
        
        for(int i = 0; i < PlayerSkill.transform.Find("GridLine").childCount; i++)
        {
            var go = PlayerSkill.transform.Find("GridLine").GetChild(i);
            if(go.GetComponent<Image>().sprite.name != "Grey")
            {
                inst.InGameSkill[i] = go.GetComponent<Image>().sprite.name;
                inst.UiSkillList[i + (10 * inst.WeaponType)] =  go.GetComponent<Image>().sprite.name;
            }  
            else
            {
                inst.InGameSkill[i] = null;
                inst.UiSkillList[i + (10 * inst.WeaponType)] = null;
            }
                
            
        }
        
    }
    public void MouseInSlotCheck(int index, bool InCheck)
    {
        isMouseInSlot[index] = InCheck;
    }
    public void EndDrop(int index, Sprite image)
    {
        
        for(int i = 0; i < isMouseInSlot.Length; i++)
        {
            if(isMouseInSlot[i])
                return;
        }

        PlayerSkill.transform.Find("GridLine").GetChild(index).GetComponent<Image>().sprite
        = image;
    }
}

