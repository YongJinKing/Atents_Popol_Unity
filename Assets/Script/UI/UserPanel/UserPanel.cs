using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class UserPanel : MonoBehaviour
{
    public GameObject Canvas;
    public GameObject MainUi;
    public UnityEvent<int> BtnAct;
    public UnityEvent<int> CleanBtnAct;
    public UnityEvent<int> FreshBtnAct;

    public GameObject UserPanelPopup;
    public void PressedBtn(int index)
    {
        
        if(index == 0)
        {
            for(int i = 2; i < Canvas.transform.childCount; i++)
            {
                Canvas.transform.GetChild(i).gameObject.SetActive(false);
            }
            MainUi.gameObject.SetActive(true);
            transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
            BtnAct?.Invoke(0);
            
        }
        else 
        {
            PopupDetail(index-1, true);
            CleanBtnAct?.Invoke(-1);
            FreshBtnAct?.Invoke(0);
        }
    }
    void PopupDetail(int index, bool Onpopup)
    {
        UserPanelPopup.transform.gameObject.SetActive(Onpopup);
        if(index == 0)
        {
            UserPanelPopup.transform.GetChild(index).gameObject.SetActive(Onpopup);
            UserPanelPopup.transform.GetChild(1).gameObject.SetActive(false);
        }
        if(index == 1)
        {
            UserPanelPopup.transform.GetChild(index).gameObject.SetActive(Onpopup);
            UserPanelPopup.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
    public void ClosePopup() =>
        UserPanelPopup.transform.gameObject.SetActive(false);
}
