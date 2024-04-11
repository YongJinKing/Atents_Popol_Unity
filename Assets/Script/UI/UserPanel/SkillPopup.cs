using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
    }
    void SlotUpdate()
    {
        AllSlots = Resources.LoadAll<GameObject>("Player/SkillEffect/TwoHandSwordSkill");
        for(int i = 0; i < AllSlots.Length; i++)
        {
            Instantiate(Resources.Load<GameObject>("UI/UserSkill/SlotBg"), Content.transform);
            var go = Content.transform.GetChild(i).Find("Paper");
            go.Find("Image").GetComponent<Image>().sprite = 
            AllSlots[i].GetComponent<SkillManager>().uiSkillStatus.uiSkillSprite;
            go.Find("SkillName").GetChild(0).GetComponent<TMP_Text>().text = //Skill Name
            AllSlots[i].GetComponent<SkillManager>().uiSkillStatus.uiSkillName;
            go.Find("SkillType").GetChild(0).GetComponent<TMP_Text>().text = //SkillType
            WeaponTypeToString(AllSlots[i].GetComponent<SkillManager>().WeaponType);
            go.Find("SkillDesc").GetChild(0).GetComponent<TMP_Text>().text = //SkillType
            AllSlots[i].GetComponent<SkillManager>().uiSkillStatus.uiSkillDesc;
        }   
    }
    public void SkillChange(string SkillName, int index)
    {
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
        DataManager.instance.playerData.Skill = new List<string>();
        for(int i = 0; i < PlayerSkill.transform.Find("GridLine").childCount; i++)
        {
            var go = PlayerSkill.transform.Find("GridLine").GetChild(i);
            DataManager.instance.playerData.Skill.Add(go.GetComponent<Image>().sprite.name);
        }
    }

    public string WeaponTypeToString(int index)
    {
        string Rtstring = "";
        //0 : 한손 검, 1: 양손 검, 2 : 한손 둔기, 3 : 양손 둔기, 4 : 창, 5 : 단검, 6 : 투창용 창, 10 : 가죽, 11 : 경갑, 12 : 판금
        if(index == 0)
            Rtstring = "참격";
        if(index == 1)
            Rtstring = "참격";
        if(index == 2)
            Rtstring = "타격";
        if(index == 3)
            Rtstring = "타격";
        if(index == 4)
            Rtstring = "관통";
        if(index == 5)
            Rtstring = "참격";
        if(index == 6)
            Rtstring = "관통";
        return Rtstring;
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

