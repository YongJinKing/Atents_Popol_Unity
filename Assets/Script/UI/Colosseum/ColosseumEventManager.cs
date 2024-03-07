using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using Unity.VisualScripting;







public class ColosseumEventManager : MonoBehaviour
{
    
    UnityAction SlotDesTroyAll;
    
    public GameObject ColoBtn;
    public GameObject BossState;
    public GameObject HideBoss;
    public GameObject BossStage;
    public GameObject SkillSlot;
    public GameObject BossAbility;
    public GameObject Popup;

    public GameObject Obj_Slime_Lv1;
    public GameObject Obj_Slime_Lv2;
    enum BossMonsterType
    {
        None,
        Slime_Lv1,
        TurtleShell_Lv1,
        Slime_Lv2,
        TurtleShell_Lv2,
        Slime_Lv3,
        TurtleShell_Lv3,
    }

    public class SkillBtn
    {
        public GameObject gameObject;
        public bool ChooseBtn;
    }

    int BossMonsterDisplayer;
    bool[] isClearBoss = new bool[System.Enum.GetValues(typeof(BossMonsterType)).Length];
    public List<GameObject> Slime_Lv1SkillList = new List<GameObject>();
    public List<GameObject> Slime_Lv2SkillList = new List<GameObject>();
    private List<SkillBtn> SkillBtnList = new List<SkillBtn>();

    // Start is called before the first frame update
    void Start()
    {
        isClearBoss[0] = true;
        isClearBoss[1] = true;
        BossMonsterDisplayer = 1;
        HideBoss.transform.gameObject.SetActive(false);
        Popup.transform.gameObject.SetActive(false);
        ChangeDisplay();

        Button[] ColoBtnList = ColoBtn.GetComponentsInChildren<UnityEngine.UI.Button>();//MainUI버튼
        for(int i = 0; i < ColoBtnList.Length; i++)
        {
            int index = i;
            ColoBtnList[i].onClick.AddListener(() => StageBtnManager(index));
        }
        for(int i = 0; i < BossAbility.transform.GetChild(0).childCount; i++)
        {
            int index = i;
            SkillBtn temp = new SkillBtn();
            temp.gameObject = BossAbility.transform.GetChild(0).GetChild(i).GetChild(1).gameObject;
            temp.ChooseBtn = false;
            SkillBtnList.Add(temp);
            SkillBtnList[i].gameObject.GetComponent<Button>().onClick.AddListener(()=>SkillSlotBtnChoose(index));

        }
    }
    


    void StageBtnManager(int index)
    {
        
        if(index == 0)
        {
            if(BossMonsterDisplayer > 1)
            {
                StageBtnStatus(-1);
            }
        }
        if(index == 1)
        {
            if(BossMonsterDisplayer < System.Enum.GetValues(typeof(BossMonsterType)).Length)
            {
                StageBtnStatus(1);
            }
        }
        BossStage.gameObject.GetComponent<TMP_Text>().text =
        $"Stage\n{BossMonsterDisplayer}";
    }
    void StageBtnStatus(int isMinus)
    {
        if(BossState.transform.GetChild(0).childCount != 0)
            Destroy(BossState.transform.GetChild(0).GetChild(0).gameObject);
        BossMonsterDisplayer += isMinus;
        CleanSkillBtn();
        ChangeDisplay();

    }
    void ChangeDisplay()
    {
        switch(BossMonsterDisplayer)
        {
            case 1:
                DisplayBossMonster(BossMonsterType.Slime_Lv1, isClearBoss[0]);
                break;
            case 2:
                DisplayBossMonster(BossMonsterType.Slime_Lv2, isClearBoss[1]);
                break;
            case 3:
                DisplayBossMonster(BossMonsterType.Slime_Lv2, isClearBoss[2]);
                break;

        }
    }
    void DisplayBossMonster(BossMonsterType B, bool ClearCheck)
    {
        HideBoss.transform.gameObject.SetActive(false);
        if(B == BossMonsterType.Slime_Lv1 && ClearCheck)
        {
            DisplayState(Obj_Slime_Lv1, "슬라임", "가죽", "콜로세움에서 전투용으로 키운 슬라임이다.", Slime_Lv1SkillList);
        }
        else if(B == BossMonsterType.Slime_Lv2 && ClearCheck)
        {
            DisplayState(Obj_Slime_Lv2, "거부깅", "판금", "콜로세움에서 전투용으로 키운 거부깅이다.", Slime_Lv2SkillList);
        }
        else if(!ClearCheck)
        {
            HideBoss.transform.gameObject.SetActive(true);
        }
    }
    void DisplayState(GameObject boss, string Bossname, string BossArmor, string BossDesc, List<GameObject> SkillList)
    {
        Instantiate(boss,BossState.transform.GetChild(0));
        BossState.transform.GetChild(1).GetChild(0).gameObject.GetComponent<TMP_Text>().text = //보스 어빌리티
        $"보스 이름 : {Bossname}\n\n방어 타입 : {BossArmor}\n\n 스킬 :\n\n패시브 :";
        BossState.transform.GetChild(1).GetChild(1).gameObject.GetComponent<TMP_Text>().text = // 보스 설명
        $"설명 :\n{BossDesc}";
        int i = 0;
        for(; i < SkillList.Count; i++)
        {
            Debug.Log(SkillList.Count);
            BossAbility.transform.GetChild(0).GetChild(i).GetChild(0).gameObject.GetComponent<Image>().sprite = 
            SkillList[i].transform.gameObject.GetComponent<Skill>().uiSkillStatus.uiSkillSprite;
            
            BossAbility.transform.GetChild(0).GetChild(i).GetChild(0).gameObject.GetComponent<Image>().color = 
            AlphaColorChange(1.0f, BossAbility.transform.GetChild(0).GetChild(i).GetChild(0).gameObject.GetComponent<Image>().color);
        }
        for(; i < BossAbility.transform.GetChild(0).childCount; i++)
        {   
            BossAbility.transform.GetChild(0).GetChild(i).GetChild(0).gameObject.GetComponent<Image>().color = 
            AlphaColorChange(0.0f, BossAbility.transform.GetChild(0).GetChild(i).GetChild(0).gameObject.GetComponent<Image>().color);
        }
    }
    void SkillSlotBtnChoose(int index)
    {
        if(BossAbility.transform.GetChild(0).GetChild(index).GetChild(0).gameObject.GetComponent<Image>().sprite == null)
        {
            return;
        }
        if(SkillBtnList[index].ChooseBtn)
        {
            CleanSkillBtn();
            return;
        }
        else
        {
            CleanSkillBtn();
            SkillBtnList[index].ChooseBtn = true;   
        }
        
        if(SkillBtnList[index].ChooseBtn)
        {
            SkillBtnList[index].gameObject.GetComponent<Image>().color = 
            AlphaColorChange(0.3f, SkillBtnList[index].gameObject.GetComponent<Image>().color);
        }
    }


    void CleanSkillBtn()
    {
        for(int i = 0; i < SkillBtnList.Count; i++)
        {
            SkillBtnList[i].ChooseBtn = false;
            SkillBtnList[i].gameObject.GetComponent<Image>().color = 
            AlphaColorChange(0.0f, SkillBtnList[i].gameObject.GetComponent<Image>().color);
        }
    }

   Color AlphaColorChange(float Value, Color Objcolor)
    {
        Color color = Objcolor;//
        color.a = Value;
        Objcolor = color;
        return Objcolor;
    }
    
    
}
