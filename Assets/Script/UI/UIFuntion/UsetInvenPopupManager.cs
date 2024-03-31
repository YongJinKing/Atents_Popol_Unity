using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UsetInvenPopupManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Coroutine PopupControll;
    Coroutine MouseControll;
    
    public UnityEvent<bool> LRPosition;//0 : Left 1 : Right
    public UnityEvent<int> Popup;
    public UnityEvent<Vector2> Event_SlotPosition;
    public GameObject UserInven;
    

    public void OnPointerEnter(PointerEventData eventData)
    {
        
        if(UserInven.transform.Find("PlayerAbility").Find("Inventory").GetComponent<DisplayInven>().items.Count > transform.GetSiblingIndex())
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
        StopAllCoroutines();
        UserInven.transform.Find("DescPopup").gameObject.SetActive(false);
        transform.Find("Button").gameObject.SetActive(false);
    }
    
    IEnumerator OnPopupControll()
    {
        yield return new WaitForSeconds(0.8f);
        UserInven.transform.Find("DescPopup").gameObject.SetActive(true);
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
}
