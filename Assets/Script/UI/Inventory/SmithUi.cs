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
    
    public GameObject FuntionPopup;
    public GameObject CantPopup;
    public GameObject SmithGridLine;
    public GameObject ItemDetail;
    public GameObject ItemAbility;
    public GameObject RiggigItem;
    public UnityEvent<int> InvenItemRepair;
    public UnityEvent<int> RiggingItemRepair;

    public UnityEvent<int> BtnAct2;
    bool RiggingItemSelected= false;
    bool SlotSelected= false;
    int SlotIndex = 0;
    int RiggingIndex = 0;
    int smithFuntion;
    int PopupType = 0;
    Button[] SlotBtnFunctionList ;
    int SlotPrevIndex = -1;
    int RiggingItemPrevIndex = -1;
    
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
    public void OnHighLite(int index, bool OnCheck)
    {
        if(OnCheck)
        {
            if(SmithGridLine.transform.GetChild(index).GetComponent<UIItem>().id > 0)
                SmithGridLine.transform.GetChild(index).Find("HLImage").gameObject.SetActive(true);
        }
        else
        {
            if(SmithGridLine.transform.GetChild(index).GetComponent<UIItem>().id > 0)
                SmithGridLine.transform.GetChild(index).Find("HLImage").gameObject.SetActive(false);
        }
    }
    public void ChooseSlot(int index)
    {
        int id = SmithGridLine.transform.GetChild(index).GetComponent<UIItem>().id;
        if(SlotPrevIndex == index)
        {
            CleanSlot(-1);
            SlotSelected = false;
            ItemDetailShow();
            return;
        }
        else
        {
            if(id > 0)
            {
                SlotIndex = index;
                CleanRiggingItem(-1);
                RiggingItemSelected = false;
                CleanSlot(index);
                SlotImgColorChange(0.3f, index);
                SlotSelected = true;
                ItemDetailShow(0, index);
            }
            else
                return;
        }
    }
    public void ChooseRiggingItem(int index)
    {
        int id = RiggigItem.transform.GetChild(index).GetComponent<UIItem>().id;
        if(RiggingItemPrevIndex == index)
        {
            CleanRiggingItem(-1);
            RiggingItemSelected = false;
            ItemDetailShow();
            return;
        }
        else
        {
            if(id > 0)
            {
                RiggingIndex = index;
                CleanSlot(-1);
                CleanRiggingItem(index);
                RiggingItemImgColorChange(0.3f, index);
                SlotSelected = false;
                RiggingItemSelected = true;
                ItemDetailShow(1, index);
            }
        }
    }
    public void ItemDetailShow()
    {
        ItemDetail.transform.GetChild(0).gameObject.SetActive(false);
        ItemDetail.transform.GetChild(1).gameObject.SetActive(false);
    }
    void ItemDetailShow(int type, int index)
    {
        ItemDetail.transform.GetChild(0).gameObject.SetActive(true);
        ItemDetail.transform.GetChild(1).gameObject.SetActive(true);
        UIItem DetailItem = gameObject.AddComponent<UIItem>();
        if(type == 0)
            DetailItem = SmithGridLine.transform.GetChild(index).GetComponent<UIItem>();
        if(type == 1)
            DetailItem = RiggigItem.transform.GetChild(index).GetComponent<UIItem>();
        ItemAbility.transform.GetChild(0).GetComponent<Image>().sprite = DetailItem.icon.sprite;
        ItemAbility.transform.GetChild(1).GetComponent<TMP_Text>().text = "이름 : " + DetailItem.ItemName;
        ItemAbility.transform.GetChild(2).GetComponent<TMP_Text>().text = "공격력 : " + DetailItem.ItemValue.ToString();
        ItemAbility.transform.GetChild(3).GetComponent<TMP_Text>().text = "내구도 : " + DetailItem.ItemDuration.ToString();
        ItemAbility.transform.GetChild(4).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = 
        DetailItem.ItemSmith;
        
    }


    void CleanSlot(int PrevIdxSetting)
    {
        for(int i = 0; i < SmithGridLine.transform.childCount; i++)
        {
            SlotImgColorChange(0.0f, i);
        }
        SlotPrevIndex = PrevIdxSetting;
    }
    void CleanRiggingItem(int PrevIdxSetting)
    {
        for(int i = 0; i < RiggigItem.transform.childCount; i++)
        {
            RiggingItemImgColorChange(0.0f, i);
        }
        RiggingItemPrevIndex = PrevIdxSetting;
    }
    public void CleanSlotAndRiggingItem()
    {
        for(int i = 0; i < SmithGridLine.transform.childCount; i++)
        {
            SlotImgColorChange(0.0f, i);
        }
        for(int i = 0; i < RiggigItem.transform.childCount; i++)
        {
            RiggingItemImgColorChange(0.0f, i);
        }
        RiggingItemPrevIndex = -1;
        SlotPrevIndex = -1;
    }

    void SlotImgColorChange(float Value, int index)
    {
        Color color = SmithGridLine.transform.GetChild(index).Find("MainButton").GetComponent<Image>().color;
        color.a = Value;
        SmithGridLine.transform.GetChild(index).Find("MainButton").GetComponent<Image>().color = color;
    }
    void RiggingItemImgColorChange(float Value, int index)
    {
        Color color = RiggigItem.transform.GetChild(index).Find("Button").GetComponent<Image>().color;
        color.a = Value;
        RiggigItem.transform.GetChild(index).Find("Button").GetComponent<Image>().color = color;
    }
    
    public void PressedPopupBtn(int index)
    {
        PopupType = 0;
        String ModeText = "";
        if(SlotSelected)
            PopupType = 1;
        if(RiggingItemSelected)
            PopupType = 2;
        if(PopupType > 0)
        {   
            if(PopupType == 1)
            {
                FuntionPopup.transform.gameObject.SetActive(true);
                if(index == 0)
                    ModeText = "수리";
                else if(index == 1)
                    ModeText = "폐기";
                FuntionPopup.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text =
                $"아이템을 {ModeText}하시겠습니까?";
                smithFuntion = index + 1;
            }
            if(PopupType == 2)
            {
                
                if(index == 0)
                {
                    FuntionPopup.transform.gameObject.SetActive(true);
                    ModeText = "수리";
                }
                else if(index == 1)
                {
                    StartCoroutine(CantDesPopup());
                }
                FuntionPopup.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text =
                $"아이템을 {ModeText}하시겠습니까?";
                smithFuntion = index + 1;
            }
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
                if(PopupType == 1)
                    InvenItemRepair?.Invoke(SlotIndex);
                if(PopupType == 2)
                    RiggingItemRepair?.Invoke(RiggingIndex);
            }
            if(smithFuntion == 2)
            {
                BtnAct2?.Invoke(SlotIndex);                
            }
            FuntionPopup.transform.gameObject.SetActive(false);
            ItemDetailShow();
            CleanSlotAndRiggingItem();
        }
        else
            FuntionPopup.transform.gameObject.SetActive(false);
    }
    IEnumerator CantDesPopup()
    {
        CantPopup.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.8f);
        CantPopup.gameObject.SetActive(false);
    }
}
