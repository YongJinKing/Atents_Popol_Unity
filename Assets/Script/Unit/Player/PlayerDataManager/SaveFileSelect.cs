using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;

public class SaveFileSelect : MonoBehaviour
{
    public GameObject Canvas;
    public GameObject RemovePopup;
    public GameObject NewSlotPopup;
    int SelectSlot = 0;
    public TextMeshProUGUI[] GoldText;
    public TextMeshProUGUI[] ProgressText;
    public TextMeshProUGUI[] LevelText;
    public TextMeshProUGUI[] PLayTimeText;
    public bool[] savefile = new bool[3];

    public GameObject[] delButton;  //데이터 삭제버튼(임시)
    public GameObject delCheck;

    private void Awake()
    {
        
    }

    private void Start()
    {
        PlayerDataManager.GetInstance().LoadPlayerData();
        SlotCheck();
    }

    void SlotCheck()
    {
        for (int i = 0; i < 3; i++)
        {
            DataManager datamanager = DataManager.instance;
            transform.GetChild(0).GetChild(i).GetChild(0).GetChild(0).gameObject.SetActive(false);
            transform.GetChild(0).GetChild(i).GetChild(0).GetChild(1).gameObject.SetActive(false);
            if (File.Exists(DataManager.instance.path + DataManager.instance.fileName + $"{i}"))	// 슬롯 데이터 존재 유무 확인
            {
                savefile[i] = true;     // 데이터가 있으면 true값 저장
                datamanager.SlotNum = i;
                datamanager.LoadData();
                ItemDataManager.GetInstance().InvenItemLoadDatas();
                
                if(DataManager.instance.playerData.Weapon_Id> 0)
                {
                    transform.Find("SlotList").GetChild(i).GetChild(0).Find("LoadSlot").Find("ProfileSlot").Find("Weapon").GetComponent<UIItem>().
                    Init(datamanager.playerData.Weapon_Id);
                    transform.Find("SlotList").GetChild(i).GetChild(0).Find("LoadSlot").Find("ProfileSlot").Find("Armor").GetComponent<UIItem>().
                    Init(datamanager.playerData.Armor_Id);
                    
                }

                //Slot Display Data
                transform.GetChild(0).GetChild(i).GetChild(0).GetChild(0).gameObject.SetActive(true);

                int count = 0;
                foreach (bool value in datamanager.playerData.clearStage)
                {
                    if (value)
                    {
                        count++;
                    }
                }

                if (datamanager.playerData.PlayTime < 60) PLayTimeText[i].text = "<1m";
                else PLayTimeText[i].text = (datamanager.playerData.PlayTime / 60).ToString() + ":" + (datamanager.playerData.PlayTime % 60).ToString("D2");

                ProgressText[i].text = (100*count/datamanager.playerData.clearStage.Length).ToString()+"%";
                GoldText[i].text = UnitCalculate.GetInstance().Calculate(datamanager.playerData.PlayerGold);
                LevelText[i].text = datamanager.playerData.Character_CurrentLevel.ToString() + "Lv";
            }
            else	// IF Data Null
            {
                transform.GetChild(0).GetChild(i).GetChild(0).GetChild(1).gameObject.SetActive(true);
                delButton[i].SetActive(false);
            }
        }
        DataManager.instance.DataClear();   // 데이터 체크하는동안 저장된 데이터 클리어
    }

    public void Slot(int number)	// 슬롯 선택
    {
        DataManager.instance.SlotNum = number;

        if (savefile[number])   // 데이터가 있다면
        {
            DataManager.instance.LoadData();	// 슬롯 번호에 맞는 데이터 로드
            GoGame();
        }
        else    // 데이터가 없으면 그냥 게임 시작
        {
            NewSlotPopup?.gameObject.SetActive(true);
        }
    }

    public void GoGame()	// 게임 씬으로 이동    1 Title, 2 Loading, 3 Main
    {
        var playerdata = DataManager.instance.playerData;
        var playerstat = PlayerDataManager.instance.dicPlayerData[10000];
        if (!savefile[DataManager.instance.SlotNum])	// 저장되어있는 데이터가 없으면
        {
            playerdata.Character_CurrentLevel = playerstat.Character_CurrentLevel;
            playerdata.Character_CurrentExp = playerstat.Character_CurrentExp;
            playerdata.Character_Hp = playerstat.Character_Hp;
            playerdata.Character_AttackPower = playerstat.Character_AttackPower;
            playerdata.Character_EnergyGage = playerstat.Character_EnergyGage;
            playerdata.Character_MoveSpeed = playerstat.Character_MoveSpeed;
            playerdata.Character_AttackSpeed = playerstat.Character_AttackSpeed;
            DataManager.instance.SaveData(); // 새로운 데이터 저장
        }

        SceneLoading.SceneNum(2);
        SceneManager.LoadScene(1);
    }


    public void DelSlot(int index)
    {
        DataManager.instance.DelData(index);
        savefile[index] = false;
        SlotCheck();
    }

    public void CancelBtnAct(int index)
    {
        if(index != 3)//SaveSlotCancel이 아닐 때
        {
            SelectSlot = index;
            RemovePopup.gameObject.SetActive(true);
            RemovePopup.transform.Find("PopupBg").Find("Paper").Find("Desc").GetComponent<TMP_Text>().text
            = $"Slot{index + 1}번을 삭제하시겠습니까?";
        }
        else
        {
            Canvas.transform.Find("Title").gameObject.SetActive(true);
            Canvas.transform.Find("GameStart").gameObject.SetActive(false);
        }
        
    }
    public void SlotRemovePopupYesOrNo(int index)//0 : Yes 1 : No
    {
        if(index == 0)
        {
            //SelectSlot 활용
            DelSlot(SelectSlot);
            RemovePopup.gameObject.SetActive(false);
        }
        if(index == 1)
        {
            RemovePopup.gameObject.SetActive(false);
        }
    }
    public void NewSlotPopupYesOrNo(int index)
    {
        if(index == 0)
        {
            GoGame();
        }
        NewSlotPopup.gameObject.SetActive(false);
        
    }
}
