using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NavPanel : MonoBehaviour
{
    
    public GameObject GameSettingPopup;
    void Update()
    {
        
        transform.Find("Profile BackGround").Find("Gold_Displayer").Find("Text (TMP)").GetComponent<TMP_Text>().text =
        UnitCalculate.GetInstance().Calculate(DataManager.instance.playerData.PlayerGold);
    }
    public void PressedBtn(int index)
    {
        if(index == 0)
        {
            GameSettingPopup.SetActive(true);
        }
    }
}
