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
    public Dictionary<int, InvenStringTable> dicStringTable;
    public Dictionary<int, InvenImageResourceTable> dicResouseTable;

    private ItemDataManager()
    {
        
    }

    public static ItemDataManager GetInstance()
    {
        if(ItemDataManager.instance == null)
            ItemDataManager.instance = new ItemDataManager();
        return ItemDataManager.instance;
    }
    public void InvenItemLoadDatas()
    {
        var Mestiarii_InvenItemJson = Resources.Load<TextAsset>("UI/UIItem/Json/Mestiarii_Inven_ItemTable").text;
        var Mestiarii_StringTableJson = Resources.Load<TextAsset>("UI/UIItem/Json/Mestiarii_Inven_StringTable").text;
        var Mestiarii_ImageResourceTable = Resources.Load<TextAsset>("UI/UIItem/Json/Mestiarii_Inven_ImageResourceTable").text;
        
        var arrItemDatas = JsonConvert.DeserializeObject<ItemData[]>(Mestiarii_InvenItemJson);
        var arrStringDatas = JsonConvert.DeserializeObject<InvenStringTable[]>(Mestiarii_StringTableJson);
        var arrResourceDatas = JsonConvert.DeserializeObject<InvenImageResourceTable[]>(Mestiarii_ImageResourceTable);
        /* foreach(var data in arrStringDatas)
        {
            Debug.LogFormat("{0}, {1}, {2} ",data.index, data.String_Type, data.String_Desc);
        } */
        this.dicItemDatas = arrItemDatas.ToDictionary(x => x.index);
        this.dicStringTable = arrStringDatas.ToDictionary(x => x.index);
        this.dicResouseTable = arrResourceDatas.ToDictionary(x => x.index);
    }
}
