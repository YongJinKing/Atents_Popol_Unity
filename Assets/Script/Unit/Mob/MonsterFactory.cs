using UnityEngine;
using Newtonsoft.Json;

public class MonsterFactory : MonoBehaviour
{
    public static void CreateMonsterSkill(GameObject parent ,int index)
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

        AddSkillType(objSkill, data.Skill_Option1);
        AddSkillType(objSkill, data.Skill_Option2);
        AddSkillType(objSkill, data.Skill_Option3);
        AddSkillType(objSkill, data.Skill_Option4);


        objSkill.transform.SetParent(parent.transform);
    }

    public static void AddSkillType(Skill parent, int index)
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



                    obj.transform.SetParent(parent.transform);
                }
                break;
            //dont have skill type
            default:
                return;
        }

        obj.transform.SetParent(parent.transform);
    }
}
