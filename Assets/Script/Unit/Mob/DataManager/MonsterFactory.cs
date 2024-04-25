using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;
using Unity.VisualScripting;

public class MonsterFactory
{
    private MonsterSkillDataManager monsterDataManager = MonsterSkillDataManager.GetInstance();

    public MonsterFactory() 
    {
        monsterDataManager.LoadMonsterMakingDatas();
    }

    ~MonsterFactory()
    {
        monsterDataManager = null;
    }

    public GameObject CreateMonster(int index)
    {
        GameObject obj = new GameObject();
        MonsterDataTable data = default;

        //load from file
        if (monsterDataManager.dicMonsterDataTable.ContainsKey(index))
        {
            data = monsterDataManager.dicMonsterDataTable[index];
        }
        else
        {
            //nullCheck
        }

        /*
        var json = Resources.Load<TextAsset>("Monster/Character_Ability_Monster").text;
        var arrDatas = JsonConvert.DeserializeObject<MonsterDataTable[]>(json);
        foreach (var Data in arrDatas)
        {
            if (Data.Index == index)
            {
                data = Data;
                break;
            }
        }
        */


        float scale = data.Character_Scale;
        obj.transform.localScale = new Vector3(scale, scale, scale);
        obj.name = "Slime2";
        CapsuleCollider col = obj.AddComponent<CapsuleCollider>();
        col.center = new Vector3(0, 0.5f, 0);
        col.radius = 0.5f;
        col.height = 1.0f;
        Rigidbody rigid = obj.AddComponent<Rigidbody>();
        rigid.useGravity = true;
        rigid.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

        //need exl
        Monster objMon = FindAI(obj,data.Character_AIType);

        objMon.onMovementEvent = new UnityEngine.Events.UnityEvent<Vector3, float, UnityEngine.Events.UnityAction, UnityEngine.Events.UnityAction>();
        objMon.followEvent = new UnityEngine.Events.UnityEvent<Transform, Info<float, float>, UnityEngine.Events.UnityAction, UnityEngine.Events.UnityAction>();
        objMon.rotateEvent = new UnityEngine.Events.UnityEvent<Vector3, float>();
        objMon.sideMoveEvent = new UnityEngine.Events.UnityEvent<Transform, Info<float,float>, UnityEngine.Events.UnityAction, UnityEngine.Events.UnityAction>();
        objMon.stopEvent = new UnityEngine.Events.UnityEvent<UnityEngine.Events.UnityAction>();
        objMon.DeadEvent = new UnityEngine.Events.UnityEvent();

        UnitMovement objMove = obj.AddComponent<UnitMovement>();
        objMon.onMovementEvent.AddListener(objMove.MoveToPos);
        objMon.followEvent.AddListener(objMove.FollowTarget);
        objMon.rotateEvent.AddListener(objMove.Rotate);
        objMon.sideMoveEvent.AddListener(objMove.SideMove);
        objMon.stopEvent.AddListener(objMove.StopMove);

        Status objStatus = obj.AddComponent<Status>();
        objMon.DeadEvent.AddListener(objStatus.RemoveAll);

        GameObject prefab = GameObject.Instantiate(FindPrefab(data.Character_Prefab));
        //GameObject prefab = GameObject.Instantiate(Resources.Load<GameObject>("Monster/MonsterPrefabs/Prefab_Stage1_Slime"));
        prefab.transform.SetParent(obj.transform, false);
        MonsterPartManager part = prefab.GetComponent<MonsterPartManager>();
        objMon.attackStartPos = new Transform[part.attackStartPos.Length];
        for (int i = 0; i < part.attackStartPos.Length; i++)
        {
            objMon.attackStartPos[i] = part.attackStartPos[i];
        }
        for(int i = 0; i< part.parts.Length; i++)
        {
            part.parts[i].col.gameObject.layer = LayerMask.NameToLayer("Monster_Body");
            part.parts[i].type = (DefenceType)data.Character_DetailArmorTypeArr[i];
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


        objMon.skills = new Skill[data.Skill_IndexArr.Length];

        for (int i = 0; i < data.Skill_IndexArr.Length; ++i)
        {
            objMon.skills[i] = CreateMonsterSkill(objMon, data.Skill_IndexArr[i]);
        }

        objMon.idleAI = new List<int>();
        int temp = data.Character_IdleType;
        if (temp > Mathf.Pow(2, 11))
        {
            Debug.Log("Wrong IdleType");
            objMon.idleAI.Add(0);
        }
        else
        {
            for (int i = 10; i >= 0; --i)
            {
                if (temp / (int)Mathf.Pow(2, i) > 0)
                {
                    objMon.idleAI.Add((int)i);
                    temp -= (int)Mathf.Pow(2, i);
                }
            }
        }


        return obj;
    }

    public Skill CreateMonsterSkill(Monster parent ,int index)
    {
        
        SkillDataTable data = default;

        //load from file
        if (monsterDataManager.dicSkillDataTable.ContainsKey(index))
        {
            data = monsterDataManager.dicSkillDataTable[index];
        }
        else
        {
            //nullCheck
        }
        /*
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
        */

        GameObject obj = new GameObject();

        //making skill by recipe
        obj.name = data.Skill_Name.ToString();
        Skill objSkill = obj.AddComponent<Skill>();
        objSkill.preDelay = data.Skill_PreDelay;
        objSkill.postDelay = data.Skill_PostDelay;
        objSkill.detectRadius = data.Skill_DetectRange;
        objSkill.targetMask = 1 << data.Skill_TargetMask;
        objSkill.animType = data.Skill_AnimType;

        objSkill.onSkillActivatedEvent = new UnityEngine.Events.UnityEvent<Transform>();
        objSkill.onSkillHitCheckStartEvent = new UnityEngine.Events.UnityEvent();
        objSkill.onSkillHitCheckEndEvent = new UnityEngine.Events.UnityEvent();

        for(int i = 0; i < data.Skill_OptionArr.Length; ++i)
        {
            AddSkillType(parent, objSkill, data.Skill_OptionArr[i]);
        }

        objSkill.onAddSkillEvent = new UnityEngine.Events.UnityEvent<UnityEngine.Events.UnityAction<Transform, UnityEngine.Events.UnityAction, UnityEngine.Events.UnityAction, UnityEngine.Events.UnityAction>, UnityEngine.Events.UnityAction, UnityEngine.Events.UnityAction, UnityEngine.Events.UnityAction>();
        objSkill.onAddSkillEvent2 = new UnityEngine.Events.UnityEvent<UnityEngine.Events.UnityAction, int, LayerMask>();

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
                    if (monsterDataManager.dicSkillMovementTypeDataTable.ContainsKey(index))
                    {
                        data = monsterDataManager.dicSkillMovementTypeDataTable[index];
                    }
                    else
                    {
                        //nullCheck
                    }
                    /*
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
                    */

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
                    if (monsterDataManager.dicSkillMeleeTypeDataTable.ContainsKey(index))
                    {
                        data = monsterDataManager.dicSkillMeleeTypeDataTable[index];
                    }
                    else
                    {
                        //nullCheck
                    }
                    /*
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
                    */
                    obj.name = "Melee";
                    MeleeSkillType melee = obj.AddComponent<MeleeSkillType>();
                    melee.maxIndex = data.Skill_NumOfHitBox;
                    melee.areaOfEffectPrefeb = FindPrefab(data.Skill_ObjectEffect);
                    melee.hitEffectPrefeb = FindPrefab(data.Skill_HitEffect);
                    melee.targetMask = 1 << data.Skill_TargetMask;
                    melee.attackStartPos = new Transform[data.Skill_NumOfHitBox];
                    for(int i = 0; i < data.Skill_NumOfHitBox; i++)
                    {
                        melee.attackStartPos[i] = monster.attackStartPos[0];
                    }
                    melee.hitDuration = data.Skill_hitDuration;

                    melee.onSkillHitEvent = new UnityEngine.Events.UnityEvent<Collider>();
                    for(int i = 0; i < data.Skill_AffectOptionArr.Length; ++i)
                    {
                        AddSkillAffect(melee, data.Skill_AffectOptionArr[i]);
                    }

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
                    if (monsterDataManager.dicSkillProjectileDetailDataTable.ContainsKey(index))
                    {
                        data = monsterDataManager.dicSkillProjectileDetailDataTable[index];
                    }
                    else
                    {
                        //nullCheck
                    }
                    /*
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
                    */
                    obj.name = "Projectile";
                    ProjectileSkillType projectile = obj.AddComponent<ProjectileSkillType>();
                    projectile.maxIndex = data.Skill_NumOfHitBox;
                    projectile.areaOfEffectPrefeb = FindPrefab(data.Skill_ObjectEffect);
                    projectile.hitEffectPrefeb = FindPrefab(data.Skill_HitEffect);
                    projectile.destroyEffectPrefeb = FindPrefab(data.Skill_DestroyEffect);
                    projectile.unPenetrableMask = 1 << data.Skill_Unpenetrable;
                    projectile.targetMask = 1 << data.Skill_TargetMask;
                    projectile.moveSpeed = data.Skill_LongRangeAttackSpeed;
                    projectile.maxDist = data.Skill_LongRangeAttackDist;
                    projectile.parabolaHeight = data.Skill_ParabolaHeight;
                    projectile.attackStartPos = new Transform[data.Skill_NumOfHitBox];
                    projectile.penetrable = data.Skill_Penetrable;
                    projectile.isParabola = data.Skill_IsParabola;

                    for(int i = 0; i < data.Skill_NumOfHitBox; ++i)
                    {
                        projectile.attackStartPos[i] = monster.attackStartPos[data.Skill_HitBoxStartPosArr[i]];
                    }

                    projectile.hitDuration = data.Skill_hitDuration;

                    projectile.onSkillHitEvent = new UnityEngine.Events.UnityEvent<Collider>();
                    for (int i = 0; i < data.Skill_AffectOptionArr.Length; ++i)
                    {
                        AddSkillAffect(projectile, data.Skill_AffectOptionArr[i]);
                    }
                    parent.onSkillActivatedEvent.AddListener(projectile.OnSkillActivated);
                    parent.onSkillHitCheckStartEvent.AddListener(projectile.OnSkillHitCheckStartEventHandler);
                    parent.onSkillHitCheckEndEvent.AddListener(projectile.OnSkillHitCheckEndEventHandler);
                }
                break;
            case 4:
                {
                    obj.name = "SideMove";
                    SideMovementSkillType sideMove = obj.AddComponent<SideMovementSkillType>();
                    sideMove.sideMoveEvent = new UnityEngine.Events.UnityEvent<Transform, Info<float, float>, UnityEngine.Events.UnityAction, UnityEngine.Events.UnityAction>();
                    sideMove.stopEvent = new UnityEngine.Events.UnityEvent<UnityEngine.Events.UnityAction>();

                    sideMove.sideMoveEvent.AddListener(monster.GetComponent<UnitMovement>().SideMove);
                    sideMove.stopEvent.AddListener(monster.GetComponent<UnitMovement>().StopMove);
                    parent.onSkillActivatedEvent.AddListener(sideMove.OnSkillActivated);
                    parent.onSkillHitCheckEndEvent.AddListener(sideMove.OnSkillHitCheckEndEventHandler);
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
                {
                    SkillDamageAffectDataTable data = default;
                    if (monsterDataManager.dicSkillDamageAffectDataTable.ContainsKey(index))
                    {
                        data = monsterDataManager.dicSkillDamageAffectDataTable[index];
                    }
                    else
                    {
                        //nullCheck
                    }

                    /*
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
                    */
                    obj.name = "Damage";
                    DamageSkillEffect damage = obj.AddComponent<DamageSkillEffect>();
                    damage.power = data.Skill_Power;
                    damage.Atype = (AttackType)data.Skill_AttackType;
                    parent.GetComponent<HitCheckSkillType>().onSkillHitEvent.AddListener(damage.OnSkillHit);
                }
                break;
            case 2:
                {
                    SkillKnockBackAffectDataTable data = default;
                    if (monsterDataManager.dicSkillKnockBackAffectDataTable.ContainsKey(index))
                    {
                        data = monsterDataManager.dicSkillKnockBackAffectDataTable[index];
                    }
                    else
                    {
                        //nullCheck
                    }
                    obj.name = "KnockBack";
                    KnockBackSkillEffect knockBack = obj.AddComponent<KnockBackSkillEffect>();
                    knockBack.knockBackPower = data.Skill_KnockBackPower;
                    knockBack.knockUpPower = data.Skill_KnockUpPower;
                    parent.GetComponent<HitCheckSkillType>().onSkillHitEvent.AddListener(knockBack.OnSkillHit);
                }
                break;
            case 3:
                {
                    InflictStatusSkillEffect inflict = obj.AddComponent<InflictStatusSkillEffect>();
                    inflict.abType = (E_StatusAbnormality)(index % 10000);
                    obj.name = inflict.abType.ToString();
                    parent.GetComponent<HitCheckSkillType>().onSkillHitEvent.AddListener(inflict.OnSkillHit);
                }
                break;
            default:
                GameObject.Destroy(obj);
                return;
        }


        obj.transform.SetParent(parent.transform, false);
    }

    public GameObject FindPrefab(int index)
    {
        PrefabTable data = default;
        if (monsterDataManager.dicPrefabTable.ContainsKey(index))
        {
            data = monsterDataManager.dicPrefabTable[index];
        }
        else
        {
            //nullCheck
        }

        switch (index / 10000)
        {
            //Monster/MonsterPrefabs/
            case 1:
                return Resources.Load<GameObject>($"Monster/MonsterPrefabs/{data.Prefab_Name}");
            //Monster/SkillEffect/
            case 2:
                return Resources.Load<GameObject>($"Monster/SkillEffect/{data.Prefab_Name}");
            default:
                return null;
        }
    }

    public Monster FindAI(GameObject obj, int index)
    {
        switch (index)
        {
            case 0:
                return obj.AddComponent<Slime>();
            case 1:
                return null;
            case 2:
                return null;
            default:
                return null;
        }
    }
}
