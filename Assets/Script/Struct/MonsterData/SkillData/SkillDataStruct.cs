public struct SkillDataStruct
{
    public int Index;
    public string Skill_Name;
    public float Skill_PreDelay;
    public float Skill_PostDelay;
    public float Skill_DetectRange;
    public int Skill_TargetMask;
    public int Skill_Option1;
    public int Skill_Option2;
    public int Skill_Option3;
    public int Skill_Option4;
    public int Skill_DescComponent;
}

public struct SkillMovementTypeDataStruct
{
    public int Index;
    public float Skill_ShortRangeAttackSpeed;
    public float Skill_ShortRangeAttackDist;
}

public struct SkillMeleeTypeDataStruct
{
    public int Index;
    public int Skill_NumOfHitBox;
    public int Skill_HitBox;
    public float Skill_hitDuration;
    public int Skill_AffectOption1;
    public int Skill_AffectOption2;
    public int Skill_AffectOption3;
    public int Skill_AffectOption4;
    public int Skill_AffectOption5;
}

public struct SkillProjectileTypeDataStruct
{
    public int Index;
    public int Skill_NumOfHitBox;
    public int Skill_HitBox;
    public int Skill_Unpenetrable;
    public float Skill_hitDuration;
    public float Skill_LongRangeAttackSpeed;
    public float Skill_LongRangeAttackDist;
    public bool Skill_Penetrable;
    public int Skill_AffectOption1;
    public int Skill_AffectOption2;
    public int Skill_AffectOption3;
    public int Skill_AffectOption4;
    public int Skill_AffectOption5;
}

public struct SkillDamageAffectDataStruct
{
    public int Index;
    public float Skill_Power;
    public int Skill_AttackType;
}
