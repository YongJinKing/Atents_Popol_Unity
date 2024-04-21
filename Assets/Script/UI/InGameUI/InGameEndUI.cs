using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameEndUI : MonoBehaviour
{
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

        }
        GameWinPopup.SetActive(false);
        GameLosePopup.SetActive(false);
    }
    public void GameEnd(int index)
    {   
        if(index == 0)
        {
            GameWinPopup.SetActive(true);
        }
        if(index == 1)
        {
            GameLosePopup.SetActive(true);
        }
    }

    
    
}
