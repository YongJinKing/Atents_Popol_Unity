using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    

    
    public GameObject SmithInventory;//인벤토리 오브젝트 할당
    public GameObject gameCanvas;//게임 캔버스 할당
    public GameObject MainUI;//메인 UI할당
    private List<Slot> slotList = new List<Slot>();//Slot넣을 리스트 할당
    //private Button moveUI;


    public class Slot
    {
        public GameObject gameObject;
        public bool ChooseSlot;
    }
    private void Start() 
    {
        for(int i = 0; i < SmithInventory.GetComponent<Inventory>().slots.Length; i++)
        //리스트에 버튼할당 하는 프로세스
        {
            int index = i;
            //클래스 생성만
            Slot temp = new Slot();
            //클래스 초기화
            temp.gameObject = SmithInventory.transform.GetChild(1).GetChild(0).GetChild(i).GetChild(1).gameObject;
            temp.ChooseSlot = false;
            slotList.Add(temp);
            slotList[i].gameObject.GetComponent<Button>().onClick.AddListener(() => InvenBtnChoise(index));
        } 
        Button[] MainUIButtonList = MainUI.GetComponentsInChildren<UnityEngine.UI.Button>();//MainUI버튼할당
        for(int i = 0; i < MainUIButtonList.Length; i++)
        {
            int index = i;
            MainUIButtonList[i].onClick.AddListener(() => MainUiControll(index, MainUIButtonList.Length));
        }
        gameCanvas.transform.GetChild(2).gameObject.SetActive(true); //시작시 메인화면 On
        gameCanvas.transform.GetChild(1).GetChild(0).gameObject.SetActive(false); //시작시 메인화면 On
        /* moveUI = MainUI.GetComponentInChildren<Button>();
        Debug.Log(moveUI.gameObject.name); */
        
    }
    

    Color AlphaColorChange(int i, float Value)
    {
        Color color = slotList[i].gameObject.GetComponent<UnityEngine.UI.Image>().color;//현재 버튼 색깔인자값 전달받기
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
            slotList[i].ChooseSlot = false;//점등 X
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


