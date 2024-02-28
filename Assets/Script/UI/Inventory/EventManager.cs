using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    

    

    public GameObject SmithInventory;//?�벤?�리 ?�브?�트 ?�당
    public GameObject gameCanvas;//게임 캔버???�당
    public GameObject MainUI;//메인 UI?�당
    private List<Slot> slotList = new List<Slot>();//Slot?�을 리스???�당

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

        Button[] MainUIButtonList = MainUI.GetComponentsInChildren<UnityEngine.UI.Button>();//MainUI버튼?�당

        for(int i = 0; i < MainUIButtonList.Length; i++)
        {
            int index = i;
            MainUIButtonList[i].onClick.AddListener(() => MainUiControll(index, MainUIButtonList.Length));
        }

        gameCanvas.transform.GetChild(2).gameObject.SetActive(true); //?�작??메인?�면 On
        gameCanvas.transform.GetChild(1).GetChild(0).gameObject.SetActive(false); //?�작??메인?�면 On

        
    }
    

    Color AlphaColorChange(int i, float Value)
    {
        Color color = slotList[i].gameObject.GetComponent<UnityEngine.UI.Image>().color;//?�재 버튼 ?�깔?�자�??�달받기
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
            slotList[i].ChooseSlot = false;//?�등 X
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


