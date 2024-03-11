using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SampleGame : MonoBehaviour
{
    public Text Lvtext;
    public Text slotNum;
    string fstSkill;
    string secSkill;

    public void Start()
    {
        Lvtext.text = DataManager.instance.playerData.Level.ToString();
        slotNum.text = "Slot " + (DataManager.instance.SlotNum + 1).ToString();
    }
    public void LvUPButton()
    {
        DataManager.instance.playerData.Level++;
        Lvtext.text = DataManager.instance.playerData.Level.ToString();
    }
    public void BackScreen()
    {
        SceneManager.LoadScene(0);
    }


    public void SaveData()
    {
        DataManager.instance.SaveData();
    }
}
