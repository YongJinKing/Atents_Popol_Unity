using UnityEngine;

public class DisplayCondition : MonoBehaviour
{
    public GameObject GridLine;
    private void Start() 
    {
        ConditionDataManager.GetInstance().ConditionLoadDatas();
    }
    public void Display(int Id)
    {
        var go = GridLine.transform;
        for(int i = 0; i < go.childCount; i++)
        {   
            if(go.GetChild(i).GetComponent<Condition>().ConditionId == Id)
            {
                go.GetChild(i).GetComponent<Condition>().SetTrigger(Id);
                return;
            }
        }
        Instantiate(Resources.Load<GameObject>("UI/UserSkill/Condition"), go);
        //Debug.Log(go.childCount);
        go.GetChild(go.childCount - 1).GetComponent<Condition>().SetTrigger(Id);

    }
}
