using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;


public class SlotHL : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{   
    public UnityEvent<int, bool> OnHighLite;
    public void OnPointerEnter(PointerEventData eventData)
    {
        OnHighLite?.Invoke(transform.GetSiblingIndex(),true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {   
        OnHighLite?.Invoke(transform.GetSiblingIndex(),false);
    }
}
