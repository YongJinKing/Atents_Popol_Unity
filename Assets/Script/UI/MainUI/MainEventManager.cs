using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class MainEventManager : MonoBehaviour
{
    public GameObject gameCanvas;//게임 
    public GameObject DontDestoyedObj;
    public GameObject UserPanel;


    // Start is called before the first frame update
    void Start()
    {
        #region UIInit

        for(int i = 2; i < gameCanvas.transform.childCount; i++)
            {
                gameCanvas.transform.GetChild(i).gameObject.SetActive(false);
            }
            transform.gameObject.SetActive(true);
            UserPanel.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        #endregion
    }
    
    void ChangeMainUI()
    {
        
        for(int i = 2; i < gameCanvas.transform.childCount; i++)
        {
            gameCanvas.transform.GetChild(i).gameObject.SetActive(false);
        }
        UserPanel.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
    }
    public void MainUiControll(int index)
    {
        ChangeMainUI();
        gameCanvas.transform.GetChild(index+3).gameObject.SetActive(true);
    }
    
}
