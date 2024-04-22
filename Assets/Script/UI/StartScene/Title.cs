using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    public GameObject Canvas;
    private void Start() {
        
        SoundManager.instance.PlayBgmMusic("TitleMusic");
    }
    public void PressedBtn(int index)
    {
        if(index == 0)//GameStart
        {
            MenuAct(index+1, false);
        }
        if(index == 1)//GameSetting
        {
            MenuAct(index+1, true);
        }
        if(index == 2)//GameEnd
        {
            MenuAct(index+1, true);
        }
    }
    public void MenuAct(int index, bool OnCheck)
    {
        Canvas.transform.Find("Title").gameObject.SetActive(OnCheck);
        Canvas.transform.GetChild(index).gameObject.SetActive(true);
    }
    
}
