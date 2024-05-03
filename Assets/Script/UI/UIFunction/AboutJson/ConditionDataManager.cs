
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Linq;

public class ConditionDataManager 
{
    private static ConditionDataManager instance;
    public Dictionary<int, ConditionData> dicConditionDatas;
    public Dictionary<int, ConditionStringTable> dicStringTable;
    public Dictionary<int, ConditionImageResourceTable> dicResouseTable;

    private ConditionDataManager()
    {

    }
    
    public static ConditionDataManager GetInstance()
    {
        if(ConditionDataManager.instance == null)
            ConditionDataManager.instance = new ConditionDataManager();
        return ConditionDataManager.instance;
    }
    public void ConditionLoadDatas()
    {
        var Mestiarii_InGame_ConditionDataTable = Resources.Load<TextAsset>("UI/BuffAndDeBuff/Json/Mestiarii_InGame_ConditionDataTable").text;
        var Mestiarii_InGame_StringTable = Resources.Load<TextAsset>("UI/BuffAndDeBuff/Json/Mestiarii_InGame_StringTable").text;
        var Mestiarii_InGame_ImageResourceTable = Resources.Load<TextAsset>("UI/BuffAndDeBuff/Json/Mestiarii_InGame_ImageResourceTable").text;
        
        var arrConditionDatas = JsonConvert.DeserializeObject<ConditionData[]>(Mestiarii_InGame_ConditionDataTable);
        var arrStringDatas = JsonConvert.DeserializeObject<ConditionStringTable[]>(Mestiarii_InGame_StringTable);
        var arrResourceDatas = JsonConvert.DeserializeObject<ConditionImageResourceTable[]>(Mestiarii_InGame_ImageResourceTable);
        /* foreach(var data in arrStringDatas)
        {
            Debug.LogFormat("{0}, {1}, {2} ",data.index, data.String_Type, data.String_Desc);
        } */
        this.dicConditionDatas = arrConditionDatas.ToDictionary(x => x.index);
        this.dicStringTable = arrStringDatas.ToDictionary(x => x.index);
        this.dicResouseTable = arrResourceDatas.ToDictionary(x => x.index);
    }
}
