using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class SkillSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject GridLine;
    public GameObject SkillPopup;
    public GameObject ColosseumEventObj;
    private RectTransform SlotPositon;
    private RectTransform PopupPositon;
    public List<GameObject> BossSkillList = new List<GameObject>();
    GameObject[] SkillSlots;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        
        if(transform.GetComponent<Image>().color.a != 0.0f)
        {
           /*  
            SlotPositon = transform.gameObject.GetComponent<RectTransform>();
            PopupPositon = SkillPopup.GetComponent<RectTransform>();
            PopupPositon.anchoredPosition = SlotPositon.anchoredPosition; */
            SkillPopup.SetActive(true);
            transform.GetChild(0).GetComponent<Animator>().SetBool("ChangeColor",true);
            SkillDetailPopup(transform.GetSiblingIndex());
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
        SkillPopup.transform.GetChild(0).GetComponent<Image>().sprite =
        BossSkillList[SkillIndex].transform.gameObject.GetComponent<Skill>().uiSkillStatus.uiSkillSprite;
        SkillPopup.transform.GetChild(1).GetComponent<TMP_Text>().text =
        BossSkillList[SkillIndex].transform.gameObject.GetComponent<Skill>().uiSkillStatus.uiSkillName;
        SkillPopup.transform.GetChild(2).GetComponent<TMP_Text>().text =
        BossSkillList[SkillIndex].transform.gameObject.GetComponent<Skill>().uiSkillStatus.uiSkillDesc;
    }
}
