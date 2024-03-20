using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Player;
    public PlayerDetaManager playerdata;
    Player pl;
    void Start()
    {
        playerdata = new PlayerDetaManager();
        pl = Player.GetComponent<Player>();
        LoadPlayerStat();
    }

    void LoadPlayerStat()
    {
        var playerstat = playerdata.playerstatdata;
        pl.ATK = playerstat.Character_AttackPower;
        pl.HP = playerstat.Character_Hp;
        pl.Exp = playerstat.Character_CurrentExp;
        pl.Lavel = playerstat.Character_CurrentLevel;
        pl.EnergyGage = playerstat.Character_EnergyGage;
        
        var plLvstat = playerdata.dicPlayerLevelData[pl.Lavel];
        pl.ATK += plLvstat.Total_AttackPower;
        pl.HP += plLvstat.Total_Hp;
        pl.MaxExp += plLvstat.Total_Exp;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
