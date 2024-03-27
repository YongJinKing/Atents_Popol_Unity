using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColoUI : MonoBehaviour
{
    public int StageIndex;
    GameObject BossPrefab;
    Sprite BossSkill1;
    Sprite BossSkill2;
    Sprite BossSkill3;
    Sprite BossSkill4;
    void Start()
    {
        MonsterSkillDataManager.GetInstance().LoadSkillUI();
        StageIndex = 1;
        DisplayStageBoss(StageIndex);
    }

    public void DisplayStageBoss(int index)
    {
        var stageTabledata = MonsterSkillDataManager.GetInstance().dicStageTable[index];
        var stageBossPrefabData = MonsterSkillDataManager.GetInstance().dicDisplayTable[stageTabledata.Stage_BossMonster];
        var stageBossSkill1Data = MonsterSkillDataManager.GetInstance().dicResourceTable[stageTabledata.Stage_BossSkill_Image1];
        var stageBossSkill2Data = MonsterSkillDataManager.GetInstance().dicResourceTable[stageTabledata.Stage_BossSkill_Image2];
        var stageBossSkill3Data = MonsterSkillDataManager.GetInstance().dicResourceTable[stageTabledata.Stage_BossSkill_Image3];
        var stageBossSkill4Data = MonsterSkillDataManager.GetInstance().dicResourceTable[stageTabledata.Stage_BossSkill_Image4];

        this.BossPrefab = Resources.Load<GameObject>($"UI/Colosseum/Display/{stageBossPrefabData.Boss_Display_Prefab_Name}");
        if(stageBossSkill1Data.index > 0)
            this.BossSkill1 = Resources.Load<Sprite>($"UI/Colosseum/MonsterSkill/Stage{index + 1}/{stageBossSkill1Data.Skill_ImageResource}");
        if(stageBossSkill2Data.index > 0)
            this.BossSkill2 = Resources.Load<Sprite>($"UI/Colosseum/MonsterSkill/Stage{index + 1}/{stageBossSkill2Data.Skill_ImageResource}");
        if(stageBossSkill3Data.index > 0)
            this.BossSkill3 = Resources.Load<Sprite>($"UI/Colosseum/MonsterSkill/Stage{index + 1}/{stageBossSkill3Data.Skill_ImageResource}");
        if(stageBossSkill4Data.index > 0)
            this.BossSkill4 = Resources.Load<Sprite>($"UI/Colosseum/MonsterSkill/Stage{index + 1}/{stageBossSkill4Data.Skill_ImageResource}");
        Debug.Log(MonsterSkillDataManager.GetInstance().dicStageTable.Count);
    }

    public void PressedBtn(int index)
    {
        if(index == 0)
        {
                
        }
        if(index == 1)
        {

        }
        if(index == 2)
        {

        }
    }
}
