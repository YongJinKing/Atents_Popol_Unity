using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.IO;

public class PlayerDetaManager
{
    public static PlayerDetaManager instance;
    public Dictionary<int, PlayerStatDeta> dicPlayerData;
    public Dictionary<int, PlayerLevelStat> dicPlayerLevelData;
    public Dictionary<int, UnitStringTable> dicStringData;
   
    private PlayerDetaManager()
    {

    }
    public static PlayerDetaManager GetInstance()
    {
        if(PlayerDetaManager.instance == null)
            PlayerDetaManager.instance = new PlayerDetaManager();
        return PlayerDetaManager.instance;
    }

    public void LoadPlayerData()
    {
        var PlayerStatJson = Resources.Load<TextAsset>("Player/PlayerStat/PlayerStat").text;
        var PlayerLevelStatJson = Resources.Load<TextAsset>("Player/PlayerStat/PlayerLevelStat").text;
        var UnitStringTable = Resources.Load<TextAsset>("Player/PlayerStat/Mestiarii_Charactor_StringTable").text;
    
        var arrPlayerDatas = JsonConvert.DeserializeObject<PlayerStatDeta[]>(PlayerStatJson);
        var arrPlayerLevel = JsonConvert.DeserializeObject<PlayerLevelStat[]>(PlayerLevelStatJson);
        var arrStringDatas = JsonConvert.DeserializeObject<UnitStringTable[]>(UnitStringTable);

        this.dicPlayerData = arrPlayerDatas.ToDictionary(x => x.index);
        this.dicPlayerLevelData = arrPlayerLevel.ToDictionary(x => x.Level);
        this.dicStringData = arrStringDatas.ToDictionary(x => x.index);
    }
}
