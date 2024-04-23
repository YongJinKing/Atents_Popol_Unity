using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class InGameEndUI : MonoBehaviour
{
    public GameObject GameManager;
    GameObject GameWinPopup;
    GameObject GameLosePopup;
    void Start()
    {
        GameWinPopup = transform.GetChild(0).gameObject;//GameEndUI/GameWin
        GameLosePopup = transform.GetChild(1).gameObject;//GameEndUI/GameLose
        GameWinPopup.SetActive(false);
        GameLosePopup.SetActive(false);
    }

    public void PressedBtn(int index)
    {
        if(index == 0)//SceneChange
        {
            SceneLoading.SceneNum(2);
            SceneManager.LoadScene(1);
        }
        GameWinPopup.SetActive(false);
        GameLosePopup.SetActive(false);
    }
    public void GameEnd(int index)
    {   
        if(index == 0)
        {
            GameWinPopup.SetActive(true);
            var go =  GameWinPopup.transform.GetChild(0).GetChild(1).GetChild(1);//GameWin//Paper/GridLine
            go.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text =//GoldReward/Text
            GameManager.GetComponent<GameManager>().curRewardGold.ToString();
            go.GetChild(1).GetChild(1).GetComponent<TMP_Text>().text =//ExpReward/Text
            GameManager.GetComponent<GameManager>().curRewardExp.ToString();   
        }
        if(index == 1)
        {
            GameLosePopup.SetActive(true);
            var go =  GameLosePopup.transform.GetChild(0).GetChild(1).GetChild(1);//GameWin//Paper/GridLine
            go.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text =//GoldReward/Text
            GameManager.GetComponent<GameManager>().curRewardGold.ToString();
            go.GetChild(1).GetChild(1).GetComponent<TMP_Text>().text =//ExpReward/Text
            GameManager.GetComponent<GameManager>().curRewardExp.ToString();   
        }
    }

    
    
}
