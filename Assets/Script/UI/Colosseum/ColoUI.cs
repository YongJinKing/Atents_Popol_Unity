using System.Collections;
using System.Collections.Generic;

using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ColoUI : MonoBehaviour
{
    public GameObject BossAbility;
    public int skillLength = 4;


    public int StageIndex;
    List<bool> ClearCheck = new List<bool>();
    GameObject BossPrefab;
    List<Sprite> BossSkillList;
    void Start()
    {
        MonsterSkillDataManager.GetInstance().LoadSkillUI();
        StageIndex = 1;
        DisplayStageBoss(StageIndex);
        for(int i = 0; i < MonsterSkillDataManager.GetInstance().dicStageTable.Count; i++)
        {
            ClearCheck.Add(false);
        }
        ClearCheck[0] = true;
        ClearCheck[1] = true;
    }

    public void DisplayStageBoss(int index)
    {
        
        BossSkillList = new List<Sprite>();
        var stageTableData = MonsterSkillDataManager.GetInstance().dicStageTable[index];
        var stageBossPrefabData = MonsterSkillDataManager.GetInstance().dicDisplayTable[stageTableData.Stage_BossMonster];
        this.BossPrefab = Resources.Load<GameObject>($"UI/Colosseum/Display/{stageBossPrefabData.Boss_Display_Prefab_Name}");
        Instantiate(BossPrefab,BossAbility.transform.Find("BossMonster"));
        for(int i = 0; i < skillLength; i++)
        {   
            try
            {
                var BossSkillData = MonsterSkillDataManager.GetInstance().dicResourceTable[30000 + i + ((index -1) * 100)];
                var SkillStringTable = MonsterSkillDataManager.GetInstance().dicStringTable;
                if(BossSkillData.index > 0)
                {
                    BossSkillList.Add(Resources.Load<Sprite>($"UI/Colosseum/MonsterSkill/Stage{index}/{BossSkillData.Skill_ImageResource}"));
                    var go = BossAbility.transform.Find("BossExplain").GetChild(0).GetChild(0).GetChild(i);
                    go.GetChild(0).GetComponent<Image>().sprite
                    = BossSkillList[i];
                    go.gameObject.SetActive(true);
                    /* Color color = go.GetChild(0).GetComponent<Image>().color;
                    color.a = 1.0f;
                    go.GetChild(0).GetComponent<Image>().color = color;  */
                    
                }
            }
            catch
            {
                var go = BossAbility.transform.Find("BossExplain").GetChild(0).GetChild(0).GetChild(i);
                go.gameObject.SetActive(false);
                /* Color color = go.GetChild(0).GetComponent<Image>().color;
                color.a = 0.0f;
                go.GetChild(0).GetComponent<Image>().color = color;  */
            }
        }
        BossAbility.transform.Find("BossStage").GetComponent<TMP_Text>().text = $"Stage\n{index}";   
    }

    public void PressedBtn(int index)
    {
        if(index == 0)
        {
            if(StageIndex > 1)
            {
                StageIndex--;
                if(BossAbility.transform.Find("BossMonster").GetChild(0).gameObject)
                    Destroy(BossAbility.transform.Find("BossMonster").GetChild(0).gameObject);
                DisplayStageBoss(StageIndex);
            }
        }
        if(index == 1)
        {
            int trueCount = ClearCheck.Where(x => x == true).Count();
            if(StageIndex < trueCount)
            {
                StageIndex++;
                if(BossAbility.transform.Find("BossMonster").GetChild(0).gameObject)
                    Destroy(BossAbility.transform.Find("BossMonster").GetChild(0).gameObject);
                DisplayStageBoss(StageIndex);
            }
            else
            {
                //현재 보스를 클리어 하세요!
            }
        }
        if(index == 2)//GameStart
        {
            
        }
    }
}
