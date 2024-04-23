using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RiggingItemChange : MonoBehaviour
{
    public UnityEvent<int> EventSlotNum;
    public GameObject RiggingPopup;
    IEnumerator OnMouseControll(int index)
    {
        while(true)
        {
            if(Input.GetMouseButtonDown(1))
            {
                PopupControll(index);
                EventSlotNum?.Invoke(index);
            }
            yield return null;
        }   
    }
    public void PopupControll(int index)
    {
        RiggingPopup.gameObject.SetActive(true);
    }
}
