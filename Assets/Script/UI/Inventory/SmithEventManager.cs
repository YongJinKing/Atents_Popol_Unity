using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System.Security.Cryptography;
using System;

public class SmithEventManager : MonoBehaviour
{
    private enum SmithPopupType
    {
        None,
        Repair,
        ThrowAway,
    }
    
    public Sprite[] ButtonImgSprite;

   
    public GameObject SmithUI;
    public GameObject SmithInventory;//
    public GameObject SmithTypeBtnManager;
    public GameObject SmithItemAbility;
    public GameObject SmithPairAndAwayBtnManager;
    public GameObject SmithPopupManager;
    public GameObject SmithMainImage;

    
    private List<InvenSlot> InvenSlotList = new List<InvenSlot>();//
    private List<InvenBtn> InvenBtnList = new List<InvenBtn>();//

    SmithPopupType popupType = SmithPopupType.None;
    int ChooseSlotIndex = 0;
    //private Button moveUI;


    public class InvenSlot
    {
        public GameObject gameObject;
        public bool ChooseSlot;
    }
    public class InvenBtn
    {
        public GameObject gameObject;
        public bool ChooseBtn;
    }

    private void Start() 
    {
     

        #region SmithInventoryInit
        for(int i = 0; i < SmithInventory.GetComponent<Inventory>().slots.Length; i++)
        {
            int index = i;
            InvenSlot temp = new InvenSlot();
            temp.gameObject = SmithInventory.transform.GetChild(1).GetChild(0).GetChild(i).GetChild(1).gameObject;
            temp.ChooseSlot = false;
            InvenSlotList.Add(temp);
            InvenSlotList[i].gameObject.GetComponent<Button>().onClick.AddListener(() => InvenSlotBtnChoise(index));
        }

        for(int i = 0; i < SmithTypeBtnManager.transform.childCount; i++)
        {
            int index = i;
            InvenBtn temp = new InvenBtn();
            temp.gameObject = SmithTypeBtnManager.transform.GetChild(i).gameObject;
            temp.ChooseBtn = false;
            InvenBtnList.Add(temp);
            InvenBtnList[i].gameObject.GetComponent<Button>().onClick.AddListener(()=>ChangeEqirType(index));
        }

        Button[] SmithPairAndAwayBtnList = SmithPairAndAwayBtnManager.GetComponentsInChildren<UnityEngine.UI.Button>();
        for(int i = 0; i < SmithPairAndAwayBtnManager.transform.childCount; i++)
        {
         
            int index = i;
            SmithPairAndAwayBtnList[i].onClick.AddListener(() => RepairAndAwayPopup(index));
        }

        

        #endregion
         
        #region SmithPopupInit
        Button[] SmithPopupBtnList = SmithPopupManager.GetComponentsInChildren<UnityEngine.UI.Button>();
        for(int i = 0; i < SmithPopupBtnList.Length; i++)
        {
     
            int index = i;
            SmithPopupBtnList[i].onClick.AddListener(() => PopupUiControll(index));
        }
        #endregion

        #region UIInit
        
        ItemDetailShow(false);
        #endregion
        
        //Debug.Log(SmithInventory.transform.GetChild(1).GetChild(0).GetChild(0).gameObject);
        //Debug.Log(GameObject.Find("Smith UI").transform.GetChild(0).GetChild(1).GetChild(0).GetChild(1).GetChild(0).gameObject);
        //Debug.Log(GameObject.Find("Smith UI").transform.GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject);
        //smith UI / Main Panel / BuyAndsell / itemDetail / ItemAbility / ItemAbility / Image
        
    }
    
    #region SmithSystem
    Color AlphaColorChange(int i, float Value)
    {
        Color color = InvenSlotList[i].gameObject.GetComponent<UnityEngine.UI.Image>().color;//
        color.a = Value;
        InvenSlotList[i].gameObject.GetComponent<UnityEngine.UI.Image>().color = color;
        return InvenSlotList[i].gameObject.GetComponent<UnityEngine.UI.Image>().color;
    }

    public void CleanSlots()
    {
        for(int i = 0; i < SmithInventory.GetComponent<Inventory>().slots.Length; i++)
        {
            AlphaColorChange(i, 0.0f);
            InvenSlotList[i].ChooseSlot = false;//
        }
        ItemDetailShow(false);
    }
    void InvenSlotBtnChoise(int index)
    {
        
        Slot[] SlotList = SmithInventory.GetComponent<Inventory>().slots;
        UnityEngine.UI.Image ItemInSlot = SmithInventory.transform.GetChild(1).GetChild(0).GetChild(index).gameObject.GetComponent<UnityEngine.UI.Image>();
        if(index >= SmithInventory.transform.GetComponent<Inventory>().TypeCount)
            return;
        if(SlotList[index])
        {
            ItemDetailShow(false);
            if(InvenSlotList[index].ChooseSlot) 
            {
                CleanSlots();
                return;
            }
            else
            {
                CleanSlots();
                InvenSlotList[index].ChooseSlot = true;   
            }
        
            if(InvenSlotList[index].ChooseSlot)
            {
                AlphaColorChange(index, 0.3f);
                ItemDetailShow(true);
                ItemDetailChange(SlotList[index], index);
            }
        }
        else return;
    }
    void ChangeEqirType(int index)
    {
        CleanSlots();
        if(InvenBtnList[index].ChooseBtn)
        {
            InvenBtnList[index].ChooseBtn = false;
            BtnImage(false,index, "InvenButton");
            SmithInventory.GetComponent<Inventory>().FreshSlot(0);
            return;
        }
        else
        {
            InvenBtnList[0].ChooseBtn = false;
            InvenBtnList[1].ChooseBtn = false;
            InvenBtnList[index].ChooseBtn = true;
            BtnImage(true, index, "InvenButton");
            
                
            
        }
        if(InvenBtnList[0].ChooseBtn)
        {
            SmithInventory.GetComponent<Inventory>().FreshSlot(1);
            
        }
        if(InvenBtnList[1].ChooseBtn)
        {
            SmithInventory.GetComponent<Inventory>().FreshSlot(2);
        }
    }
    void ItemDetailShow(bool isShow)
    {
        GameObject ItemAbility = SmithMainImage.transform.GetChild(0).gameObject;
        GameObject NpcTalkBollum = SmithMainImage.transform.GetChild(1).GetChild(0).gameObject;
        GameObject NpcText = SmithMainImage.transform.GetChild(1).GetChild(1).gameObject;
        ItemAbility.SetActive(isShow);
        NpcTalkBollum.SetActive(isShow);
        NpcText.SetActive(isShow);
    }
    
    void ItemDetailChange(Slot slot, int index)//슬롯의 정보를 받아서
    {

        ChooseSlotIndex = SmithInventory.GetComponent<Inventory>().itemsIndex[index];
        UnityEngine.UI.Image ItemAbilityImage = SmithItemAbility.transform.GetChild(0).GetComponent<UnityEngine.UI.Image>();
        TMP_Text ItemAbilityText = GameObject.Find("Smith UI").transform.GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetChild(1).gameObject.GetComponent<TMP_Text>();
        UnityEngine.UI.Image NpcTalkBollum = GameObject.Find("Smith UI").transform.GetChild(0).GetChild(1).GetChild(0).GetChild(1).GetChild(0).gameObject.GetComponent<UnityEngine.UI.Image>();
        TMP_Text NpcText = GameObject.Find("Smith UI").transform.GetChild(0).GetChild(1).GetChild(0).GetChild(1).GetChild(1).gameObject.GetComponent<TMP_Text>();
        ItemAbilityImage.sprite = SmithInventory.transform.GetChild(1).GetChild(0).GetChild(index).GetChild(0).gameObject.GetComponent<UnityEngine.UI.Image>().sprite;
        string EqirType(RiggingType Type)
        {
            string Typename = "";
            if(Type == RiggingType.Weapon)
                Typename = "공격력 : ";
            if(Type == RiggingType.Armor)
                Typename = "체력 : ";
            return Typename;
        }
         ItemAbilityText.text = 
        "이름 : " + slot.InvenDetailName + "\n\n" + EqirType(slot.InvenDetailRiggingType)
         + slot.InvenDetailValue + "\n\n" + "내구도 : " + slot.InvenDetailDurAbility;
        NpcText.text = slot.InvenDetailSmithTalk;
    }
    void RepairAndAwayPopup(int index)
    {
        bool isSelected = false;
        foreach(InvenSlot i in InvenSlotList)
        {
            if(i.ChooseSlot)
            {
                isSelected = true;
                SmithUI.transform.GetChild(1).gameObject.SetActive(true);// 팝업 on
            }
            
        }

        if(!isSelected)
            {
                return;
            }
        if(index == 0)
        {
            SmithPopupManager.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Text>().text = 
            "아이템을 수리하시겠습니까?";
            popupType = SmithPopupType.Repair;
        }
        if(index == 1)
        {
            SmithPopupManager.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Text>().text = 
            "아이템을 폐기하시겠습니까?";
            popupType = SmithPopupType.ThrowAway;
        }
    }
    #endregion 
    #region ButtonImage
    void BtnImage(bool IsClicked, int index, string UiName)
    {

        #region SmithUI
        if(UiName == "InvenButton")
        {
            for(int i = 0; i < SmithTypeBtnManager.transform.childCount; i++)
            {
                SmithTypeBtnManager.transform.GetChild(i).gameObject.GetComponent<UnityEngine.UI.Image>().sprite = ButtonImgSprite[0];
                SmithTypeBtnManager.transform.GetChild(i).GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 10, 0);
                
            }
            
            if(IsClicked)
            {
                SmithTypeBtnManager.transform.GetChild(index).gameObject.GetComponent<UnityEngine.UI.Image>().sprite = ButtonImgSprite[1];
                SmithTypeBtnManager.transform.GetChild(index).GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -5, 0);
            }
        }
        #endregion
    }
    #endregion
    #region PopupControll
    void PopupUiControll(int index)
    {
        Debug.Log(ChooseSlotIndex);
        if(index == 0)//yes
        {
            
            if(popupType == SmithPopupType.Repair)
            {
                SmithInventory.GetComponent<Inventory>().items[ChooseSlotIndex].durAbility = 100;
                CleanSlots();
                PopupClose();
                if(InvenBtnList[0].ChooseBtn)
                {
                    SmithInventory.GetComponent<Inventory>().FreshSlot(1);
                }
                else if(InvenBtnList[1].ChooseBtn)
                {
                    SmithInventory.GetComponent<Inventory>().FreshSlot(2);
                }else
                {
                    SmithInventory.GetComponent<Inventory>().FreshSlot(0);
                }
            }
            if(popupType == SmithPopupType.ThrowAway)
            {
                SmithInventory.GetComponent<Inventory>().items.RemoveAt(ChooseSlotIndex);
                CleanSlots();
                PopupClose();
                if(InvenBtnList[0].ChooseBtn)
                {
                    SmithInventory.GetComponent<Inventory>().FreshSlot(1);
                }
                else if(InvenBtnList[1].ChooseBtn)
                {
                    SmithInventory.GetComponent<Inventory>().FreshSlot(2);
                }else
                {
                    SmithInventory.GetComponent<Inventory>().FreshSlot(0);
                }
            }
        }
        if(index == 1)//no
        {
            /* Item itemAdd = new Item();
            itemAdd = SmithInventory.GetComponent<Inventory>().items[ChooseSlotIndex];
            SmithInventory.GetComponent<Inventory>().AddItem(itemAdd); */
            PopupClose();
        }
    }

    void PopupClose()
    {
        popupType = SmithPopupType.None;
        SmithUI.transform.GetChild(1).gameObject.SetActive(false);// 팝업 off
    }

    #endregion
}


