using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Linq;

public class PlayerDataManager
{
    public static PlayerDataManager instance;
    public Dictionary<int, PlayerStatData> dicPlayerData;
    public Dictionary<int, PlayerLevelStat> dicPlayerLevelData;
    public Dictionary<int, UnitStringTable> dicStringData;
   
    private PlayerDataManager()
    {

    }
    public static PlayerDataManager GetInstance()
    {
        if(PlayerDataManager.instance == null)
            PlayerDataManager.instance = new PlayerDataManager();
        return PlayerDataManager.instance;
    }

    public void LoadPlayerData()
    {
        var PlayerStatJson = Resources.Load<TextAsset>("Player/PlayerStat/PlayerStat").text;
        var PlayerLevelStatJson = Resources.Load<TextAsset>("Player/PlayerStat/PlayerLevelStat").text;
        var UnitStringTable = Resources.Load<TextAsset>("Player/PlayerStat/Mestiarii_Charactor_StringTable").text;
    
        var arrPlayerDatas = JsonConvert.DeserializeObject<PlayerStatData[]>(PlayerStatJson);
        var arrPlayerLevel = JsonConvert.DeserializeObject<PlayerLevelStat[]>(PlayerLevelStatJson);
        var arrStringDatas = JsonConvert.DeserializeObject<UnitStringTable[]>(UnitStringTable);

        this.dicPlayerData = arrPlayerDatas.ToDictionary(x => x.index);
        this.dicPlayerLevelData = arrPlayerLevel.ToDictionary(x => x.Level);
        this.dicStringData = arrStringDatas.ToDictionary(x => x.index);
    }
}
