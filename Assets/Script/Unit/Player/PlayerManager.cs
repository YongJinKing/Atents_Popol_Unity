using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    int totalDmg;
    public void totalDamege(Collider other, int Pldmg, int skillDamage, AttackType A_type, DefenceType Dtype, float SkillCalculation)
    {
        
        IDamage iDamage = other.GetComponentInParent<IDamage>();
        totalDmg = (int)(Pldmg*SkillCalculation) * skillDamage;
        iDamage.TakeDamage(totalDmg, A_type, Dtype);
    }
}