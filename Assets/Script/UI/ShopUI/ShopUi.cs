using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUi : MonoBehaviour
{
    public GameObject Contents;
    int InstanceCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        ItemDataManager.GetInstance().InvenItemLoadDatas();
        
        foreach(KeyValuePair<int, ItemData> item in ItemDataManager.GetInstance().dicItemDatas)
        {   
            
            Instantiate(Resources.Load("UI/ShopUi/ItemSlot"),Contents.transform);
            Contents.transform.GetChild(InstanceCount).Find("ItemDetail").GetComponent<UIItem>().Init(item.Key);
            Contents.transform.GetChild(InstanceCount).Find("ItemDetail").Find("ItemName").GetComponent<TMP_Text>().text =
            Contents.transform.GetChild(InstanceCount).Find("ItemDetail").GetComponent<UIItem>().ItemName;
            Contents.transform.GetChild(InstanceCount).Find("ItemDetail").Find("ItemValue").GetComponent<TMP_Text>().text =
            Contents.transform.GetChild(InstanceCount).Find("ItemDetail").GetComponent<UIItem>().ItemValue.ToString();
            InstanceCount++;
        }
    }
}
