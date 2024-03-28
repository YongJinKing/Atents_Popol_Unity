using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;

public class SaveFileSelect : MonoBehaviour
{
    public TextMeshProUGUI[] GoldText;
    public TextMeshProUGUI[] TimeText;
    public TextMeshProUGUI[] DescText;
    public GameObject[] CoinIC;
    public bool[] savefile = new bool[3];

    public GameObject[] delButton;  //데이터 삭제버튼(임시)
    public GameObject delCheck;

    private void Start()
    {
        SlotCheck();
    }

    void SlotCheck()
    {
        for (int i = 0; i < 3; i++)
        {
            
            if (File.Exists(DataManager.instance.path + DataManager.instance.fileName + $"{i}"))	// 슬롯 데이터 존재 유무 확인
            {
                savefile[i] = true;     // 데이터가 있으면 true값 저장
                CoinIC[i].SetActive(true);
                delButton[i].SetActive(true);
                DataManager.instance.SlotNum = i;
                DataManager.instance.LoadData();
                GoldText[i].text = DataManager.instance.playerData.PlayerGold.ToString();	// 슬롯에 표시할 데이터
                TimeText[i].text = "PlayTime";   //DataManager.instance.playerData.PlayerGold.ToString();
                DescText[i].text = "Player.Lv : " + DataManager.instance.playerData.Level.ToString() +
                                   "\n(추가 예정)";
            }
            else	// 데이터가 없다면
            {
                GoldText[i].text = "";
                TimeText[i].text = "PlayTime";
                DescText[i].text = "비어있음";  //빈 슬롯 텍스트
                CoinIC[i].SetActive(false);
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
            GoGame();
        }
    }

    public void GoGame()	// 게임 씬으로 이동    1 Title, 2 Loading, 3 Main
    {
        if (!savefile[DataManager.instance.SlotNum])	// 저장되어있는 데이터가 없으면
        {
            DataManager.instance.SaveData(); // 새로운 데이터 저장
        }
        SceneLoading.SceneNum(2);
        SceneManager.LoadScene(1);
    }


    //슬롯 선택 창에서 데이터를 지우는 팝업창 임시 함수, 추후 변경 예정

    int selslot;
    public void DelCheck(int slotnum)
    {
        delCheck.SetActive(true);
        selslot = --slotnum;
    }

    public void closeWin()
    {
        delCheck.SetActive(false);
    }

    public void DelSlot()
    {
        DataManager.instance.DelData(selslot);
        savefile[selslot] = false;
        closeWin();
        SlotCheck();
    }
}
