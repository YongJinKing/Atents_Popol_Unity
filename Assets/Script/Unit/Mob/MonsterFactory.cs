using UnityEngine;
using Newtonsoft.Json;

public class MonsterFactory : MonoBehaviour
{
    public static void CreateMonster(int index)
    {

    }

    public static void CreateMonsterSkill(Monster parent ,int index)
    {
        GameObject obj = new GameObject();
        SkillDataStruct data = default;

        //load from file
        var json = Resources.Load<TextAsset>("Monster/SkillData/Monster_SkillData").text;
        var arrMonsterSkillDatas = JsonConvert.DeserializeObject<SkillDataStruct[]>(json);
        foreach(var skillData in arrMonsterSkillDatas)
        {
            if(skillData.Index == index)
            {
                data = skillData;
                break;
            }
        }

        //making skill by recipe
        obj.name = data.Skill_Name;
        Skill objSkill = obj.AddComponent<Skill>();
        objSkill.preDelay = data.Skill_PreDelay;
        objSkill.postDelay = data.Skill_PostDelay;
        objSkill.detectRadius = data.Skill_DetectRange;
        objSkill.targetMask = 1 << data.Skill_TargetMask;

        AddSkillType(parent, objSkill, data.Skill_Option1);
        AddSkillType(parent, objSkill, data.Skill_Option2);
        AddSkillType(parent, objSkill, data.Skill_Option3);
        AddSkillType(parent, objSkill, data.Skill_Option4);

        objSkill.onAddSkillEvent.AddListener(parent.OnAddSkillEventListener);
        objSkill.onAddSkillEvent2.AddListener(parent.OnAddSkillEvent2Listener);

        objSkill.transform.SetParent(parent.transform);
    }

    public static void AddSkillType(Monster monster, Skill parent, int index)
    {
        GameObject obj = new GameObject();


        switch (index / 10000)
        {
            //MovementSkillType
            case 1:
                {
                    SkillMovementTypeDataStruct data = default;
                    //load from file
                    var json = Resources.Load<TextAsset>("Monster/SkillData/SkillType/Monster_SkillType_Movement").text;
                    var arrDatas = JsonConvert.DeserializeObject<SkillMovementTypeDataStruct[]>(json);
                    foreach (var Data in arrDatas)
                    {
                        if (Data.Index == index)
                        {
                            data = Data;
                            break;
                        }
                    }
                    MovementSkillType move = obj.AddComponent<MovementSkillType>();
                    move.maxDist = data.Skill_ShortRangeAttackDist;
                    move.moveSpeed = data.Skill_ShortRangeAttackSpeed;
                    move.onMoveEndEvent.AddListener(parent.OnSkillAnimEnd);
                    move.moveToPosEvent.AddListener(monster.GetComponent<UnitMovement>().MoveToPos);
                    parent.onSkillActivatedEvent.AddListener(move.OnSkillActivated);
                }
                break;
            //MeleeSkillType
            case 2:
                {
                    SkillMeleeTypeDataStruct data = default;
                    //load from file
                    var json = Resources.Load<TextAsset>("Monster/SkillData/SkillType/Monster_SkillType_Melee").text;
                    var arrDatas = JsonConvert.DeserializeObject<SkillMeleeTypeDataStruct[]>(json);
                    foreach (var Data in arrDatas)
                    {
                        if (Data.Index == index)
                        {
                            data = Data;
                            break;
                        }
                    }
                    MeleeSkillType melee = obj.AddComponent<MeleeSkillType>();
                    melee.maxIndex = data.Skill_NumOfHitBox;
                    melee.areaOfEffectPrefeb = FindPrefab(data.Skill_HitBox);
                    melee.attackStartPos[0] = monster.attackStartPos[0];
                    melee.hitDuration = data.Skill_hitDuration;
                    AddSkillAffect(melee, data.Skill_AffectOption1);
                    AddSkillAffect(melee, data.Skill_AffectOption2);
                    AddSkillAffect(melee, data.Skill_AffectOption3);
                    AddSkillAffect(melee, data.Skill_AffectOption4);
                    AddSkillAffect(melee, data.Skill_AffectOption5);
                    parent.onSkillActivatedEvent.AddListener(melee.OnSkillActivated);
                    parent.onSkillHitCheckStartEvent.AddListener(melee.OnSkillHitCheckStartEventHandler);
                    parent.onSkillHitCheckEndEvent.AddListener(melee.OnSkillHitCheckEndEventHandler);
                }
                break;
            //ProjectileSkillType
            case 3:
                {
                    SkillProjectileTypeDataStruct data = default;
                    //load from file
                    var json = Resources.Load<TextAsset>("Monster/SkillData/SkillType/Monster_SkillType_Projectile").text;
                    var arrDatas = JsonConvert.DeserializeObject<SkillProjectileTypeDataStruct[]>(json);
                    foreach (var Data in arrDatas)
                    {
                        if (Data.Index == index)
                        {
                            data = Data;
                            break;
                        }
                    }

                    ProjectileSkillType projectile = obj.AddComponent<ProjectileSkillType>();
                    projectile.maxIndex = data.Skill_NumOfHitBox;
                    projectile.areaOfEffectPrefeb = FindPrefab(data.Skill_HitBox);
                    projectile.attackStartPos[0] = monster.attackStartPos[0];
                    projectile.hitDuration = data.Skill_hitDuration;
                    AddSkillAffect(projectile, data.Skill_AffectOption1);
                    AddSkillAffect(projectile, data.Skill_AffectOption2);
                    AddSkillAffect(projectile, data.Skill_AffectOption3);
                    AddSkillAffect(projectile, data.Skill_AffectOption4);
                    AddSkillAffect(projectile, data.Skill_AffectOption5);
                    parent.onSkillActivatedEvent.AddListener(projectile.OnSkillActivated);
                    parent.onSkillHitCheckStartEvent.AddListener(projectile.OnSkillHitCheckStartEventHandler);
                    parent.onSkillHitCheckEndEvent.AddListener(projectile.OnSkillHitCheckEndEventHandler);

                }
                break;
            //dont have skill type
            default:
                return;
        }

        obj.transform.SetParent(parent.transform);
    }

    public static void AddSkillAffect(BaseSkillType parent, int index)
    {
        switch(index / 10000)
        {
            case 1:
                break;
            default:
                break;
        }
    }

    public static GameObject FindPrefab(int index)
    {
        switch (index / 10000)
        {
            case 1:
                break;
            case 2:
                break;
            default:
                return Instantiate(Resources.Load("Skill/Monster/SlimeBall")as GameObject);
        }
        return null;
    }
}
