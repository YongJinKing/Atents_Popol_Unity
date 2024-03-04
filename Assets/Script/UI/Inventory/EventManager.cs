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

public class EventManager : MonoBehaviour
{
    private enum PopupType
    {
        None,
        Repair,
        ThrowAway,
    }
    
    public Sprite[] ButtonImgSprite;

    public GameObject SmithInventory;//
    public GameObject gameCanvas;//게임 
    public GameObject MainUI;//메인 UI
    public GameObject UserPanel;

    public GameObject SmithTypeBtnManager;
    public GameObject SmithItemAbility;
    public GameObject SmithPairAndAwayBtnManager;
    public GameObject PopupManager;

    
    private List<InvenSlot> InvenSlotList = new List<InvenSlot>();//
    private List<InvenBtn> InvenBtnList = new List<InvenBtn>();//
    PopupType popupType = PopupType.None;
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
        #region MainUIInit

        Button[] MainUIButtonList = MainUI.GetComponentsInChildren<UnityEngine.UI.Button>();//MainUI버튼
        for(int i = 0; i < MainUIButtonList.Length; i++)
        {
            int index = i;
            MainUIButtonList[i].onClick.AddListener(() => MainUiControll(index, MainUIButtonList.Length));
        }
        
        #endregion   
        #region User_PanelInit


        Button[] UserPanelBtnList = UserPanel.GetComponentsInChildren<UnityEngine.UI.Button>();
        for(int j = 0; j < UserPanelBtnList.Length; j++)
        {
            Debug.Log(UserPanelBtnList[0]);
            int index = j;
            UserPanelBtnList[j].onClick.AddListener(() => UserPanelControll(index, gameCanvas.transform.childCount));
        }


        #endregion
        
        Button[] PopupBtnList = PopupManager.GetComponentsInChildren<UnityEngine.UI.Button>();
        for(int i = 0; i < PopupBtnList.Length; i++)
        {
     
            int index = i;
            PopupBtnList[i].onClick.AddListener(() => PopupUiControll(index));
        }

        #region UIInit
        gameCanvas.transform.GetChild(2).gameObject.SetActive(true); // 메인 on
        gameCanvas.transform.GetChild(3).gameObject.SetActive(false); // 쇼핑 off
        gameCanvas.transform.GetChild(4).gameObject.SetActive(false); // 수리 off
        gameCanvas.transform.GetChild(5).gameObject.SetActive(false); // 경기 off
        gameCanvas.transform.GetChild(6).gameObject.SetActive(false); // 팝업 off
        gameCanvas.transform.GetChild(1).GetChild(0).gameObject.SetActive(false); //x버튼 off
        #endregion
        
        //Debug.Log(SmithInventory.transform.GetChild(1).GetChild(0).GetChild(0).gameObject);
        //Debug.Log(GameObject.Find("Smith UI").transform.GetChild(0).GetChild(1).GetChild(0).GetChild(1).GetChild(0).gameObject);
        //Debug.Log(GameObject.Find("Smith UI").transform.GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject);
        //smith UI / Main Panel / BuyAndsell / itemDetail / ItemAbility / ItemAbility / Image
        
    }
    #region MainUI
    
    void ChangeMainUI(int UiLength, bool isShow)
    {
        for(int i = 0; i < UiLength-2; i++)
        {
            gameCanvas.transform.GetChild(i+2).gameObject.SetActive(false);
        }
        gameCanvas.transform.GetChild(1).GetChild(0).gameObject.SetActive(isShow);
        ItemDetailShow(false);
        
    }

    void MainUiControll(int index, int UiLength)
    {
        ChangeMainUI(UiLength, true);
        gameCanvas.transform.GetChild(index+3).gameObject.SetActive(true);
    }
    #endregion
    #region SmithSystem
    Color AlphaColorChange(int i, float Value)
    {
        Color color = InvenSlotList[i].gameObject.GetComponent<UnityEngine.UI.Image>().color;//
        color.a = Value;
        InvenSlotList[i].gameObject.GetComponent<UnityEngine.UI.Image>().color = color;
        return InvenSlotList[i].gameObject.GetComponent<UnityEngine.UI.Image>().color;
    }

    void CleanSlots()
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
        GameObject ItemAbility = gameCanvas.transform.GetChild(4).GetChild(0).GetChild(1).GetChild(0).GetChild(0).gameObject;
        GameObject NpcTalkBollum = gameCanvas.transform.GetChild(4).GetChild(0).GetChild(1).GetChild(0).GetChild(1).GetChild(0).gameObject;
        GameObject NpcText = gameCanvas.transform.GetChild(4).GetChild(0).GetChild(1).GetChild(0).GetChild(1).GetChild(1).gameObject;
        ItemAbility.SetActive(isShow);
        NpcTalkBollum.SetActive(isShow);
        NpcText.SetActive(isShow);
    }
    
    void ItemDetailChange(Slot slot, int index)//슬롯의 정보를 받아서
    {

        
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
        gameCanvas.transform.GetChild(6).gameObject.SetActive(true); // 팝업 on
        if(index == 0)
        {
            PopupManager.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Text>().text = 
            "아이템을 수리하시겠습니까?";
            popupType = PopupType.Repair;
        }
        if(index == 1)
        {
            PopupManager.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Text>().text = 
            "아이템을 폐기하시겠습니까?";
            popupType = PopupType.ThrowAway;
        }
    }
    #endregion 
    #region UserPanelUI
    void UserPanelControll(int index, int Length)
    {
        if(index == 0)
        {
            CleanSlots();
            ChangeMainUI(Length, false);
            gameCanvas.transform.GetChild(2).gameObject.SetActive(true); //
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
    
        if(index == 0)//yes
        {
            if(popupType == PopupType.Repair)
            {
                
            }
            if(popupType == PopupType.ThrowAway)
            {
                
            }
        }
        if(index == 1)//no
        {
            popupType = PopupType.None;
            gameCanvas.transform.GetChild(6).gameObject.SetActive(false); // 팝업 on
        }
    }

    #endregion
}


