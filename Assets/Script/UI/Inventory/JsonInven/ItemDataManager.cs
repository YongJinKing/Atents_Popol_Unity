using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

public class ItemDataManager 
{
    private static ItemDataManager instance;
    public Dictionary<int, ItemData> dicItemDatas;

    private ItemDataManager()
    {
        
    }

    public static ItemDataManager GetInstance()
    {
        if(ItemDataManager.instance == null)
            ItemDataManager.instance = new ItemDataManager();
        return ItemDataManager.instance;
    }
    public void LoadDatas()
    {
        var json = Resources.Load<TextAsset>("UI/UIItem/InvenItemData").text;
        var arrItemDatas = JsonConvert.DeserializeObject<ItemData[]>(json);
        /* foreach(var data in arrItemDatas)
        {
            Debug.LogFormat("{0}, {1}, {2} ",data.index, data.Inven_itemName, data.Inven_itemDesc);
        } */
        this.dicItemDatas = arrItemDatas.ToDictionary(x => x.index);
    }
}
