using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            GridLine.transform.GetChild(i).GetComponent<UIItem>().Attached
            (InvenDataManager.transform.GetChild(0).GetChild(i).GetComponent<UIItem>());
            GridLine.transform.GetChild(i).Find("Image").GetComponent<Image>().sprite = 
            GridLine.transform.GetChild(i).GetComponent<UIItem>().icon.sprite;
            Color color = GridLine.transform.GetChild(i).Find("Image").GetComponent<Image>().color;
            color.a = 1.0f;
            GridLine.transform.GetChild(i).Find("Image").GetComponent<Image>().color = color;
            
        }
        for(; i < GridLine.transform.childCount; i++)
        {
            GridLine.transform.GetChild(i).GetComponent<UIItem>().InitAll();
            GridLine.transform.GetChild(i).Find("Image").GetComponent<Image>().sprite = 
            GridLine.transform.GetChild(i).GetComponent<UIItem>().icon.sprite;
            Color color = GridLine.transform.GetChild(i).Find("Image").GetComponent<Image>().color;
            color.a = 0.0f;
            GridLine.transform.GetChild(i).Find("Image").GetComponent<Image>().color = color;
        }
        PlayerRiggingType.transform.Find("Weapon").GetComponent<UIItem>().
        Init(InvenDataManager.transform.Find("PlayerRigging").Find("Weapon").GetComponent<UIItem>().id);
        PlayerRiggingType.transform.Find("Armor").GetComponent<UIItem>().
        Init(InvenDataManager.transform.Find("PlayerRigging").Find("Armor").GetComponent<UIItem>().id);
    }
}
