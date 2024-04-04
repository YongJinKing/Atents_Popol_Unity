using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayInven : MonoBehaviour
{
    public List<UIItem> items;
    public GameObject GridLine;
    public GameObject InvenDataManager;
    public GameObject PlayerRiggingType;
    
    void Update()
    {
        items = InvenDataManager.GetComponent<Inventory>().ItemList;
        int i = 0;
        for(; i < items.Count; i++)
        {
            GridLine.transform.GetChild(i).GetComponent<UIItem>().Init(items[i].id);
        }
        for(; i < GridLine.transform.childCount; i++)
        {
            GridLine.transform.GetChild(i).GetComponent<UIItem>().InitAll();
        }
        PlayerRiggingType.transform.Find("Weapon").GetComponent<UIItem>().
        Init(InvenDataManager.transform.Find("PlayerRigging").Find("Weapon").GetComponent<UIItem>().id);
        PlayerRiggingType.transform.Find("Armor").GetComponent<UIItem>().
        Init(InvenDataManager.transform.Find("PlayerRigging").Find("Armor").GetComponent<UIItem>().id);
    }
}
