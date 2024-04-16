using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayCondition : MonoBehaviour
{
    private void Start() 
    {
        ConditionDataManager.GetInstance().ConditionLoadDatas();
    }
    public void Display(int Id, int CoolTime)
    {
        var ConditionData = ConditionDataManager.GetInstance().dicConditionDatas[Id];
        var SpriteData = ConditionDataManager.GetInstance().dicStringTable[ConditionData.Condition_Sprite];
        var NameData = ConditionDataManager.GetInstance().dicStringTable[ConditionData.Condition_Name];
    }
}
