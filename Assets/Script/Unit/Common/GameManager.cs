using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public UnityEvent<int> deadAct;
    public GameObject Player;
    private GameObject Monster;
    //public PlayerDetaManager playerdata;


    Player pl;
    Monster Ms;

    private void Awake()
    {
        pl = Player.GetComponent<Player>();
        MonsterFactory mf = new MonsterFactory();
        Monster = mf.CreateMonster(30000);
        Ms = Monster.GetComponent<Monster>();
        mf = null;
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
        //var unitname = PlayerDetaManager.instance.dicStringData[playerstat.Character_Name]; // UI ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿?ï¿½ï¿½ï¿½ï¿½
        bs.Exp = pldata.Character_CurrentExp;
        bs.Level = pldata.Character_CurrentLevel;
        bs.ATK = pldata.Character_AttackPower;
        bs.HP = pldata.Character_Hp;
        bs.EnergyGage = pldata.Character_EnergyGage; // int
        bs.Speed = pldata.Character_MoveSpeed; // float
        bs.AttackDelay = pldata.Character_AttackSpeed; // float

        NextLevel = bs.Level + 1;

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
        pl.enabled = false;
        var playerdata = DataManager.instance.playerData;
        if (UnitType == 0) // Player Dead
        {

        }
        else // Monstar Dead
        {
            pl.Exp += Ms.Exp;
            playerdata.Character_CurrentExp += Ms.Exp;
            StartCoroutine(LevelUp());
        }
        deadAct.Invoke(UnitType);
        DataManager.instance.SaveData();
        StartCoroutine(tempDebug());
        if (cameraMove.isBoss)
        {
            
        }
    }
    public void UpdateUI()
    {
        //DataManager.instance.playerData.Armor_Duration = 
    }

    IEnumerator tempDebug()
    {
        yield return new WaitForSeconds(12);
        SceneLoading.SceneNum(2);
        SceneManager.LoadScene(1);
    }

    IEnumerator LevelUp()
    {
        var playerstat = PlayerDetaManager.instance.dicPlayerData[10000];
        var playerdata = DataManager.instance.playerData;
        while(playerdata.Character_CurrentExp >= PlayerDetaManager.instance.dicPlayerLevelData[playerdata.Character_CurrentLevel + 1].Total_Exp)
        {
            playerdata.Character_CurrentLevel++;
            if (playerdata.Character_CurrentLevel >= 30)
            {
                playerdata.Character_CurrentLevel = 30;
            }
            yield return null;
            var plLvstat = PlayerDetaManager.instance.dicPlayerLevelData[playerdata.Character_CurrentLevel];
            playerdata.Character_AttackPower = playerstat.Character_AttackPower + plLvstat.Total_AttackPower;
            playerdata.Character_Hp = playerstat.Character_Hp + plLvstat.Total_Hp;
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
