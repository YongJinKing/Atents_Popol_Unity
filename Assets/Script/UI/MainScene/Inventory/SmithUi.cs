using System;
using System.Collections;

using TMPro;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class SmithUi : MonoBehaviour
{
    
    
    
    public GameObject DestroyPopup;
    public GameObject RepairPopup;
    public GameObject CantPopup;
    public GameObject NomoneyPopup;
    public GameObject CantRepair;
    public GameObject SmithGridLine;
    public GameObject ItemDetail;
    public GameObject ItemAbility;
    public GameObject RiggigItem;
    public UnityEvent<int> InvenItemRepair;
    public UnityEvent<int> RiggingItemRepair;
    public UnityEvent<bool, int> EventRiggingPopup;

    public UnityEvent<int> BtnAct2;
    bool RiggingItemSelected= false;
    bool SlotSelected= false;
    int PayRepairMoney;
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
        
            
        if(SmithGridLine.transform.GetChild(index).GetComponent<UIItem>().id > 0)
        {
            SmithGridLine.transform.GetChild(index).Find("HLImage").gameObject.SetActive(OnCheck);
            EventRiggingPopup?.Invoke(OnCheck,index);
        }
            
        
        
    }
    public void OnRiggingHighLite(int index, bool OnCheck)
    {
        
        if(RiggigItem.transform.GetChild(index).GetComponent<UIItem>().id > 0)
        {
            RiggigItem.transform.GetChild(index).Find("HLImage").gameObject.SetActive(OnCheck);
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
        string WeaponType = "";
        ItemDetail.transform.GetChild(0).gameObject.SetActive(true);
        ItemDetail.transform.GetChild(1).gameObject.SetActive(true);
        UIItem DetailItem = gameObject.AddComponent<UIItem>();
        if(type == 0)
            DetailItem = SmithGridLine.transform.GetChild(index).GetComponent<UIItem>();
        if(type == 1)
            DetailItem = RiggigItem.transform.GetChild(index).GetComponent<UIItem>();
        ItemAbility.transform.GetChild(0).GetComponent<Image>().sprite = DetailItem.icon.sprite;
        ItemAbility.transform.GetChild(1).GetComponent<TMP_Text>().text = "이름 : " + DetailItem.ItemName;
        if(DetailItem.ItemRigging == 0)
             WeaponType = "공격력 : ";
        else
            WeaponType = "체력 : ";
        ItemAbility.transform.GetChild(2).GetComponent<TMP_Text>().text = WeaponType + DetailItem.ItemValue.ToString();
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
        PayRepairMoney = 0;
        
        if(SlotSelected)
            PopupType = 1;
        if(RiggingItemSelected)
            PopupType = 2;
        if(PopupType > 0)
        {   
            if(PopupType == 1)
            {
                if(index == 0)
                {
                    var go = SmithGridLine.transform.GetChild(SlotIndex).GetComponent<UIItem>();
                    PayRepairMoney = RepairCalculate(go.ItemPrice, go.ItemDuration);
                    if(go.ItemDuration != 100)
                    {
                        if(DataManager.instance.playerData.PlayerGold >= RepairCalculate(go.ItemPrice, go.ItemDuration))
                        {
                            RepairPopup.transform.gameObject.SetActive(true);
                            RepairPopup.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = 
                            UnitCalculate.GetInstance().Calculate(RepairCalculate(go.ItemPrice, go.ItemDuration)) + "Gold";
                        }
                        else
                        {
                            StartCoroutine(CantDesPopup(NomoneyPopup));
                            NomoneyPopup.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<TMP_Text>().text =
                            UnitCalculate.GetInstance().Calculate(RepairCalculate(go.ItemPrice, go.ItemDuration)) + "만큼 필요합니다.";
                        }
                    }
                    else
                    {
                        StartCoroutine(CantDesPopup(CantRepair));
                    }
                    
                    
                    //수리할 아이템의 가격 가져온다음 80퍼로 만들고, 수리해야될 내구도를 100분률로 나타내어 마이너스 한 돈을 표현시키기
                }
                else if(index == 1)
                {
                    DestroyPopup.transform.gameObject.SetActive(true);
                }
                    
                
                smithFuntion = index + 1;
                
            }
            if(PopupType == 2)
            {
                
                if(index == 0)
                {
                    var go = RiggigItem.transform.GetChild(RiggingIndex).GetComponent<UIItem>();
                    PayRepairMoney = RepairCalculate(go.ItemPrice, go.ItemDuration);
                    if(go.ItemDuration != 100)
                    {
                        if(DataManager.instance.playerData.PlayerGold >= RepairCalculate(go.ItemPrice, go.ItemDuration))
                        {
                            RepairPopup.transform.gameObject.SetActive(true);
                            RepairPopup.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = 
                            RepairCalculate(go.ItemPrice, go.ItemDuration).ToString() + "Gold";
                        }
                        else
                        {
                            StartCoroutine(CantDesPopup(NomoneyPopup));
                            NomoneyPopup.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<TMP_Text>().text =
                            UnitCalculate.GetInstance().Calculate(RepairCalculate(go.ItemPrice, go.ItemDuration)) + "만큼 필요합니다.";
                        
                        }
                    }
                    else
                    {
                        StartCoroutine(CantDesPopup(CantRepair));
                    }
                    //수리할 아이템의 가격 가져온다음 80퍼로 만들고, 수리해야될 내구도를 100분률로 나타내어 마이너스 한 돈을 표현시키기
                }
                else if(index == 1)
                {
                    StartCoroutine(CantDesPopup(CantPopup));
                }
                smithFuntion = index + 1;
            }
        }
        else
            return;
    }
    public int RepairCalculate(int ItemPrice, int ItemDuration)
    {
        float TempPrice;
        int RepairPrice;
        TempPrice = ItemPrice * 0.8f;
        RepairPrice = (int)(TempPrice - ((TempPrice * ItemDuration)/ 100.0f));
        return RepairPrice;
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
                DataManager.instance.playerData.PlayerGold -= PayRepairMoney;
            }
            if(smithFuntion == 2)
            {
                BtnAct2?.Invoke(SlotIndex);                
            }
            ItemDetailShow();
            CleanSlotAndRiggingItem();
        }
        DestroyPopup.transform.gameObject.SetActive(false);
        RepairPopup.transform.gameObject.SetActive(false);
        
            
    }
    IEnumerator CantDesPopup(GameObject ReferencePopup)
    {
        ReferencePopup.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.8f);
        ReferencePopup.gameObject.SetActive(false);
    }
}
