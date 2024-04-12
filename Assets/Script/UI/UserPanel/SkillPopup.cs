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
            var go = Content.transform.GetChild(i).Find("Paper");
            go.Find("Image").GetComponent<Image>().sprite = 
            AllSlots[i].GetComponent<SkillManager>().uiSkillStatus.uiSkillSprite;
            go.Find("SkillName").GetChild(0).GetComponent<TMP_Text>().text = //Skill Name
            AllSlots[i].GetComponent<SkillManager>().uiSkillStatus.uiSkillName;
            go.Find("SkillType").GetChild(0).GetComponent<TMP_Text>().text = //SkillType
            ItemTypeIntToString.IntToStringUISkillType(AllSlots[i].GetComponent<SkillManager>().WeaponType);
            go.Find("SkillDesc").GetChild(0).GetComponent<TMP_Text>().text = //SkillType
            AllSlots[i].GetComponent<SkillManager>().uiSkillStatus.uiSkillDesc;
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
            }
            else
            {
                Content.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        for(int i = 0; i < PlayerSkill.transform.Find("GridLine").childCount; i++)
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
        inst.InGameSkill = new List<string>();
        for(int i = 0; i < PlayerSkill.transform.Find("GridLine").childCount; i++)
        {
            var go = PlayerSkill.transform.Find("GridLine").GetChild(i);
            if(go.GetComponent<Image>().sprite.name != "Grey")
                inst.InGameSkill.Add(go.GetComponent<Image>().sprite.name);
            else
                inst.InGameSkill.Add(null);
            inst.UiSkillList[index + (10 * inst.WeaponType)] = DragImage.GetComponent<Image>().sprite.name;
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

