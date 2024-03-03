using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class EventManager : MonoBehaviour
{
    
    public GameObject SmithInventory;//
    public GameObject gameCanvas;//게임 
    public GameObject MainUI;//메인 UI
    public GameObject UserPanel;

    public GameObject InvenBtnManager;
    public GameObject ItemAbility;
    
    private List<InvenSlot> InvenSlotList = new List<InvenSlot>();//
    private List<InvenBtn> InvenBtnList = new List<InvenBtn>();//

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

        for(int i = 0; i < InvenBtnManager.transform.childCount; i++)
        {
            int index = i;
            InvenBtn temp = new InvenBtn();
            temp.gameObject = InvenBtnManager.transform.GetChild(i).gameObject;
            temp.ChooseBtn = false;
            InvenBtnList.Add(temp);
            InvenBtnList[i].gameObject.GetComponent<Button>().onClick.AddListener(()=>ChangeEqirType(index));
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
            int index = j;
            UserPanelBtnList[j].onClick.AddListener(() => UserPanelControll(index, gameCanvas.transform.childCount));
        }

        #endregion

        gameCanvas.transform.GetChild(2).gameObject.SetActive(true); //
        gameCanvas.transform.GetChild(1).GetChild(0).gameObject.SetActive(false); //
        
        //Debug.Log(SmithInventory.transform.GetChild(1).GetChild(0).GetChild(0).gameObject);
        //Debug.Log(GameObject.Find("Smith UI").transform.GetChild(0).GetChild(1).GetChild(0).GetChild(1).GetChild(0).gameObject);
        //Debug.Log(GameObject.Find("Smith UI").transform.GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject);
        //smith UI / Main Panel / BuyAndsell / itemDetail / ItemAbility / ItemAbility / Image
        
    }
    
    #region InventorySystem
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
            SmithInventory.GetComponent<Inventory>().FreshSlot(0);
            return;
        }
        else
        {
            InvenBtnList[0].ChooseBtn = false;
            InvenBtnList[1].ChooseBtn = false;
            InvenBtnList[index].ChooseBtn = true;
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

    #endregion
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
    
    void UserPanelControll(int index, int Length)
    {
        if(index == 0)
        {
            CleanSlots();
            ChangeMainUI(Length, false);
            gameCanvas.transform.GetChild(2).gameObject.SetActive(true); //
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

        
        UnityEngine.UI.Image ItemAbilityImage = ItemAbility.transform.GetChild(0).GetComponent<UnityEngine.UI.Image>();
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
}


