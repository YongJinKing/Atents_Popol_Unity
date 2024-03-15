using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class MainEventManager : MonoBehaviour
{
    public GameObject gameCanvas;//게임 
    public GameObject MainUI;//메인 UI
    public GameObject UserPanel;
    public GameObject DontDestoyedObj;
    public GameObject SmithEvnetManager;


    // Start is called before the first frame update
    void Start()
    {
        
        Button[] MainUIButtonList = MainUI.GetComponentsInChildren<UnityEngine.UI.Button>();//MainUI버튼
        for(int i = 0; i < MainUIButtonList.Length; i++)
        {
            int index = i;
            MainUIButtonList[i].onClick.AddListener(() => MainUiControll(index, MainUIButtonList.Length));
        }
        #region User_PanelInit


        Button[] UserPanelBtnList = UserPanel.GetComponentsInChildren<UnityEngine.UI.Button>();
        for(int j = 0; j < UserPanelBtnList.Length; j++)
        {

            int index = j;
            UserPanelBtnList[j].onClick.AddListener(() => UserPanelControll(index, gameCanvas.transform.childCount));
        }


        #endregion


        #region UIInit
        gameCanvas.transform.GetChild(2).gameObject.SetActive(true); // 메인 on
        gameCanvas.transform.GetChild(3).gameObject.SetActive(false); // 쇼핑 off
        gameCanvas.transform.GetChild(4).gameObject.SetActive(false); // 수리 off
        gameCanvas.transform.GetChild(5).gameObject.SetActive(false); // 경기 off
        gameCanvas.transform.GetChild(6).gameObject.SetActive(false); // 팝업 off
        gameCanvas.transform.GetChild(1).GetChild(0).gameObject.SetActive(false); //x버튼 off
        #endregion
    }
    
    void ChangeMainUI(int UiLength, bool isShow)
    {
        for(int i = 0; i < UiLength-2; i++)
        {
            gameCanvas.transform.GetChild(i+2).gameObject.SetActive(false);
        }
        gameCanvas.transform.GetChild(1).GetChild(0).gameObject.SetActive(isShow);
    }
    void MainUiControll(int index, int UiLength)
    {
        ChangeMainUI(UiLength, true);
        gameCanvas.transform.GetChild(index+3).gameObject.SetActive(true);
    }
    #region UserPanelUI
    void UserPanelControll(int index, int Length)
    {
        if(index == 0)
        {
            SmithEvnetManager.GetComponent<SmithEventManager>().CleanSlots();
            ChangeMainUI(Length, false);
            gameCanvas.transform.GetChild(2).gameObject.SetActive(true); //
        }
    }
    #endregion  
}
