
using System.Collections.Generic;

using Unity.VisualScripting;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;

public class UsetInvenPopupManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Coroutine PopupControll;
    Coroutine MouseControll;
    
    public UnityEvent<bool> LRPosition;//0 : Left 1 : Right
    public UnityEvent<int> Popup;
    public UnityEvent<Vector2> Event_SlotPosition;
    public GameObject Inven;
    

    public void OnPointerEnter(PointerEventData eventData)
    {
        
        if(Inven.transform.Find("PlayerAbility").Find("Inventory").GetComponent<DisplayInven>().items.Count > transform.GetSiblingIndex())
        {
            if(transform.GetComponent<RectTransform>().anchoredPosition.x >= 300)
                LRPosition?.Invoke(true);
            else
                LRPosition?.Invoke(false);
            StartCoroutine(OnPopupControll());
            StartCoroutine(OnMouseControll());
            transform.Find("Button").gameObject.SetActive(true);
        }
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(Inven.transform.Find("PlayerAbility").Find("Inventory").GetComponent<DisplayInven>().items.Count > transform.GetSiblingIndex())
        {
            StopAllCoroutines();
            Inven.transform.Find("DescPopup").gameObject.SetActive(false);
            transform.Find("Button").gameObject.SetActive(false);
        }
    }
    
    IEnumerator OnPopupControll()
    {
        var go = transform.GetComponent<UIItem>();
        yield return new WaitForSeconds(0.8f);
        Inven.transform.Find("DescPopup").gameObject.SetActive(true);
        Inven.transform.Find("DescPopup").Find("Paper").Find("ImgBg").Find("ItemImage").GetComponent<Image>().sprite
        = go.icon.sprite;
        Inven.transform.Find("DescPopup").Find("Paper").Find("ItemName").GetComponent<TMP_Text>().text
        = go.ItemName;
        Inven.transform.Find("DescPopup").Find("Paper").Find("ItemType").GetComponent<TMP_Text>().text
        = WeaponTypeToString(go.WeaponType);
        Inven.transform.Find("DescPopup").Find("Paper").Find("ItemValue").GetComponent<TMP_Text>().text
        = RiggingTypeToString(go.ItemRigging) + go.ItemValue.ToString();
        Inven.transform.Find("DescPopup").Find("Paper").Find("ItemDesc").GetComponent<TMP_Text>().text
        = go.ItemDesc;
        

        Event_SlotPosition?.Invoke(transform.GetComponent<RectTransform>().anchoredPosition);
    }


    IEnumerator OnMouseControll()
    {
        while(true)
        {
            if(Input.GetMouseButtonDown(1))
            {
                Popup?.Invoke(transform.GetSiblingIndex());
            }
            yield return null;
        }   
    }
    public string WeaponTypeToString(int index)
    {
        string Rtstring = "";
        //0 : 한손 검, 1: 양손 검, 2 : 한손 둔기, 3 : 양손 둔기, 4 : 창, 5 : 단검, 6 : 투창용 창, 10 : 가죽, 11 : 경갑, 12 : 판금
        if(index == 0)
            Rtstring = "한손 검";
        if(index == 1)
            Rtstring = "양손 검";
        if(index == 2)
            Rtstring = "한손 둔기";
        if(index == 3)
            Rtstring = "양손 둔기";
        if(index == 4)
            Rtstring = "창";
        if(index == 5)
            Rtstring = "단검";
        if(index == 6)
            Rtstring = "투창용 창";
        if(index == 10)
            Rtstring = "가죽";
        if(index == 11)
            Rtstring = "경갑";
        if(index == 12)
            Rtstring = "판금";
        return Rtstring;
    }
    public string RiggingTypeToString(int index)
    {
        string Rtstring = "";
        //0 : 무기 1 : 방어구
        if(index == 0)
            Rtstring = "공격력 : ";
        if(index == 1)
            Rtstring = "체력 : ";
       
        return Rtstring;
    }
}
