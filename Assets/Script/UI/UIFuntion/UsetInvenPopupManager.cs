using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UsetInvenPopupManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Coroutine MouseControll;
    public UnityEvent<bool> LRPosition;//0 : Left 1 : Right
    public UnityEvent<Vector2> Event_SlotPosition;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(transform.GetComponent<RectTransform>().anchoredPosition.x > 300)
            LRPosition.Invoke(true);
        else
            LRPosition.Invoke(false);
        MouseControll = StartCoroutine(OnMouseControll());
        transform.Find("Button").gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(MouseControll != null)
        {
            StopCoroutine(MouseControll);
            MouseControll = null;
        }
        transform.Find("Button").gameObject.SetActive(false);
    }
    
    IEnumerator OnMouseControll()
    {
        while(true)
        {
            if(Input.GetMouseButtonDown(1))
            {
                Event_SlotPosition.Invoke(transform.GetComponent<RectTransform>().anchoredPosition);

            }
            yield return null;
        }
        
    }
}
