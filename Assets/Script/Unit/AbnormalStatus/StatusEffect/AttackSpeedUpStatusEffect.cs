using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpeedUpStatusEffect : StatValueChangeStatusEffect
{
    private CharacterProperty myPro;
    public AttackSpeedUpStatusEffect() : base("AtkUp") { }

    protected override void Initailize()
    {
        myPro = GetComponentInParent<CharacterProperty>();
        myPro.myAnim.SetFloat("AttackSpeed", 2.0f);

        type = E_BattleStat.AttackDelay;
        value = 0.5f;
    }

    private void OnDestroy()
    {
        myPro.myAnim.SetFloat("AttackSpeed", 1.0f);
    }
}
