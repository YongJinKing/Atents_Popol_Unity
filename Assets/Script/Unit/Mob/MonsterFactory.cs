using UnityEngine;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;

public class MonsterFactory : MonoBehaviour
{
    public void CreateMonster(int index)
    {
        GameObject obj;
        MonsterDataStruct data = default;

        //load from file
        var json = Resources.Load<TextAsset>("Monster/Monster_Data").text;
        var arrDatas = JsonConvert.DeserializeObject<MonsterDataStruct[]>(json);
        foreach (var Data in arrDatas)
        {
            if (Data.Index == index)
            {
                data = Data;
                break;
            }
        }

        obj = FindPrefab(data.Character_Prefab);
        Monster objMon = obj.GetComponent<Monster>();

        BattleStat bs = default;
        bs.HP = data.Character_Hp;
        bs.ATK = data.Character_AttackPower;
        bs.Speed = data.Character_MoveSpeed;
        objMon.battlestat = bs;


        List<Skill> skillList = new List<Skill>();
        if(data.Skill_Index1 / 10000 > 0)
        {
            skillList.Add(CreateMonsterSkill(objMon, data.Skill_Index1));
            if(data.Skill_Index2 / 10000 > 0)
            {
                skillList.Add(CreateMonsterSkill(objMon, data.Skill_Index2));
                if (data.Skill_Index3 /10000 >0)
                {
                    skillList.Add(CreateMonsterSkill(objMon, data.Skill_Index3));
                    if ((data.Skill_Index4 /10000) > 0)
                    {
                        skillList.Add(CreateMonsterSkill(objMon, data.Skill_Index4));
                    }
                }
            }
        }

        objMon.skills = new Skill[skillList.Count];

        for(int i = 0; i < skillList.Count; i++)
        {
            objMon.skills[i] = skillList[i];
        }
    }

    public Skill CreateMonsterSkill(Monster parent ,int index)
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
        objSkill.isLoopAnim = data.Skill_IsLoopAttackAnim;

        AddSkillType(parent, objSkill, data.Skill_Option1);
        AddSkillType(parent, objSkill, data.Skill_Option2);
        AddSkillType(parent, objSkill, data.Skill_Option3);
        AddSkillType(parent, objSkill, data.Skill_Option4);

        objSkill.onAddSkillEvent.AddListener(parent.OnAddSkillEventListener);
        objSkill.onAddSkillEvent2.AddListener(parent.OnAddSkillEvent2Listener);

        obj.transform.SetParent(parent.transform);

        return objSkill;
    }

    public void AddSkillType(Monster monster, Skill parent, int index)
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
                    obj.name = "Movement";
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
                    obj.name = "Melee";
                    MeleeSkillType melee = obj.AddComponent<MeleeSkillType>();
                    melee.maxIndex = data.Skill_NumOfHitBox;
                    melee.areaOfEffectPrefeb = FindPrefab(data.Skill_HitBox);
                    melee.targetMask = 1 << data.Skill_TargetMask;
                    melee.attackStartPos = new Transform[1];
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

                    obj.name = "Projectile";
                    ProjectileSkillType projectile = obj.AddComponent<ProjectileSkillType>();
                    projectile.maxIndex = data.Skill_NumOfHitBox;
                    projectile.areaOfEffectPrefeb = FindPrefab(data.Skill_HitBox);
                    projectile.unPenetrableMask = 1 << data.Skill_Unpenetrable;
                    projectile.targetMask = 1 << data.Skill_TargetMask;
                    projectile.moveSpeed = data.Skill_LongRangeAttackSpeed;
                    projectile.maxDist = data.Skill_LongRangeAttackSpeed;
                    projectile.attackStartPos = new Transform[data.Skill_NumOfHitBox];
                    projectile.penetrable = data.Skill_Penetrable;


                    switch (data.Skill_NumOfHitBox)
                    {
                        case 4:
                            projectile.attackStartPos[3] = monster.attackStartPos[data.Skill_HitBoxStartPos4];
                            goto case 3;
                        case 3:
                            projectile.attackStartPos[2] = monster.attackStartPos[data.Skill_HitBoxStartPos3];
                            goto case 2;
                        case 2:
                            projectile.attackStartPos[1] = monster.attackStartPos[data.Skill_HitBoxStartPos2];
                            goto case 1;
                        case 1:
                            projectile.attackStartPos[0] = monster.attackStartPos[data.Skill_HitBoxStartPos1];
                            break;
                    }

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

    public void AddSkillAffect(BaseSkillType parent, int index)
    {
        GameObject obj = new GameObject();

        switch (index / 10000)
        {
            case 1:
                SkillDamageAffectDataStruct data = default;
                var json = Resources.Load<TextAsset>("Monster/SkillData/SkillAffect/Monster_SkillAffect_DamageAffect").text;
                var arrDatas = JsonConvert.DeserializeObject<SkillDamageAffectDataStruct[]>(json);
                foreach (var Data in arrDatas)
                {
                    if (Data.Index == index)
                    {
                        data = Data;
                        break;
                    }
                }

                obj.name = "Damage";
                DamageSkillEffect damage = obj.AddComponent<DamageSkillEffect>();
                damage.power = data.Skill_Power;
                damage.Atype = (AttackType)data.Skill_AttackType;
                parent.GetComponent<HitCheckSkillType>().onSkillHitEvent.AddListener(damage.OnSkillHit);
                break;
            default:
                break;
        }
    }

    public GameObject FindPrefab(int index)
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
