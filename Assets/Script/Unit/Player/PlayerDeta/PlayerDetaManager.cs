using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

public class PlayerDetaManager
{
    // public static PlayerDetaManager instance;
    public PlayerStatDeta playerstatdata;
    public Dictionary<int, PlayerLevelStat> dicPlayerLevelData;
    
    public PlayerDetaManager()
    {
        LoadPlayerStatDatas();
        LoadLevelDatas();
    }

    // public PlayerDetaManager GetInstance()
    // {
    //     if(PlayerDetaManager.instance == null)
    //         PlayerDetaManager.instance = new PlayerDetaManager();
    //     return PlayerDetaManager.instance;
    // }

    public void LoadPlayerStatDatas()
    {
        var json = Resources.Load<TextAsset>("Player/PlayerStat/PlayerStat").text;
        var arrPlayerDatas = JsonConvert.DeserializeObject<PlayerStatDeta[]>(json);
        playerstatdata = arrPlayerDatas[0];
        // Debug.Log(playerstatdata);
    }

    public void LoadLevelDatas()
    {
        var json = Resources.Load<TextAsset>("Player/PlayerStat/PlayerLevelStat").text;
        var arrPlayerLevel = JsonConvert.DeserializeObject<PlayerLevelStat[]>(json);
        this.dicPlayerLevelData = arrPlayerLevel.ToDictionary(x => x.Level);
    }
}
