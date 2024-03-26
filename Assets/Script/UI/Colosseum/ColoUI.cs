using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColoUI : MonoBehaviour
{
    public int StageIndex;
    // Start is called before the first frame update
    void Start()
    {
        MonsterSkillDataManager.GetInstance().LoadSkillUI();
        StageIndex = 0;
        DisplayStageBoss(StageIndex);
    }

    public void DisplayStageBoss(int index)
    {
        var stageTable = MonsterSkillDataManager.GetInstance().dicStageTable[index + 1];
        
    }
}
