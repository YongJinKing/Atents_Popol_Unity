using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System.Security.Cryptography;
using System;

public class ColosseumEventManager : MonoBehaviour
{

    public GameObject ColoBtn;
    public GameObject BossState;
    public GameObject Obj_Slime_Lv1;
    enum BossMonsterType
    {
        None,
        Slime_Lv1,
        TurtleShell_Lv1,
        Slime_Lv2,
        TurtleShell_Lv2,
        Slime_Lv3,
        TurtleShell_Lv3,
    }

    int BossMonsterDisplayer;
    bool[] isClearBoss = new bool[System.Enum.GetValues(typeof(BossMonsterType)).Length];
    // Start is called before the first frame update
    void Start()
    {
        isClearBoss[0] = true;
        BossMonsterDisplayer = 1;
        ChangeDisplay();
        Button[] ColoBtnList = ColoBtn.GetComponentsInChildren<UnityEngine.UI.Button>();//MainUI버튼
        for(int i = 0; i < ColoBtnList.Length; i++)
        {
            int index = i;
            ColoBtnList[i].onClick.AddListener(() => ColoBtnManager(index));
        }
    }
    void ChangeDisplay()
    {
        switch(BossMonsterDisplayer)
        {
            case 1:
            DisplayBossMonster(BossMonsterType.Slime_Lv1, isClearBoss[0]);
                break;

        }
    }
    void DisplayBossMonster(BossMonsterType B, bool ClearCheck)
    {
        if(B == BossMonsterType.Slime_Lv1 && ClearCheck)
        {
            DisplayState(Obj_Slime_Lv1);
        }
    }
    void DisplayState(GameObject boss)
    {
        Instantiate(boss,BossState.transform.GetChild(0));
    }

    void ColoBtnManager(int index)
    {
        if(index == 0)
        {
            if(BossMonsterDisplayer > 1)
            {
                BossMonsterDisplayer--;
            }
        }
        if(index == 1)
        {
            if(BossMonsterDisplayer < System.Enum.GetValues(typeof(BossMonsterType)).Length)
            {
                BossMonsterDisplayer++;
            }
        }
    }

    void Update()
    {

    }
    
}
