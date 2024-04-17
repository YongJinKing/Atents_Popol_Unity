using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class Condition : MonoBehaviour
{
    public Image image;
    public Image CoolTimeImage;
    public TMP_Text ConditionCountTxt;


    float StartDurationTime = 0;
    float EndDurationTime;
    int ConditionCount = 1;

    public int ConditionId;
    string Spname;
    Coroutine myco;
    private void Start() 
    {
        ConditionCountTxt.text = "";
        ConditionDataManager.GetInstance().ConditionLoadDatas();
    }
    public void SetTrigger(int Id)//cooltime = endtime
    {
        this.ConditionId = Id;
        var ConditionData = ConditionDataManager.GetInstance().dicConditionDatas[Id];
        var SpriteData = ConditionDataManager.GetInstance().dicResouseTable[ConditionData.Condition_Sprite];
        var NameData = ConditionDataManager.GetInstance().dicStringTable[ConditionData.Condition_Name];
        Spname = SpriteData.ImageResourceName;
        Sprite sp = Resources.Load<Sprite>($"UI/BuffAndDeBuff/{ItemTypeIntToString.IntToStringConditionFileName(Id/1000)}/{Spname}");
        image.sprite = sp;
        EndDurationTime = ConditionData.Condition_DurationTime;
        if(myco != null)
        {
            ConditionCount++;
        }
        else
            myco = StartCoroutine(ConditionTimeCheck());
        
        
    }
    IEnumerator ConditionTimeCheck()
    {
        //Debug.Log($"실행 및 쿨타임 체크 : ");
        while (ConditionCount > 0)
        {
            StartDurationTime += Time.deltaTime;
            CoolTimeImage.fillAmount = StartDurationTime/EndDurationTime;
            ConditionCountTxt.text = "x" +ConditionCount.ToString();
            if(StartDurationTime >= EndDurationTime)
            {
                ConditionCount--;
                StartDurationTime = 0;
            }
            yield return null;
        }
        ConditionCount = 1;
        StartDurationTime = 0;
        CoolTimeImage.fillAmount = 0.0f;
        myco = null;
        ConditionCountTxt.text = "";
        Destroy(gameObject);
        //Debug.Log($"종료 : ");
    }
    
}
