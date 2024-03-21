using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Player;
    public GameObject Monster;
    public PlayerDetaManager playerdata;
    Player pl;
    Monster Ms;
    void Start()
    {
        playerdata = new PlayerDetaManager();
        pl = Player.GetComponent<Player>();
        Ms = Monster.GetComponent<Monster>();
        LoadPlayerStat();
    }

    void LoadPlayerStat()
    {
        var plLv = playerdata.playerlv;
        var playerstat = playerdata.playerstatdata;
        if(plLv != null)
        {
            playerstat.Character_CurrentExp = plLv.Exp;
            playerstat.Character_CurrentLevel = plLv.Level;
        }
        pl.ATK = playerstat.Character_AttackPower;
        pl.HP = playerstat.Character_Hp;
        pl.Exp = playerstat.Character_CurrentExp;
        pl.Lavel = playerstat.Character_CurrentLevel;
        pl.EnergyGage = playerstat.Character_EnergyGage;
        
        var plLvstat = playerdata.dicPlayerLevelData[playerstat.Character_CurrentLevel];
        pl.ATK += plLvstat.Total_AttackPower;
        pl.HP += plLvstat.Total_Hp;
        pl.MaxExp = plLvstat.Total_Exp;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnGameEnd(int UnitType)
    {
        var playerstat = playerdata.playerstatdata;
        if (UnitType == 0)
        {

        }
        else
        {
            pl.Exp += Ms.Exp;
            playerstat.Character_CurrentExp += Ms.Exp;
            if(playerstat.Character_CurrentExp > pl.MaxExp)
            {
                playerstat.Character_CurrentLevel++;
            }
            playerdata.SavePlayerProgress();
        }
    }

    public void EndGame()
    {
        var filePath = "Assets/Resources/Player/PlayerStat/Playerlv.json";
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }
}
