using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    //will delete later
    //this is for file load test
    private Dictionary<int, WaveData> dicWaveTable;
    

    private WaveData curWave;
    private Queue<WaveData> waveQueue = new Queue<WaveData>();

    private MonsterFactory mf;

    public UnityEvent<int> deadAct;

    //stage load, int for send total wave count
    public UnityEvent<int> loadStageDataEvent;
    //wave end event
    public UnityEvent waveEndEvent;
    //boss wave start
    public UnityEvent bossWaveStartEvent;
    //every wave is end
    public UnityEvent stageEndEvent;
    //player loss Game
    public UnityEvent lossGameEvent;

    public GameObject player;
    public HpAndEnergy hpEpBar;
    public WaveUI waveUI;

    public int curRewardGold = 0;
    public int curRewardExp = 0;
    //public PlayerDetaManager playerdata;

    


    private Player pl;
    private List<GameObject> monsters = new List<GameObject>();


    private void Awake()
    {
        mf = new MonsterFactory();

        pl = player.GetComponent<Player>();
        pl.hpbarChangeAct.AddListener(hpEpBar.HpGageTrigger);

        //Monster = mf.CreateMonster(30000);
        //Ms = Monster.GetComponent<Monster>();

        //need stage number
        //for test, static int 1 inside the function
        LoadStageData(DataManager.instance.StageNum);
        //StartCoroutine(WaveRound());

        LoadPlayerStat();
    }

    private void LoadStageData(int stageIndex)
    {
        var Mestiarii_WaveData = Resources.Load<TextAsset>("System/Mestiarii_WaveData_Table").text;
        var arrWaveDatas = JsonConvert.DeserializeObject<WaveData[]>(Mestiarii_WaveData);
        this.dicWaveTable = arrWaveDatas.ToDictionary(x => x.index);

        foreach(var data in dicWaveTable)
        {
            if(data.Value.Stage_Index == stageIndex)
            {
                waveQueue.Enqueue(data.Value);
            }
        }

        //waveQueue.Count is wave number
        //need to send waveQueue.Count to WaveUI class
        loadStageDataEvent?.Invoke(waveQueue.Count);

        /*
        //for debug
        while(waveQueue.Count > 0)
        {
            Debug.Log(waveQueue.Dequeue().index);
        }
        */
    }

    private void LoadPlayerStat()
    {
        int NextLevel;
        BattleStat bs = default;

        var pldata = DataManager.instance.playerData;
        //var unitname = PlayerDetaManager.instance.dicStringData[playerstat.Character_Name]; // UI ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿?ï¿½ï¿½ï¿½ï¿½
        bs.Exp = pldata.Character_CurrentExp;
        bs.Level = pldata.Character_CurrentLevel;
        bs.ATK = pldata.Character_AttackPower + pldata.Rigging_Weapon_Ability;
        bs.HP = pldata.Character_Hp + pldata.Rigging_Armor_Ability;
        bs.EnergyGage = pldata.Character_EnergyGage; // int
        bs.Speed = pldata.Character_MoveSpeed; // float
        bs.AttackDelay = pldata.Character_AttackSpeed; // float

        NextLevel = bs.Level + 1;
        if(NextLevel >= PlayerDataManager.instance.dicPlayerData[10000].Character_MaxLevel)
        {
            NextLevel = PlayerDataManager.instance.dicPlayerData[10000].Character_MaxLevel;
        }
        var plLvstat1 = PlayerDataManager.instance.dicPlayerLevelData[NextLevel];
        bs.MaxExp = plLvstat1.Total_Exp;

        pl.battlestat = bs;
    } 
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F4))
        {
            OnGameEnd(1, gameObject);
        }
    }

    public void TimeOutGameEnd()
    {
        pl.TakeDamage(99999, AttackType.Normal, DefenceType.Normal);
    }
    
    public void OnGameEnd(int UnitType, GameObject deadUnit)
    {
        var playerdata = DataManager.instance.playerData;
        if (UnitType == 0) // Player Dead
        {
            lossGameEvent?.Invoke();
            pl.enabled = false;
            DataManager.instance.SaveData();
        }
        else // Monstar Dead
        {
            if (monsters.Contains(deadUnit))
            {
                monsters.Remove(deadUnit);
                if(monsters.Count == 0)
                {
                    //RoundEnd
                    
                    playerdata.PlayerGold += curWave.Wave_Reward_Gold;
                    curRewardGold += curWave.Wave_Reward_Gold;
                    pl.Exp += curWave.Wave_Reward_Exp;
                    playerdata.Character_CurrentExp += curWave.Wave_Reward_Exp;
                    curRewardExp += curWave.Wave_Reward_Exp;
                    DataManager.instance.SaveData();
                    waveEndEvent?.Invoke();
                    player.GetComponent<Status>().Add(E_Buff.DotHeal);
                }
            }

            StartCoroutine(LevelUp());
        }
        if (curWave.index / 10000 >= 2 && monsters.Count <= 0) //Boss Wave End
        {
            var go = DataManager.instance.playerData;
            Debug.Log("StageEnd");
            DataManager.instance.SaveData();
            stageEndEvent?.Invoke();
            go.ClearStage[DataManager.instance.StageNum - 1] = true;
            //GameEndUI?.Invoke(1);//Monster
            pl.enabled = false;
        }
        
    }
    public void UpdateUI()
    {
        //DataManager.instance.playerData.Armor_Duration = 
    }

    IEnumerator LevelUp()
    {
        var playerstat = PlayerDataManager.instance.dicPlayerData[10000];
        var playerdata = DataManager.instance.playerData;
        while(playerdata.Character_CurrentExp >= PlayerDataManager.instance.dicPlayerLevelData[playerdata.Character_CurrentLevel + 1].Total_Exp)
        {
            playerdata.Character_CurrentLevel++;
            if (playerdata.Character_CurrentLevel >= 30)
            {
                playerdata.Character_CurrentLevel = 30;
            }
            yield return null;
            var plLvstat = PlayerDataManager.instance.dicPlayerLevelData[playerdata.Character_CurrentLevel];
            playerdata.Character_AttackPower = playerstat.Character_AttackPower + plLvstat.Total_AttackPower;
            playerdata.Character_Hp = playerstat.Character_Hp + plLvstat.Total_Hp;

            GameObject effect = Instantiate<GameObject>(Resources.Load($"Buff/LvUp") as GameObject);
            effect.transform.SetParent(player.transform, false); 
        }
    }
    
    public void WaveRownd()
    {
        //new Round Start
        StartCoroutine(WaveRound());
    }

    private IEnumerator WaveRound()
    {
        int spawnOffset = 15;
        if(waveQueue.Count > 0)
        {
            monsters.Clear();
            curWave = waveQueue.Dequeue();

            bool isBossWave = curWave.index / 10000 >= 2;

            if (isBossWave)
            {
                bossWaveStartEvent?.Invoke();
                pl.CinematicStart();
            }

            int count = 0;
            for(int i = 0; i < curWave.Wave_Monster_CountArr.Length; ++i)
            {
                count += curWave.Wave_Monster_CountArr[i];
            }

            float angle = 360 / (float)count;
            Vector3 spawnPoint = Vector3.back * spawnOffset;

            for(int i = 0; i < curWave.Wave_MonsterArr.Length; ++i)
            {
                for(int j = 0; j < curWave.Wave_Monster_CountArr[i]; ++j)
                {
                    monsters.Add(mf.CreateMonster(curWave.Wave_MonsterArr[i]));
                    spawnPoint = Quaternion.Euler(0, angle, 0) * spawnPoint;
                    monsters.Last().transform.position = spawnPoint;

                    if (!isBossWave)
                    {
                        monsters.Last().GetComponent<Monster>().CinematicEnd();
                    }
                    else if (curWave.Wave_MonsterArr[i] / 10000 >= 3)
                    {
                        Monster temp = monsters.Last().GetComponent<Monster>();
                        temp.CinematicStart();
                        temp.GetComponentInChildren<Camera>().gameObject.SetActive(true);
                        temp.transform.position = Vector3.up * 10;
                        temp.hpbarChangeAct = new UnityEvent<int, int>();
                        temp.hpbarChangeAct.AddListener(waveUI.BossHPFresh);
                        Status tempSt = monsters.Last().GetComponent<Status>();
                        tempSt.BuffAct = new UnityEvent<int>();
                        tempSt.BuffAct.AddListener(waveUI.GetComponent<DisplayCondition>().Display);
                    }

                    yield return new WaitForSeconds(1);
                }
            }

            yield return null;
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

    public void OnCinematicEnd()
    {
        foreach(GameObject data in monsters)
        {
            data.GetComponent<Monster>().CinematicEnd();

        }
        pl.CinematicEnd();
    }
}
