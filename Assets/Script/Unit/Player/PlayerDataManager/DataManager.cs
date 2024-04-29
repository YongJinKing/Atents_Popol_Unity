    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;




[Serializable]
public class PlayerData
{
    public int Character_Hp;
    public int Character_AttackPower;
    public int Character_CurrentExp;
    public int Character_CurrentLevel;
    public int Character_EnergyGage;
    public float Character_MoveSpeed;
    public float Character_AttackSpeed;
    public int PlayerGold = 0;

    public int Rigging_Weapon_Id;
    public int Rigging_Armor_Id;
    public int Rigging_Weapon_Ability;
    public int Rigging_Armor_Ability;
    public int Rigging_Weapon_Type; 
    public int Rigging_Armor_Type;
    public int Rigging_Weapon_Duration;
    public int Rigging_Armor_Duration;

    public string[] SaveUISkillList = new string[100];
    public String[] InGameSkillList = new string[4];
    public bool[] ClearStage = { false, false };
    public List<int> PlayerInven = new List<int>();
    public List<int> PlayerInvenDuraion = new List<int>();
    public int PlayTime;

    public float Master_Volum = 0.75f;
    public float Bgm_Volum = 0.75f;
    public float Sfx_Volum = 0.75f;
    public bool Master_Mute_Check = true;
    public bool Bgm_Mute_Check = true;
    public bool Sfx_Mute_Check = true;
}


public class DataManager : MonoBehaviour
{
    public PlayerData playerData = new PlayerData();
    public string path;
    public string fileName = "PlayerData";

    public int StageNum;
    public int SlotNum;

    public static DataManager instance;

    private void Awake()    //?��???
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != null)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        path = Application.dataPath + "/Data/PlayerSaveFile/";      // json?�일 ?�??경로
    }

    public void Start()
    {
    }

    public void SaveData()
    {
        string data = JsonUtility.ToJson(playerData, true);
        File.WriteAllText(path + fileName + SlotNum.ToString(), data);
    }

    public void LoadData()
    {
        string data = File.ReadAllText(path + fileName + SlotNum.ToString());
        playerData = JsonUtility.FromJson<PlayerData>(data);
    }
    public void DataClear()
    {
        SlotNum = -1;
        playerData = new PlayerData();
    }

    public void DelData(int slotnum)
    {
        File.Delete(path + fileName + slotnum.ToString());
    }

}


