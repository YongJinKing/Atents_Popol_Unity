using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    //public GameObject SlotObject;
    public class SlotList
    {
        public GameObject gameObject;
        public bool ChooseSlot;
    }

    public GameObject Inventory;
    private List<SlotList> slotList = new List<SlotList>();


    private void Start() 
    {
        for(int i = 0; i < Inventory.GetComponent<Inventory>().slots.Length; i++)
        {
            int index = i;
            //클래스 생성만
            SlotList temp = new SlotList();

            //클래스 초기화
            temp.gameObject = Inventory.transform.GetChild(1).GetChild(i).GetChild(1).gameObject;
            temp.ChooseSlot = false;
            slotList.Add(temp);
            slotList[i].gameObject.GetComponent<Button>().onClick.AddListener(() => btnchoise(index));
        } 
        
    }
    
    void CleanSlots()
    {
        for(int i = 0; i < Inventory.GetComponent<Inventory>().slots.Length; i++)
        {
            Color color = slotList[i].gameObject.GetComponent<UnityEngine.UI.Image>().color;//현재 버튼 색깔인자값 전달받기
            slotList[i].ChooseSlot = false;//점등 X
            color.a = 0.0f;
            slotList[i].gameObject.GetComponent<UnityEngine.UI.Image>().color = color;
        } 
    }

    void btnchoise(int buttonId)
    {
        
        if(slotList[buttonId].ChooseSlot) 
        {
            CleanSlots();
            return;
        }
        else
        {
            CleanSlots();
            slotList[buttonId].ChooseSlot = true;
        }
        
        if(slotList[buttonId].ChooseSlot)
        {
            Color color = slotList[buttonId].gameObject.GetComponent<UnityEngine.UI.Image>().color;
            color.a = 0.3f;
            slotList[buttonId].gameObject.GetComponent<UnityEngine.UI.Image>().color = color;
        }
    }
    
}


