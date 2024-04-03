using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NavPanel : MonoBehaviour
{
    
    
    void Update()
    {
        
        transform.Find("Profile BackGround").Find("Gold_Displayer").Find("Text (TMP)").GetComponent<TMP_Text>().text =
        DataManager.instance.playerData.PlayerGold.ToString();
    }
}
