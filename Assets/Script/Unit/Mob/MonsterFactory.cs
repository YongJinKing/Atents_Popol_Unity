using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;

public class MonsterFactory
{
    public GameObject CreateMonster(int index)
    {
        GameObject obj = new GameObject();
        MonsterDataStruct data = default;

        //load from file
        var json = Resources.Load<TextAsset>("Monster/Character_Ability_Monster").text;
        var arrDatas = JsonConvert.DeserializeObject<MonsterDataStruct[]>(json);
        foreach (var Data in arrDatas)
        {
            if (Data.Index == index)
            {
                data = Data;
                break;
            }
        }

        //����ó�� �ڸ�
        //if(data == default)
        //{
        //    return;
        //}

        
        
        obj.transform.localScale = new Vector3(3, 3, 3);
        obj.name = "Slime2";
        BoxCollider col = obj.AddComponent<BoxCollider>();
        col.center = new Vector3(0, 0.5f, 0);
        col.size = new Vector3(0.5f, 1.0f, 0.5f);
        Rigidbody rigid = obj.AddComponent<Rigidbody>();
        rigid.useGravity = true;
        rigid.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        //���߿� Slime�� �������� Turtle�� �������� �ε��� �ʿ�
        Monster objMon = obj.AddComponent<Slime>();
        objMon.onMovementEvent = new UnityEngine.Events.UnityEvent<Vector3, float, UnityEngine.Events.UnityAction, UnityEngine.Events.UnityAction>();
        objMon.followEvent = new UnityEngine.Events.UnityEvent<Transform, float, UnityEngine.Events.UnityAction, UnityEngine.Events.UnityAction>();
        objMon.rotateEvent = new UnityEngine.Events.UnityEvent<Vector3, float>();
        objMon.sideMoveEvent = new UnityEngine.Events.UnityEvent<Transform, float, UnityEngine.Events.UnityAction, UnityEngine.Events.UnityAction>();
        objMon.stopEvent = new UnityEngine.Events.UnityEvent<UnityEngine.Events.UnityAction>();

        UnitMovement objMove = obj.AddComponent<UnitMovement>();
        objMon.onMovementEvent.AddListener(objMove.MoveToPos);
        objMon.followEvent.AddListener(objMove.FollowTarget);
        objMon.rotateEvent.AddListener(objMove.Rotate);
        objMon.sideMoveEvent.AddListener(objMove.SideMove);
        objMon.stopEvent.AddListener(objMove.StopMove);

        //GameObject prefab = FindPrefab(data.Character_Prefab);
        GameObject prefab = GameObject.Instantiate(Resources.Load<GameObject>("Monster/MonsterPrefabs/Prefab_Stage1_Slime"));
        prefab.transform.SetParent(obj.transform, false);
        PartManager part = prefab.GetComponent<PartManager>();
        objMon.attackStartPos = new Transform[part.attackStartPos.Length];
        for (int i = 0; i < part.attackStartPos.Length; i++)
        {
            objMon.attackStartPos[i] = part.attackStartPos[i];
        }
        for(int i = 0; i< part.parts.Length; i++)
        {
            part.parts[i].col.gameObject.layer = LayerMask.NameToLayer("Monster_Body");
            switch (i)
            {
                case 0:
                    part.parts[i].type = (DefenceType)data.Character_DetailArmorType1;
                    break;
                case 1:
                    part.parts[i].type = (DefenceType)data.Character_DetailArmorType2;
                    break;
                case 2:
                    part.parts[i].type = (DefenceType)data.Character_DetailArmorType3;
                    break;
                case 3:
                    part.parts[i].type = (DefenceType)data.Character_DetailArmorType4;
                    break;
            }
        }

        MonsterAnimEvent anim = prefab.GetComponent<MonsterAnimEvent>();
        anim.onAttackStartEvent = new UnityEngine.Events.UnityEvent();
        anim.onAttackEndEvent = new UnityEngine.Events.UnityEvent();
        anim.onAttackAnimEndEvent = new UnityEngine.Events.UnityEvent();

        anim.onAttackStartEvent.AddListener(objMon.OnAttackStartAnim);
        anim.onAttackEndEvent.AddListener(objMon.OnAttackEndAnim);
        anim.onAttackAnimEndEvent.AddListener(objMon.OnSkillAnimEnd);

        

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
        skillList.Clear();

        return obj;
    }

    public Skill CreateMonsterSkill(Monster parent ,int index)
    {
        
        SkillDataTable data = default;

        //load from file
        var json = Resources.Load<TextAsset>("Monster/SkillData/Mestiarii_Monster_SkillTable").text;
        var arrMonsterSkillDatas = JsonConvert.DeserializeObject<SkillDataTable[]>(json);
        foreach(var skillData in arrMonsterSkillDatas)
        {
            if(skillData.Index == index)
            {
                Debug.Log("Find Index");
                data = skillData;
                break;
            }
        }

        //����ó�� �ڸ�
        //if(data == default)
        //{
        //    return;
        //}

        GameObject obj = new GameObject();

        //making skill by recipe
        obj.name = data.Skill_Name.ToString();
        Skill objSkill = obj.AddComponent<Skill>();
        objSkill.preDelay = data.Skill_PreDelay;
        objSkill.postDelay = data.Skill_PostDelay;
        objSkill.detectRadius = data.Skill_DetectRange;
        objSkill.targetMask = 1 << data.Skill_TargetMask;
        objSkill.isLoopAnim = data.Skill_IsLoopAttackAnim;

        objSkill.onSkillActivatedEvent = new UnityEngine.Events.UnityEvent<Vector3>();
        objSkill.onSkillHitCheckStartEvent = new UnityEngine.Events.UnityEvent();
        objSkill.onSkillHitCheckEndEvent = new UnityEngine.Events.UnityEvent();

        AddSkillType(parent, objSkill, data.Skill_Option1);
        AddSkillType(parent, objSkill, data.Skill_Option2);
        AddSkillType(parent, objSkill, data.Skill_Option3);
        AddSkillType(parent, objSkill, data.Skill_Option4);

        objSkill.onAddSkillEvent = new UnityEngine.Events.UnityEvent<UnityEngine.Events.UnityAction<Vector3, UnityEngine.Events.UnityAction, UnityEngine.Events.UnityAction, UnityEngine.Events.UnityAction>, UnityEngine.Events.UnityAction, UnityEngine.Events.UnityAction, UnityEngine.Events.UnityAction>();
        objSkill.onAddSkillEvent2 = new UnityEngine.Events.UnityEvent<UnityEngine.Events.UnityAction, bool, LayerMask>();

        objSkill.onAddSkillEvent.AddListener(parent.OnAddSkillEventListener);
        objSkill.onAddSkillEvent2.AddListener(parent.OnAddSkillEvent2Listener);

        obj.transform.SetParent(parent.transform, false);
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
                    SkillMovementTypeDataTable data = default;
                    //load from file
                    var json = Resources.Load<TextAsset>("Monster/SkillData/SkillType/Monster_SkillDetail_Movement").text;
                    var arrDatas = JsonConvert.DeserializeObject<SkillMovementTypeDataTable[]>(json);
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

                    move.onMoveEndEvent = new UnityEngine.Events.UnityEvent();
                    move.moveToPosEvent = new UnityEngine.Events.UnityEvent<Vector3, float, UnityEngine.Events.UnityAction, UnityEngine.Events.UnityAction>();

                    move.onMoveEndEvent.AddListener(parent.OnSkillAnimEnd);
                    move.moveToPosEvent.AddListener(monster.GetComponent<UnitMovement>().MoveToPos);
                    parent.onSkillActivatedEvent.AddListener(move.OnSkillActivated);
                }
                break;
            //MeleeSkillType
            case 2:
                {
                    SkillMeleeTypeDataTable data = default;
                    //load from file
                    var json = Resources.Load<TextAsset>("Monster/SkillData/SkillType/Monster_SkillDetail_Melee").text;
                    var arrDatas = JsonConvert.DeserializeObject<SkillMeleeTypeDataTable[]>(json);
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
                    melee.attackStartPos = new Transform[data.Skill_NumOfHitBox];
                    for(int i = 0; i < data.Skill_NumOfHitBox; i++)
                    {
                        melee.attackStartPos[i] = monster.attackStartPos[0];
                    }
                    melee.hitDuration = data.Skill_hitDuration;

                    melee.onSkillHitEvent = new UnityEngine.Events.UnityEvent<Collider>();
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
                    SkillProjectileDetailDataTable data = default;
                    //load from file
                    var json = Resources.Load<TextAsset>("Monster/SkillData/SkillType/Monster_SkillDetail_Projectile").text;
                    var arrDatas = JsonConvert.DeserializeObject<SkillProjectileDetailDataTable[]>(json);
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

                    projectile.onSkillHitEvent = new UnityEngine.Events.UnityEvent<Collider>();
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
                GameObject.Destroy(obj);
                //Debug.Log($"SkillType Add Fail Index is {index}");
                return;
        }

        obj.transform.SetParent(parent.transform, false);
    }

    public void AddSkillAffect(BaseSkillType parent, int index)
    {
        GameObject obj = new GameObject();

        switch (index / 10000)
        {
            case 1:
                SkillDamageAffectDataTable data = default;
                var json = Resources.Load<TextAsset>("Monster/SkillData/SkillAffect/Monster_SkillAffect_DamageAffect").text;
                var arrDatas = JsonConvert.DeserializeObject<SkillDamageAffectDataTable[]>(json);
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
                GameObject.Destroy(obj);
                return;
        }


        obj.transform.SetParent(parent.transform, false);
    }

    public GameObject FindPrefab(int index)
    {
        switch (index / 10000)
        {
            case 1:
                return Resources.Load("Monster/SkillEffect/HitBox")as GameObject;
            case 2:
                break;
            default:
                return Resources.Load("Monster/SkillEffect/SlimeBall") as GameObject;
        }
        return null;
    }
}
