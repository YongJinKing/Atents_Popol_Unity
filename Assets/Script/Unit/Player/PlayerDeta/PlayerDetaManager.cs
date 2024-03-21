using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.IO;

public class PlayerDetaManager
{
    // public static PlayerDetaManager instance;
    public PlayerStatDeta playerstatdata;
    public Dictionary<int, PlayerLevelStat> dicPlayerLevelData;
    public PlayerLv playerlv;
    public PlayerDetaManager()
    {
        LoadPlayerStatDatas();
        LoadLevelDatas();
        LoadPlayerLv();
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
    }

    public void LoadPlayerLv()
    {
        var textAsset = Resources.Load<TextAsset>("Player/PlayerStat/Playerlv");
        if (textAsset == null) return;
        var json = textAsset.text;
        var PlayerlvDatas = JsonConvert.DeserializeObject<PlayerLv>(json);
        playerlv = PlayerlvDatas;
    }

    public void SavePlayerProgress()
    {
        var updatedData = new { Exp = playerstatdata.Character_CurrentExp, 
        Level = playerstatdata.Character_CurrentLevel };
        string newDataJson = JsonConvert.SerializeObject(updatedData);
        string filePath = "Assets/Data/Resources/Player/PlayerStat/Playerlv.json";
        File.WriteAllText(filePath, newDataJson);
    }

    public void LoadLevelDatas()
    {
        var json = Resources.Load<TextAsset>("Player/PlayerStat/PlayerLevelStat").text;
        var arrPlayerLevel = JsonConvert.DeserializeObject<PlayerLevelStat[]>(json);
        this.dicPlayerLevelData = arrPlayerLevel.ToDictionary(x => x.Level);
    }
}
