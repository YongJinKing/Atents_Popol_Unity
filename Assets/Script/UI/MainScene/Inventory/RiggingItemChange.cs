using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RiggingItemChange : MonoBehaviour
{
    public UnityEvent<int> EventSlotNum;
    public UnityEvent EventChangeItem;
    public UnityEvent PlayerAbilityUpdate;
    public UnityEvent CleanSlot;
    public GameObject SkillPopup;
    public GameObject RiggingPopup;
    int SlotIndex;
    Coroutine CorMouseControll;
    public void OnSlot(bool OnCheck, int index)
    {   
        
        if(OnCheck)
        {
            CorMouseControll = StartCoroutine(OnMouseControll());
        }
        else
        {
            if(CorMouseControll != null)
            {
                StopCoroutine(CorMouseControll);
                CorMouseControll = null;
            }
            
        }
        EventSlotNum?.Invoke(index);
    }
    IEnumerator OnMouseControll()
    {
        while(true)
        {
            if(Input.GetMouseButtonDown(1))
            {
                PopupControll();
            }
            yield return null;
        }   
    }
    public void PopupControll()
    {
        RiggingPopup.gameObject.SetActive(true);
        
    }
    public void PopupYesOrNo(int index)
    {
        if(index == 0)
        {
            EventChangeItem?.Invoke();
            PlayerAbilityUpdate?.Invoke();
            CleanSlot?.Invoke();
            SkillPopup.transform.GetComponent<SkillPopup>().WeaponChange();
        }
        RiggingPopup.gameObject.SetActive(false);
    }
}
