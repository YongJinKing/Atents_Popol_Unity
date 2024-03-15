using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserPanel : MonoBehaviour
{
    public GameObject UserPanelPopup;
    public void PressedBtn(int index)
    {
        if(index == 0)
        {

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
