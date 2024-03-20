using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SmithUi : MonoBehaviour
{
    public GameObject GridLine;
    public GameObject Popup;
    public GameObject PopupBtnManager;
    public GameObject Inventory;
    int Slotindex;
    int smithFuntion;
    Button[] SlotBtnFunctionList ;
    int PrevIndex = -1;
    
    private void Start() 
    {
        SlotBtnFunctionList = GridLine.GetComponentsInChildren<Button>();
        for(int i = 0; i < SlotBtnFunctionList.Length; i++)
        {
            int index = i;
            SlotBtnFunctionList[i].onClick.AddListener(() => ChooseSlot(index));
        }
    }
    public void ChooseSlot(int index)
    {
        int id = GridLine.transform.GetChild(index).GetComponent<UIItem>().id;
        if(PrevIndex == index)
        {
            CleanSlot(-1);
            return;
        }
        else
        {
            if(id > 0)
            {
                CleanSlot(index);
                ImgColorChange(0.3f, index);
            }
            else
                return;
        }
    }
    void CleanSlot(int PrevIdxSetting)
    {
        for(int i = 0; i < GridLine.transform.childCount; i++)
        {
            ImgColorChange(0.0f, i);
        }
        PrevIndex = PrevIdxSetting;
    }
    void ImgColorChange(float Value, int index)
    {
        Color color = GridLine.transform.GetChild(index).GetChild(2).GetComponent<Image>().color;
        color.a = Value;
        GridLine.transform.GetChild(index).GetChild(2).GetComponent<Image>().color = color;
    }
    
    public void PressedPopupBtn(int index)
    {
        if(Slotindex > 0)
        {   
            String ModeText = "";
            Popup.transform.gameObject.SetActive(true);
            if(index == 0)
                ModeText = "수리";
            else if(index == 0)
                ModeText = "폐기";
            Popup.transform.GetChild(0).GetComponent<TMP_Text>().text =
            $"아이템을 {ModeText}하시겠습니까?";
            smithFuntion = index + 1;
            
        }
        else
            return;
    }
    public void PressedYesOrNoBtn(int index)
    {
        
        if(index == 0)
        {
            if(smithFuntion == 1)
            {

            }
            if(smithFuntion == 2)
            {
                Inventory.GetComponent<UIInventory>().items.RemoveAt(Slotindex);               
            }
            Popup.transform.gameObject.SetActive(false);
        }
        else
            Popup.transform.gameObject.SetActive(false);
    }
}
