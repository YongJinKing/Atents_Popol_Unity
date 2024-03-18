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
    
    public void ChooseSlot(int id)
    {
        Slotindex = 0;
        for(int i = 0; i < GridLine.transform.childCount; i++)
        {
            if(GridLine.transform.GetChild(i).GetChild(0).GetComponent<UIItem>().id
            == id)
            {
                Slotindex = i;
                break;
            }
            Slotindex = -1;
        }
        var go = GridLine.transform.GetChild(Slotindex).GetChild(0);
        if(Slotindex == -1)
            return;
        if(go.GetComponent<UIItem>().isSelected) 
        {
            go.GetComponent<UIItem>().isSelected = false;
            go.GetChild(2).
            GetComponent<Image>().color =
            AlphaColorChange(0.0f, go.GetChild(2).
            GetComponent<Image>().color);
        }
        else
            go.GetComponent<UIItem>().isSelected = true;
        if(go.GetComponent<UIItem>().isSelected)
        {
            go.GetChild(2).
            GetComponent<Image>().color =
            AlphaColorChange(0.3f, go.GetChild(2).
            GetComponent<Image>().color);
        }
    }
    Color AlphaColorChange(float Value, Color Objcolor)
    {
        Color color = Objcolor;
        color.a = Value;
        Objcolor = color;
        return Objcolor;
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
            /* Popup.transform.GetChild(0).GetComponent<TMP_Text>().text =
            $"아이템을 {ModeText}하시겠습니까?"; */
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
