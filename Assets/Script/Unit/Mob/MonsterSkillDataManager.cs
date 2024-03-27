using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Linq;

public class MonsterSkillDataManager
{
    private static MonsterSkillDataManager instance;
    
    public Dictionary<int, SkillStringTable> dicStringTable;
    public Dictionary<int, BossDisplayTable> dicDisplayTable;
    public Dictionary<int, BossStageTable> dicStageTable;
    public Dictionary<int, SkillImageResourceTable> dicResourceTable;

    public static MonsterSkillDataManager GetInstance()
    {
        if(MonsterSkillDataManager.instance == null)
            MonsterSkillDataManager.instance = new MonsterSkillDataManager();
        return MonsterSkillDataManager.instance;
    }
    public void LoadSkillDatas()
    {
        var Mestiarii_Monster_SkillStringTable = Resources.Load<TextAsset>("Monster/SkillData/Mestiarii_Monster_SkillStringTable").text;

        var arrStringDatas = JsonConvert.DeserializeObject<SkillStringTable[]>(Mestiarii_Monster_SkillStringTable);

        this.dicStringTable = arrStringDatas.ToDictionary(x => x.index);

    }
    public void LoadSkillUI()
    {
        var Mestiarii_BossMonster_Display_Prefab = Resources.Load<TextAsset>("Monster/SkillData/SkillUI/Mestiarii_BossMonster_Display_Prefab").text;
        var Mestiarii_BossStage_Table = Resources.Load<TextAsset>("Monster/SkillData/SkillUI/Mestiarii_BossStage_Table").text;
        var Mestiarii_Skill_ImgaeResource_Table = Resources.Load<TextAsset>("Monster/SkillData/SkillUI/Mestiarii_Skill_ImgaeResource_Table").text;
        var Mestiarii_Monster_SkillStringTable = Resources.Load<TextAsset>("Monster/SkillData/Mestiarii_Monster_SkillStringTable").text;

        var arrDisplayDatas = JsonConvert.DeserializeObject<BossDisplayTable[]>(Mestiarii_BossMonster_Display_Prefab);
        var arrStageDatas = JsonConvert.DeserializeObject<BossStageTable[]>(Mestiarii_BossStage_Table);
        var arrResourceDatas = JsonConvert.DeserializeObject<SkillImageResourceTable[]>(Mestiarii_Skill_ImgaeResource_Table);
        var arrStringDatas = JsonConvert.DeserializeObject<SkillStringTable[]>(Mestiarii_Monster_SkillStringTable);

        this.dicDisplayTable =  arrDisplayDatas.ToDictionary(x => x.index);
        this.dicStageTable =  arrStageDatas.ToDictionary(x => x.index);
        this.dicResourceTable =  arrResourceDatas.ToDictionary(x => x.index);
        this.dicStringTable = arrStringDatas.ToDictionary(x => x.index);
    }
}

