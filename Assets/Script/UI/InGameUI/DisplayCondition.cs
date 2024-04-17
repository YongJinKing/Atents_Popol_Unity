using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayCondition : MonoBehaviour
{
    private void Start() 
    {
        ConditionDataManager.GetInstance().ConditionLoadDatas();
    }
    public void Display(int Id)
    {
        var go = transform.GetChild(0).GetChild(0).GetChild(0);//DiplayCondition//Bg/Paper/GridLine
        for(int i = 0; i < go.childCount; i++)
        {   
            if(go.GetChild(i).GetComponent<Condition>().ConditionId == Id)
            {
                go.GetChild(i).GetComponent<Condition>().SetTrigger(Id);
                return;
            }
        }
        Instantiate(Resources.Load<GameObject>("UI/UserSkill/Condition"), go);
        Debug.Log(go.childCount);
        go.GetChild(go.childCount - 1).GetComponent<Condition>().SetTrigger(Id);

    }
}
