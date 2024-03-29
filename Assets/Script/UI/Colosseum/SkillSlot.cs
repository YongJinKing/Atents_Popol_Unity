using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class SkillSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject SkillPopup;
    public GameObject ColoUI;
    Coroutine PopupCheck;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(transform.GetChild(0).GetComponent<Image>().color.a != 0.0f)
        {
            PopupCheck = StartCoroutine(OnSkillPopup(transform.GetSiblingIndex()));
            transform.GetChild(0).GetComponent<Animator>().SetBool("ChangeColor",true);
        }
        
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if(PopupCheck != null)
        {
            StopCoroutine(PopupCheck);
            PopupCheck = null;
        }
        SkillPopup.SetActive(false);
        
        transform.GetChild(0).GetComponent<Animator>().SetBool("ChangeColor",false);
    }
    IEnumerator OnSkillPopup(int index)
    {
        yield return new WaitForSeconds(0.5f);
        SkillPopup.SetActive(true);
        Debug.Log(40000 + index + ((ColoUI.GetComponent<ColoUI>().StageIndex - 1) * 100));
        var stageTableData = MonsterSkillDataManager.GetInstance().dicStageTable[ColoUI.GetComponent<ColoUI>().StageIndex];
        var SkillNameData = MonsterSkillDataManager.GetInstance().dicStringTable[40000 + index + ((ColoUI.GetComponent<ColoUI>().StageIndex - 1) * 100)];
        var SkillDescData = MonsterSkillDataManager.GetInstance().dicStringTable[50000 + index + ((ColoUI.GetComponent<ColoUI>().StageIndex - 1) * 100)];
        string SkillNameText = SkillNameData.String_Desc;
        string SkillDescText = SkillDescData.String_Desc;
        SkillPopup.transform.Find("SkillName").GetComponent<TMP_Text>().text =
        SkillNameText;
        SkillPopup.transform.Find("SkillDesc").GetComponent<TMP_Text>().text =
        SkillDescText;
        SkillPopup.transform.Find("Image").GetComponent<Image>().sprite = transform.GetChild(0).GetComponent<Image>().sprite;
    }
}
