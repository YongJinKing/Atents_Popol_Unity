using System.Collections;
using System.Collections.Generic;

using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ColoUI : MonoBehaviour
{
    public GameObject BossAbility;
    public GameObject StagePopup;
    public UnityEvent SaveRiggingItemData;
    public int skillLength = 4;


    public int StageIndex;
    List<bool> ClearCheck = new List<bool>();
    GameObject BossPrefab;
    Coroutine CoroutineStagePopup;
    List<Sprite> BossSkillList;
    void Start()
    {
        MonsterSkillDataManager.GetInstance().LoadSkillUI();
        StageIndex = 1;
        StagePopup.gameObject.SetActive(false);
        DisplayStageBoss(StageIndex);
        for(int i = 0; i < MonsterSkillDataManager.GetInstance().dicStageTable.Count; i++)
        {
            ClearCheck.Add(false);
        }
        ClearCheck[0] = true;
    }

    public void DisplayStageBoss(int index)
    {
        
        BossSkillList = new List<Sprite>();
        var stageTableData = MonsterSkillDataManager.GetInstance().dicStageTable[index];
        var stageBossPrefabData = MonsterSkillDataManager.GetInstance().dicPrefabTable[stageTableData.Stage_BossMonster];
        
        this.BossPrefab = Resources.Load<GameObject>($"Monster/MonsterPrefabs/{stageBossPrefabData.Prefab_Name}");
        GameObject obj = Instantiate(BossPrefab,BossAbility.transform.Find("BossMonster"));
        obj.transform.localScale = new Vector3(400,400,400);
        obj.layer = (int)LayerMask.NameToLayer("UI");
        ChangeLayerRecursively(obj, obj.layer);//child Layer Change
        
        for(int i = 0; i < skillLength; i++)
        {   
            try
            {
                var BossSkillData = MonsterSkillDataManager.GetInstance().dicResourceTable[30000 + i + ((index -1) * 100)];
                var BossNameData = MonsterSkillDataManager.GetInstance().dicStringTable[stageTableData.Stage_BossMonster_Name];
                var BossDescData = MonsterSkillDataManager.GetInstance().dicStringTable[stageTableData.Stage_BossMonster_Desc];
                var BossTypeData = MonsterSkillDataManager.GetInstance().dicStringTable[stageTableData.Stage_BossMonster_Type];
                string BossNameText = BossNameData.String_Desc;
                string BossDescText = BossDescData.String_Desc;
                string BossTypeText = BossTypeData.String_Desc;
                BossAbility.transform.Find("BossExplain").Find("Ability").Find("BossName").GetComponent<TMP_Text>().text
                = "보스 이름 : " + BossNameText;
                BossAbility.transform.Find("BossExplain").Find("Ability").Find("BossType").GetComponent<TMP_Text>().text
                = "보스 타입 : " + BossTypeText;
                BossAbility.transform.Find("BossExplain").Find("Ability").Find("BossExplain").GetComponent<TMP_Text>().text
                = "설명 :\n" + BossDescText;
                

                if(BossSkillData.index > 0)
                {
                    BossSkillList.Add(Resources.Load<Sprite>($"UI/Colosseum/MonsterSkill/Stage{index}/{BossSkillData.Skill_ImageResource}"));
                    var go = BossAbility.transform.Find("BossExplain").Find("Ability").Find("BossSkill").Find("GridLine").GetChild(i);
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
                var go = BossAbility.transform.Find("BossExplain").Find("Ability").Find("BossSkill").Find("GridLine").GetChild(i);
                go.gameObject.SetActive(false);
                /* Color color = go.GetChild(0).GetComponent<Image>().color;
                color.a = 0.0f;
                go.GetChild(0).GetComponent<Image>().color = color;  */
            }
        }
        BossAbility.transform.Find("BossStage").GetComponent<TMP_Text>().text = $"Stage\n{index}";   
    }

    private void ChangeLayerRecursively(GameObject obj, int layer)
    {
        obj.layer = layer;
        foreach(Transform child in obj.transform)
        {
            ChangeLayerRecursively(child.gameObject, layer);
        }
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
                StagePopup.gameObject.SetActive(true);
                BossAbility.transform.Find("BossMonster").gameObject.SetActive(false);
                CoroutineStagePopup = StartCoroutine(OnStagePopup());
                //You should clear this boss
            }
        }
        if(index == 2)//GameStart
        {
            DataManager.instance.StageNum = StageIndex;
            SaveRiggingItemData?.Invoke();
            SceneLoading.SceneNum(3);
            SceneManager.LoadScene(1);
        }
    }
    IEnumerator OnStagePopup()
    {
        yield return new WaitForSeconds(0.5f);
        StagePopup.gameObject.SetActive(false);
        BossAbility.transform.Find("BossMonster").gameObject.SetActive(true);
    }
}
