using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    public Image image;
    public Image CoolTime;


    float StartCoolTime = 0;
    float EndCoolTime;
    Coroutine myco;
    public void SetTrigger(int Id, float CoolTime)
    {
        var ConditionData = ConditionDataManager.GetInstance().dicConditionDatas[Id];
        var SpriteData = ConditionDataManager.GetInstance().dicStringTable[ConditionData.Condition_Sprite];
        var NameData = ConditionDataManager.GetInstance().dicStringTable[ConditionData.Condition_Name];
        EndCoolTime = CoolTime;
        if(myco != null)
            StartCoolTime = 0;
        else
            myco = StartCoroutine(ConditionTimeCheck());
        
        
    }
    IEnumerator ConditionTimeCheck()
    {
        //Debug.Log($"실행 및 쿨타임 체크 : ");
        while (StartCoolTime < EndCoolTime)
        {
            StartCoolTime += Time.deltaTime;
            yield return null;
        }
        StartCoolTime = 0;
        myco = null;
        //Debug.Log($"종료 : ");
    }
    
}
