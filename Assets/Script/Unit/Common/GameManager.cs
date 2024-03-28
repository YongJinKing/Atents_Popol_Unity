using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
public class GameManager : MonoBehaviour
{
    public UnityEvent<int> deadAct;
    public GameObject Player;
    public GameObject Monster;
    //public PlayerDetaManager playerdata;

    Player pl;
    Monster Ms;

    private void Awake()
    {
        PlayerDetaManager.GetInstance().LoadPlayerData();
        PlayerDetaManager.GetInstance().LoadPlayerLv();

        pl = Player.GetComponent<Player>();
        Ms = Monster.GetComponent<Monster>();

        LoadPlayerStat();
    }
    void Start()
    {
    }

    void LoadPlayerStat()
    {
        int NextLevel;
        BattleStat bs = default;

        var plLv = PlayerDetaManager.instance.playerlv;
        var playerstat = PlayerDetaManager.instance.dicPlayerData[10000];
        var unitname = PlayerDetaManager.instance.dicStringData[playerstat.Character_Name]; // UI ������ ��� ����
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

        var plLvstat = PlayerDetaManager.instance.dicPlayerLevelData[bs.Level];
        bs.ATK += plLvstat.Total_AttackPower;
        bs.HP += plLvstat.Total_Hp;
        NextLevel = ++bs.Level;
        if(NextLevel <= 30)
        {
            bs.MaxExp = PlayerDetaManager.instance.dicPlayerLevelData[NextLevel].Total_Exp;
        }
        

        pl.battlestat = bs;
    } 
    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnGameEnd(int UnitType)
    {
        var playerstat = PlayerDetaManager.instance.dicPlayerData[10000];
        if (UnitType == 0) // 플레이어가 죽었을 때
        {

        }
        else // 몬스터가 죽었을 때
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
                SavePlayerProgress();
            }
        }
        deadAct.Invoke(UnitType);
    }

    public void SavePlayerProgress()
    {
        var playerstat = PlayerDetaManager.instance.dicPlayerData[10000];
        var updatedData = new
        {
            Exp = playerstat.Character_CurrentExp,
            Level = playerstat.Character_CurrentLevel
        };
        string newDataJson = JsonConvert.SerializeObject(updatedData);
        string filePath = "Assets/Data/Resources/Player/PlayerStat/Playerlv.json";
        File.WriteAllText(filePath, newDataJson);
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
