using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillPopup : MonoBehaviour
{
    public GameObject Content;
    GameObject[] AllSlots;
    private void Start() 
    {
        SlotUpdate();
    }
    public void SlotUpdate()
    {
        AllSlots = Resources.LoadAll<GameObject>("Player/SkillEffect/TwoHandSwordSkill");
        for(int i = 0; i < AllSlots.Length; i++)
        {
            Instantiate(Resources.Load<GameObject>("UI/UserSkill/SlotBg"), Content.transform);
            var go = Content.transform.GetChild(i).Find("Paper");
            Content.transform.GetChild(i).Find("Paper").Find("Image").GetComponent<Image>().sprite = 
            AllSlots[i].GetComponent<SkillManager>().uiSkillStatus.uiSkillSprite;
        }   
    }
}

