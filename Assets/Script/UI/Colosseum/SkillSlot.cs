using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using UnityEditor.PackageManager;

public class SkillSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int index;
    public GameObject GridLine;
    public GameObject SkillPopup;
    public GameObject ColosseumEventObj;
    public List<GameObject> BossSkillList = new List<GameObject>();

    GameObject[] SkillSlots;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        
        if(transform.GetComponent<Image>().color.a != 0.0f)
        {
            SkillPopup.SetActive(true);
            transform.GetChild(0).GetComponent<Animator>().SetBool("ChangeColor",true);
            SkillDetailPopup(index);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SkillPopup.SetActive(false);
        transform.GetChild(0).GetComponent<Animator>().SetBool("ChangeColor",false);
    }
     void SkillDetailPopup(int SkillIndex)
    {

        BossSkillList = ColosseumEventObj.GetComponent<ColosseumEventManager>().BossMonsterSkillList;
        SkillIndex = index;
        SkillPopup.transform.GetChild(0).GetComponent<Image>().sprite =
        BossSkillList[SkillIndex].transform.gameObject.GetComponent<Skill>().uiSkillStatus.uiSkillSprite;
        SkillPopup.transform.GetChild(1).GetComponent<TMP_Text>().text =
        BossSkillList[SkillIndex].transform.gameObject.GetComponent<Skill>().uiSkillStatus.uiSkillName;
        SkillPopup.transform.GetChild(2).GetComponent<TMP_Text>().text =
        BossSkillList[SkillIndex].transform.gameObject.GetComponent<Skill>().uiSkillStatus.uiSkillDesc;
    }
}
