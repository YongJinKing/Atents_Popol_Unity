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

    private void Awake()
    {
        pl = Player.GetComponent<Player>();
        playerdata = new PlayerDetaManager();
        Ms = Monster.GetComponent<Monster>();
        LoadPlayerStat();
    }
    void Start()
    {

    }

    void LoadPlayerStat()
    {
        BattleStat bs = default;

        var plLv = playerdata.playerlv;
        var playerstat = playerdata.playerstatdata;
        if(plLv != null)
        {
            playerstat.Character_CurrentExp = plLv.Exp;
            playerstat.Character_CurrentLevel = plLv.Level;
        }
        bs.ATK = playerstat.Character_AttackPower;
        bs.HP = playerstat.Character_Hp;
        bs.Exp = playerstat.Character_CurrentExp;
        bs.Level = playerstat.Character_CurrentLevel;
        bs.EnergyGage = playerstat.Character_EnergyGage;
        bs.Speed = playerstat.Character_MoveSpeed;
        bs.AttackDelay = playerstat.Character_AttackSpeed;

        var plLvstat = playerdata.dicPlayerLevelData[playerstat.Character_CurrentLevel];
        bs.ATK += plLvstat.Total_AttackPower;
        bs.HP += plLvstat.Total_Hp;
        bs.MaxExp = plLvstat.Total_Exp;

        pl.battlestat = bs;
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
            if(playerstat.Character_CurrentExp >= pl.MaxExp)
            {
                playerstat.Character_CurrentLevel++;
                if (playerstat.Character_CurrentLevel >= 30)
                {
                    playerstat.Character_CurrentLevel = 30;
                }
                
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
