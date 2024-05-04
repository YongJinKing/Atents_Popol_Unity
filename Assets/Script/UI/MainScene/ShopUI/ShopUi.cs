using System.Collections;

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ShopUi : MonoBehaviour
{
    public GameObject Contents;
    public GameObject MoneyPopup;
    public GameObject BuyPopup;
    public GameObject DescPopup;
    public GameObject GridLine;
    public GameObject RiggingItem;
    public GameObject ManyItemItem;
    public UnityEvent<int> AddItem;
    public UnityEvent<bool , int> EventRiggingPopup;
    string ItemRiggingStr = "";
    int InstanceCount = 0;
    int ItemPrice;
    int SelectItem;
    
    Coroutine CorSlotHL;
    // Start is called before the first frame update
    void Start()
    {
        ItemDataManager.GetInstance().InvenItemLoadDatas();
        BuyPopup.gameObject.SetActive(false);
        MoneyPopup.gameObject.SetActive(false);
        foreach(KeyValuePair<int, ItemData> item in ItemDataManager.GetInstance().dicItemDatas)
        {   
            bool Found = ItemDataManager.GetInstance().dicResouseTable[item.Key].ImageResourceName.Contains("Begginer");
            if(!Found)
            {
                Instantiate(Resources.Load("UI/ShopUi/ItemSlot"),Contents.transform);
                Contents.transform.GetChild(InstanceCount).Find("ItemDetail").GetComponent<UIItem>().Init(item.Key);
                Contents.transform.GetChild(InstanceCount).Find("ItemDetail").Find("ItemName").GetComponent<TMP_Text>().text =
                "이름 :\n"+
                Contents.transform.GetChild(InstanceCount).Find("ItemDetail").GetComponent<UIItem>().ItemName;
                if(item.Value.Inven_riggingType == 0)
                    ItemRiggingStr = "공격력 : ";
                if(item.Value.Inven_riggingType == 1)
                    ItemRiggingStr = "체력 : ";
                Contents.transform.GetChild(InstanceCount).Find("ItemDetail").Find("ItemValue").GetComponent<TMP_Text>().text =
                ItemRiggingStr +
                Contents.transform.GetChild(InstanceCount).Find("ItemDetail").GetComponent<UIItem>().ItemValue.ToString();
            
                Contents.transform.GetChild(InstanceCount).Find("ItemDetail").Find("ItemType").GetComponent<TMP_Text>().text =
                "종류 :" +
                ItemTypeIntToString.IntToStringUIDesc(Contents.transform.GetChild(InstanceCount).Find("ItemDetail").GetComponent<UIItem>().WeaponType);

                
                Contents.transform.GetChild(InstanceCount).Find("ItemDetail").Find("Gold").Find("Text (TMP)").GetComponent<TMP_Text>().text =
                UnitCalculate.GetInstance().Calculate(Contents.transform.GetChild(InstanceCount).Find("ItemDetail").GetComponent<UIItem>().ItemPrice);
                InstanceCount++;
            }
            Button[] BuyBtnList = Contents.GetComponentsInChildren<Button>();
            for(int i = 0; i < BuyBtnList.Length; i++)
            {
                int index = i;
                BuyBtnList[i].onClick.AddListener(() => PressedBuyBtn(index));
            }    
        }
    }

    public void PressedBuyBtn(int index)
    {
        int itemCount = 0;
        for(int i = 0; i < GridLine.transform.childCount; i++)
        {
            if(GridLine.transform.GetChild(i).GetComponent<UIItem>().id > 0)
                itemCount++;
        }
        if(itemCount < GridLine.transform.childCount)
        {
            ItemPrice = Contents.transform.GetChild(index).GetChild(0).GetComponent<UIItem>().ItemPrice;
            if(DataManager.instance.playerData.PlayerGold >= ItemPrice)
            {
                BuyPopup.gameObject.SetActive(true);
                SelectItem = index;
            }
            else
            {
                StartCoroutine(OnCorPopup(MoneyPopup));
            }
        }
        else
        {
            StartCoroutine(OnCorPopup(ManyItemItem));
        }
        
    }
    public void SortRiggingType(int index)
    {
        for(int i = 0; i <  Contents.transform.childCount; i++)
        {
                Contents.transform.GetChild(i).gameObject.SetActive(false);
        }
        if(index == 0)
        {
            for(int i = 0; i <  Contents.transform.childCount; i++)
            {
                Contents.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
        if(index == 1)
        {
            for(int i = 0; i <  Contents.transform.childCount; i++)
            {
                if(Contents.transform.GetChild(i).Find("ItemDetail").GetComponent<UIItem>().ItemRigging == 0)
                    Contents.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
        if(index == 2)
        {
            for(int i = 0; i <  Contents.transform.childCount; i++)
            {
                if(Contents.transform.GetChild(i).Find("ItemDetail").GetComponent<UIItem>().ItemRigging == 1)
                    Contents.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
    IEnumerator OnCorPopup(GameObject popup)
    {
        popup.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.8f);
        popup.gameObject.SetActive(false);
       
    }
    public void BuyPopupYesOrNo(int index)
    {
        if(index == 0)
        {
            DataManager.instance.playerData.PlayerGold -= ItemPrice;
            AddItem?.Invoke(Contents.transform.GetChild(SelectItem).Find("ItemDetail").GetComponent<UIItem>().id);
        }
        
        BuyPopup.gameObject.SetActive(false);
    }

    

    public string RiggingTypeToString(int index)
    {
        string Rtstring = "";
        //0 : 무기 1 : 방어구
        if(index == 0)
            Rtstring = "공격력 : ";
        if(index == 1)
            Rtstring = "체력 : ";
       
        return Rtstring;
    }
    public void OnInvenHighLite(int index, bool OnCheck)
    {
        if(GridLine.transform.GetChild(index).GetComponent<UIItem>().id > 0)
        {
            GridLine.transform.GetChild(index).Find("Button").gameObject.SetActive(OnCheck);
            if(OnCheck)
            {
                CorSlotHL = StartCoroutine(OnCorInvenSlotHL(0,index));
            }  
            else
            {
                if(CorSlotHL != null)
                {
                    StopCoroutine(CorSlotHL);
                    CorSlotHL = null;
                }
                    
                DescPopup.transform.gameObject.SetActive(false);
                
            }
            EventRiggingPopup?.Invoke(OnCheck, index);
        }
    }
    public void OnRiggingHighLite(int index, bool OnCheck)
    {
        RiggingItem.transform.GetChild(index).Find("Button").gameObject.SetActive(OnCheck);
        if(OnCheck)
        {
            CorSlotHL = StartCoroutine(OnCorInvenSlotHL(1,index));
        }  
        else
        {
            if(CorSlotHL != null)
            {
                StopCoroutine(CorSlotHL);
                CorSlotHL = null;
            }
            DescPopup.transform.gameObject.SetActive(false);
         }
    }

    IEnumerator OnCorInvenSlotHL(int type,int index)//0 : grid 1: rigging
    {
        UIItem go = gameObject.AddComponent<UIItem>();
        if(type == 0)
        {
            go = GridLine.transform.GetChild(index).GetComponent<UIItem>();
            if(GridLine.transform.GetChild(index).GetComponent<RectTransform>().anchoredPosition.x <= 300)
            {
                transform.GetChild(1).Find("DescPopup").GetComponent<RectTransform>().anchoredPosition =
                GridLine.transform.GetChild(index).GetComponent<RectTransform>().anchoredPosition + new Vector2(290 , -145);
            }
            else
            {
                transform.GetChild(1).Find("DescPopup").GetComponent<RectTransform>().anchoredPosition =
                GridLine.transform.GetChild(index).GetComponent<RectTransform>().anchoredPosition + new Vector2(-310 , -145);
            }
        } 
        if(type == 1)
        {
            go = RiggingItem.transform.GetChild(index).GetComponent<UIItem>();
            transform.GetChild(1).Find("DescPopup").GetComponent<RectTransform>().anchoredPosition =
            RiggingItem.transform.GetChild(index).GetComponent<RectTransform>().anchoredPosition+ new Vector2(95 , 11);
        }
        yield return new WaitForSeconds(0.8f);
        DescPopup.transform.gameObject.SetActive(true);
        transform.GetChild(1).Find("DescPopup").gameObject.SetActive(true);
        transform.GetChild(1).Find("DescPopup").Find("Paper").Find("ImgBg").Find("ItemImage").GetComponent<Image>().sprite
        = go.icon.sprite;
        transform.GetChild(1).Find("DescPopup").Find("Paper").Find("ItemName").GetComponent<TMP_Text>().text
        = go.ItemName;
        transform.GetChild(1).Find("DescPopup").Find("Paper").Find("ItemType").GetComponent<TMP_Text>().text
        = ItemTypeIntToString.IntToStringUIDesc(go.WeaponType);
        transform.GetChild(1).Find("DescPopup").Find("Paper").Find("ItemValue").GetComponent<TMP_Text>().text
        = RiggingTypeToString(go.ItemRigging) + go.ItemValue.ToString();
        transform.GetChild(1).Find("DescPopup").Find("Paper").Find("ItemDesc").GetComponent<TMP_Text>().text
        = go.ItemDesc;
         transform.GetChild(1).Find("DescPopup").Find("Paper").Find("DurationDesc").Find("Value").GetComponent<TMP_Text>().text
        = go.ItemDuration.ToString();
        
        
        
        
    }
    

}
