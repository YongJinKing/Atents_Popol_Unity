using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SmithUi : MonoBehaviour
{
    
    public GameObject Popup;
    public GameObject PopupBtnManager;
    
    public GameObject SmithGridLine;
    public GameObject UserGridLine;
    public GameObject ItemDetail;
    public GameObject ItemAbility;

    public UnityEvent BtnAct1;
    public UnityEvent<int> BtnAct2;
    bool SlotSelected= false;
    int SlotIndex = 0;
    int smithFuntion;
    Button[] SlotBtnFunctionList ;
    int PrevIndex = -1;
    
    private void Start() 
    {
        SlotBtnFunctionList = SmithGridLine.GetComponentsInChildren<Button>();
        for(int i = 0; i < SlotBtnFunctionList.Length; i++)
        {
            int index = i;
            SlotBtnFunctionList[i].onClick.AddListener(() => ChooseSlot(index));
        }
        ItemDetail.transform.GetChild(0).gameObject.SetActive(false);
        ItemDetail.transform.GetChild(1).gameObject.SetActive(false);
    }
    public void ChooseSlot(int index)
    {
        int id = SmithGridLine.transform.GetChild(index).GetComponent<UIItem>().id;
        if(PrevIndex == index)
        {
            CleanSlot(-1);
            SlotSelected = false;
            ItemDetailShow(SlotSelected, index);
            return;
        }
        else
        {
            if(id > 0)
            {
                SlotIndex = index;
                CleanSlot(index);
                ImgColorChange(0.3f, index);
                SlotSelected = true;
                ItemDetailShow(SlotSelected, index);
            }
            else
                return;
        }
        
    }
    public void ItemDetailShow()
    {
        ItemDetail.transform.GetChild(0).gameObject.SetActive(false);
        ItemDetail.transform.GetChild(1).gameObject.SetActive(false);
    }
    void ItemDetailShow(bool ShowCheck, int index)
    {
        ItemDetail.transform.GetChild(0).gameObject.SetActive(ShowCheck);
        ItemDetail.transform.GetChild(1).gameObject.SetActive(ShowCheck);
        if(!ShowCheck)  return;
        else
        {
            UIItem DetailItem = SmithGridLine.transform.GetChild(index).GetComponent<UIItem>();
            ItemAbility.transform.GetChild(0).GetComponent<Image>().sprite = DetailItem.icon.sprite;
            ItemAbility.transform.GetChild(1).GetComponent<TMP_Text>().text = "이름 : " + DetailItem.ItemName;
            ItemAbility.transform.GetChild(2).GetComponent<TMP_Text>().text = "공격력 : " + DetailItem.ItemValue.ToString();
            ItemAbility.transform.GetChild(3).GetComponent<TMP_Text>().text = "내구도 : " + DetailItem.ItemDuration.ToString();
            ItemAbility.transform.GetChild(4).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = 
            DetailItem.ItemSmith;
        }
    }


    void CleanSlot(int PrevIdxSetting)
    {
        for(int i = 0; i < SmithGridLine.transform.childCount; i++)
        {
            ImgColorChange(0.0f, i);
        }
        PrevIndex = PrevIdxSetting;
    }
    public void CleanSlot()
    {
        for(int i = 0; i < SmithGridLine.transform.childCount; i++)
        {
            ImgColorChange(0.0f, i);
        }
        PrevIndex = -1;
    }

    void ImgColorChange(float Value, int index)
    {
        Color color = SmithGridLine.transform.GetChild(index).GetChild(2).GetComponent<Image>().color;
        color.a = Value;
        SmithGridLine.transform.GetChild(index).GetChild(2).GetComponent<Image>().color = color;
    }
    
    public void PressedPopupBtn(int index)
    {
        
        if(SlotSelected)
        {   
            String ModeText = "";
            Popup.transform.gameObject.SetActive(true);
            if(index == 0)
                ModeText = "수리";
            else if(index == 1)
                ModeText = "폐기";
            Popup.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text =
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
                SmithGridLine.transform.GetChild(SlotIndex).GetComponent<UIItem>().ItemDuration = 100;
            }
            if(smithFuntion == 2)
            {
                BtnAct2?.Invoke(SlotIndex);                
            }
            Popup.transform.gameObject.SetActive(false);
            ItemDetailShow();
        }
        else
            Popup.transform.gameObject.SetActive(false);
    }
}
