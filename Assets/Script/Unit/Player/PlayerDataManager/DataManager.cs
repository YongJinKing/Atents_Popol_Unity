    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;




[Serializable]
public class PlayerData
{
    public int index;
    public int Character_Hp;
    public int Character_AttackPower;
    public int Character_CurrentExp;
    public int Character_CurrentLevel;
    public int Character_EnergyGage;
    public float Character_MoveSpeed;
    public float Character_AttackSpeed;

    public int Weapon_Id;
    public int Armor_Id;
    public int Weapon_Ability;
    public int Armor_Ability;
    public int WeaponType;
     //0 : ?úÏÜê Í≤Ä, 1: ?ëÏÜê Í≤Ä, 2 : ?úÏÜê ?îÍ∏∞, 3 : ?ëÏÜê ?îÍ∏∞, 4 : Ï∞? 5 : ?®Í?, 6 : ?¨Ï∞Ω??Ï∞?
    public int ArmorType;
    //10 : Í∞ÄÏ£? 11 : Í≤ΩÍ∞ë, 12 : ?êÍ∏à
    
    public int Weapon_Duration;
    public int Armor_Duration;
    
    public List<String> Skill = new List<string>();
    public bool[] clearStage;
    public int PlayerGold = 0;
    public List<int> PlayerInven = new List<int>();
    public List<int> PlayerItemDuraion = new List<int>();
    
}


public class DataManager : MonoBehaviour
{
    public PlayerData playerData = new PlayerData();
    public string path;
    public string fileName = "PlayerData";
    public int StageNum;
    public int SlotNum;

    public static DataManager instance;

    private void Awake()    //?±Í???
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
        path = Application.dataPath + "/Data/PlayerSaveFile/";      // json?åÏùº ?Ä??Í≤ΩÎ°ú
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


