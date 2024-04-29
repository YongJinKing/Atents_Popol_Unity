using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameShowItem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var go = transform.GetChild(0).GetChild(0).GetChild(0);
        go.GetChild(0).GetComponent<UIItem>().Init(DataManager.instance.playerData.Rigging_Armor_Id);
        go.GetChild(1).GetComponent<UIItem>().Init(DataManager.instance.playerData.Rigging_Weapon_Id);
    }

    
}
