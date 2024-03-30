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

        var pldata = DataManager.instance.playerData;
        var playerstat = PlayerDetaManager.instance.dicPlayerData[10000];
        var unitname = PlayerDetaManager.instance.dicStringData[playerstat.Character_Name]; // UI ������ ��� ����
        


        bs.ATK = playerstat.Character_AttackPower;
        bs.HP = playerstat.Character_Hp;
        bs.Exp = pldata.Character_CurrentExp;
        bs.Level = pldata.Character_CurrentLevel;
        bs.EnergyGage = playerstat.Character_EnergyGage;
        bs.Speed = playerstat.Character_MoveSpeed;
        bs.AttackDelay = playerstat.Character_AttackSpeed;

        NextLevel = bs.Level + 1;

        var plLvstat = PlayerDetaManager.instance.dicPlayerLevelData[bs.Level];
        bs.ATK += plLvstat.Total_AttackPower;
        bs.HP += plLvstat.Total_Hp;

        var plLvstat1 = PlayerDetaManager.instance.dicPlayerLevelData[NextLevel];
        bs.MaxExp = plLvstat1.Total_Exp;

        pl.battlestat = bs;
    } 
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F4))
        {
            OnGameEnd(1);
        }
    }

    public void OnGameEnd(int UnitType)
    {
        var playerdata = DataManager.instance.playerData;
        if (UnitType == 0) // 플레이어가 죽었을 때
        {

        }
        else // 몬스터가 죽었을 때
        {
            pl.Exp += Ms.Exp;
            playerdata.Character_CurrentExp += Ms.Exp;
            StartCoroutine(LevelUp());
        }
        deadAct.Invoke(UnitType);
    }

    IEnumerator LevelUp()
    {
        var playerdata = DataManager.instance.playerData;
        while(playerdata.Character_CurrentExp >= PlayerDetaManager.instance.dicPlayerLevelData[playerdata.Character_CurrentLevel + 1].Total_Exp)
        {
            playerdata.Character_CurrentLevel++;
            if (playerdata.Character_CurrentLevel >= 30)
            {
                playerdata.Character_CurrentLevel = 30;
            }
            yield return null;
        }
        DataManager.instance.SaveData();
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
