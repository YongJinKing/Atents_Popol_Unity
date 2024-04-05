
#region UI
public struct UIStringTable
{
    public int index;
    public int String_Type;
    public string String_Desc;
}
public struct PrefabTable
{
    public int index;
    public string Prefab_Name;
}
public struct BossStageTable
{
    public int index;
    public int Stage_BossMonster_Name;
    public int Stage_BossMonster_Desc;
    public int Stage_BossMonster_Type;
    public int Stage_BossMonster;
    public int Stage_ClearGold;
    public int Stage_ClearEXP;
    public int Stage_BossSkill_Image1;
    public int Stage_BossSkill_Image2;
    public int Stage_BossSkill_Image3;
    public int Stage_BossSkill_Image4;
    public int Stage_BossSkill_Image5;
    public int Stage_ClearReward1;
    public int Stage_ClearReward2;
    public int Stage_ClearReward3;
    public int Stage_ClearReward4;
    public int Stage_ClearReward5;
}
public struct SkillImageResourceTable
{
    public int index;
    public string Skill_ImageResource;
}
#endregion

#region Monster
public struct MonsterDataTable
{
    public int Index;
    public int Character_Type;
    public int Character_Name;
    public int Character_Hp;
    public float Character_MoveSpeed;
    public int Character_AttackPower;
    public int Character_SkillVisibleRange;
    public int Skill_Index1;
    public int Skill_Index2;
    public int Skill_Index3;
    public int Skill_Index4;
    public int Character_DetailArmorType1;
    public int Character_DetailArmorType2;
    public int Character_DetailArmorType3;
    public int Character_DetailArmorType4;
    public int Character_Prefab;
    public int Character_AIType;
    public float Character_Scale;
}
#endregion


#region SkillTable
public struct SkillDataTable
{
    public int Index;
    public int Skill_Name;
    public float Skill_PreDelay;
    public float Skill_PostDelay;
    public float Skill_DetectRange;
    public int Skill_TargetMask;
    public int Skill_Option1;
    public int Skill_Option2;
    public int Skill_Option3;
    public int Skill_Option4;
    public int Skill_DescComponent;
    public int Skill_AnimType;
}
#endregion
#region Type
public struct SkillMovementTypeDataTable
{
    public int Index;
    public float Skill_ShortRangeAttackSpeed;
    public float Skill_ShortRangeAttackDist;
}

public struct SkillMeleeTypeDataTable
{
    public int Index;
    public int Skill_NumOfHitBox;
    public int Skill_ObjectEffect;
    public int Skill_TargetMask;
    public float Skill_hitDuration;
    public int Skill_AffectOption1;
    public int Skill_AffectOption2;
    public int Skill_AffectOption3;
    public int Skill_AffectOption4;
    public int Skill_AffectOption5;
    public int Skill_HitEffect;
}

public struct SkillProjectileDetailDataTable
{
    public int Index;
    public int Skill_NumOfHitBox;
    public int Skill_ObjectEffect;
    public int Skill_Unpenetrable;
    public int Skill_TargetMask;
    public float Skill_hitDuration;
    public float Skill_LongRangeAttackSpeed;
    public float Skill_LongRangeAttackDist;
    public float Skill_ParabolaHeight;
    public bool Skill_Penetrable;
    public bool Skill_IsParabola;
    public int Skill_AffectOption1;
    public int Skill_AffectOption2;
    public int Skill_AffectOption3;
    public int Skill_AffectOption4;
    public int Skill_AffectOption5;
    public int Skill_HitBoxStartPos1;
    public int Skill_HitBoxStartPos2;
    public int Skill_HitBoxStartPos3;
    public int Skill_HitBoxStartPos4;
    public int Skill_HitEffect;
    public int Skill_DestroyEffect;
}

#endregion
#region Affect
public struct SkillDamageAffectDataTable
{
    public int Index;
    public float Skill_Power;
    public int Skill_AttackType;
}

public struct SkillKnockBackAffectDataTable
{
    public int Index;
    public float Skill_KnockBackPower;
    public float Skill_KnockUpPower;
}
#endregion