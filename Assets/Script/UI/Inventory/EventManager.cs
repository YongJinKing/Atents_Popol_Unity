using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    

    

    public GameObject SmithInventory;//?∏Î≤§?†Î¶¨ ?§Î∏å?ùÌä∏ ?†Îãπ
    public GameObject gameCanvas;//Í≤åÏûÑ Ï∫îÎ≤Ñ???†Îãπ
    public GameObject MainUI;//Î©îÏù∏ UI?†Îãπ
    private List<Slot> slotList = new List<Slot>();//Slot?£ÏùÑ Î¶¨Ïä§???†Îãπ

    //private Button moveUI;


    public class Slot
    {
        public GameObject gameObject;
        public bool ChooseSlot;
    }
    private void Start() 
    {
        for(int i = 0; i < SmithInventory.GetComponent<Inventory>().slots.Length; i++)

        {
            int index = i;
            
            Slot temp = new Slot();

            temp.gameObject = SmithInventory.transform.GetChild(1).GetChild(0).GetChild(i).GetChild(1).gameObject;
            temp.ChooseSlot = false;
            slotList.Add(temp);
            slotList[i].gameObject.GetComponent<Button>().onClick.AddListener(() => InvenBtnChoise(index));
        } 

        Button[] MainUIButtonList = MainUI.GetComponentsInChildren<UnityEngine.UI.Button>();//MainUIÎ≤ÑÌäº?†Îãπ

        for(int i = 0; i < MainUIButtonList.Length; i++)
        {
            int index = i;
            MainUIButtonList[i].onClick.AddListener(() => MainUiControll(index, MainUIButtonList.Length));
        }

        gameCanvas.transform.GetChild(2).gameObject.SetActive(true); //?úÏûë??Î©îÏù∏?îÎ©¥ On
        gameCanvas.transform.GetChild(1).GetChild(0).gameObject.SetActive(false); //?úÏûë??Î©îÏù∏?îÎ©¥ On

        
    }
    

    Color AlphaColorChange(int i, float Value)
    {
        Color color = slotList[i].gameObject.GetComponent<UnityEngine.UI.Image>().color;//?ÑÏû¨ Î≤ÑÌäº ?âÍπî?∏ÏûêÍ∞??ÑÎã¨Î∞õÍ∏∞
        color.a = Value;
        slotList[i].gameObject.GetComponent<UnityEngine.UI.Image>().color = color;
        return slotList[i].gameObject.GetComponent<UnityEngine.UI.Image>().color;
    }

    void CleanUi(int UiLength)
    {
        for(int i = 0; i < UiLength-1; i++)
        {
            gameCanvas.transform.GetChild(i+2).gameObject.SetActive(false);
        }
        gameCanvas.transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
    }

    void MainUiControll(int UiId, int UiLength)
    {
        CleanUi(UiLength);
        gameCanvas.transform.GetChild(UiId+3).gameObject.SetActive(true);
    }

    void CleanSlots()
    {
        for(int i = 0; i < SmithInventory.GetComponent<Inventory>().slots.Length; i++)
        {
            AlphaColorChange(i, 0.0f);
            slotList[i].ChooseSlot = false;//?êÎì± X
        } 
    }


    void InvenBtnChoise(int buttonId)
    {
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
        }
    }
}


