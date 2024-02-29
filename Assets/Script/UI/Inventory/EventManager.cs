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
    
    private List<Slot> slotList = new List<Slot>();//

    //private Button moveUI;


    public class Slot
    {
        public GameObject gameObject;
        public bool ChooseSlot;
    }
    private void Start() 
    {
        #region SmithInventoryInit
        for(int i = 0; i < SmithInventory.GetComponent<Inventory>().slots.Length; i++)
        {
            int index = i;
            Slot temp = new Slot();
            temp.gameObject = SmithInventory.transform.GetChild(1).GetChild(0).GetChild(i).GetChild(1).gameObject;
            temp.ChooseSlot = false;
            slotList.Add(temp);
            slotList[i].gameObject.GetComponent<Button>().onClick.AddListener(() => InvenBtnChoise(index));
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
    
    #region ChooseInvenItem
    Color AlphaColorChange(int i, float Value)
    {
        Color color = slotList[i].gameObject.GetComponent<UnityEngine.UI.Image>().color;//
        color.a = Value;
        slotList[i].gameObject.GetComponent<UnityEngine.UI.Image>().color = color;
        return slotList[i].gameObject.GetComponent<UnityEngine.UI.Image>().color;
    }

    void CleanSlots()
    {
        for(int i = 0; i < SmithInventory.GetComponent<Inventory>().slots.Length; i++)
        {
            AlphaColorChange(i, 0.0f);
            slotList[i].ChooseSlot = false;//
        }
        ItemDetailShow(false);
    }
    void InvenBtnChoise(int buttonId)
    {
        
        List<Item> itemlist = SmithInventory.GetComponent<Inventory>().items;
        UnityEngine.UI.Image ItemInSlot = SmithInventory.transform.GetChild(1).GetChild(0).GetChild(buttonId).gameObject.GetComponent<UnityEngine.UI.Image>();
        if(itemlist[buttonId])
        {
            ItemDetailShow(false);
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
                AlphaColorChange(buttonId, 0.3f);
                ItemDetailShow(true);
                ItemDetailChange(itemlist[buttonId]);
            }
        }
        else return;
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
    void ItemDetailChange(Item item)
    {
        UnityEngine.UI.Image ItemAbilityImage = GameObject.Find("Smith UI").transform.GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<UnityEngine.UI.Image>();
        TMP_Text ItemAbilityText = GameObject.Find("Smith UI").transform.GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetChild(1).gameObject.GetComponent<TMP_Text>();
        UnityEngine.UI.Image NpcTalkBollum = GameObject.Find("Smith UI").transform.GetChild(0).GetChild(1).GetChild(0).GetChild(1).GetChild(0).gameObject.GetComponent<UnityEngine.UI.Image>();
        TMP_Text NpcText = GameObject.Find("Smith UI").transform.GetChild(0).GetChild(1).GetChild(0).GetChild(1).GetChild(1).gameObject.GetComponent<TMP_Text>();;
        List<Item> itemlist = SmithInventory.GetComponent<Inventory>().items;
        ItemAbilityImage.sprite = item.itemImage;
        string EqirType(RiggingType Type)
        {
            if(Type == RiggingType.Weapon)
                name = "공격력 : ";
            if(Type == RiggingType.Armor)
                name = "체력 : ";
            return name;
        }
        ItemAbilityText.text = 
        "이름 : " + item.itemName + "\n\n" + EqirType(item.riggingType)
         + item.itemValue + "\n\n" + "내구도 : " + item.durAbility;
        NpcText.text = item.smithTalk;

    }
}


