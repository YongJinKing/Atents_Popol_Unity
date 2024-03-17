using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UserPanel : MonoBehaviour
{
    public GameObject Canvas;
    public GameObject MainUi;
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
            
        }
        else if(index == 1)
        {
            PopupDetail(index-1, true);
        }
        else if(index == 2)
        {
            PopupDetail(index-1, true);
        }
    }
    void PopupDetail(int index, bool Onpopup)
    {
        /* UserPanelPopup.transform.gameObject.SetActive(Onpopup);
        UserPanelPopup.transform.GetChild(index).gameObject.SetActive(Onpopup); */
    }
}
