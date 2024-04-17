using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayCondition : MonoBehaviour
{
    int index = 0;
    private void Start() 
    {
        ConditionDataManager.GetInstance().ConditionLoadDatas();
    }
    public void Display(int Id, float CoolTime)
    {
        var ConditionData = ConditionDataManager.GetInstance().dicConditionDatas[Id];
        var SpriteData = ConditionDataManager.GetInstance().dicStringTable[ConditionData.Condition_Sprite];
        var NameData = ConditionDataManager.GetInstance().dicStringTable[ConditionData.Condition_Name];
        Instantiate(Resources.Load<GameObject>("UI/UserSkill/Condition"),transform.GetChild(0).GetChild(0).GetChild(0));//DiplayCondition//Bg/Paper/GridLine
        
    }

    IEnumerator ConditionTimeCheck(int index, float CoolTime)
    {
        while(CoolTime > 0)
        {   
            CoolTime -= Time.deltaTime;
            if(CoolTime < 0)
            {
             
                CoolTime = 0.0f;
            }
          
            yield return null;
        }
    }
}
