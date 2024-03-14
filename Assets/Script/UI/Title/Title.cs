using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    public GameObject TitleMenu;
    public GameObject SaveSlotMenu;
    public void PressedBtn(int index)
    {
        if(index == 0)
        {
            MenuActive(false, true, false);
        }
        if(index == 1)
        {
            MenuActive(true, false, false);
        }
    }
    void MenuActive(bool TitleCheck,bool SettingCheck,bool EndCheck)
    {
        TitleMenu.transform.gameObject.SetActive(TitleCheck);
        SaveSlotMenu.transform.gameObject.SetActive(SettingCheck);
    }
}
