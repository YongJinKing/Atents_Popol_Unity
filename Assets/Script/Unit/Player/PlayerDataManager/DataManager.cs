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

    public string NowWeapon;
    public string NowArmor;
    public string[] Skill;
    public bool[] clearStage;
    public int PlayerGold;
}


public class DataManager : MonoBehaviour
{
    public int PlayerGold = 6000;
    public static DataManager instance;
    public PlayerData playerData = new PlayerData();
    public string path;
    public string fileName = "PlayerData";
    public int StageNum;
    public int SlotNum;
    
    
    private void Awake()    //싱글톤
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        path = Application.dataPath + "/Data/PlayerSaveFile/";      // json파일 저장 경로
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


