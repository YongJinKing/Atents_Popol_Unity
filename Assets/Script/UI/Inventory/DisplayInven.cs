using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayInven : MonoBehaviour
{
    public List<UIItem> items;
    public GameObject GridLine;
    public GameObject InvenDataManager;
    
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
    }
}
