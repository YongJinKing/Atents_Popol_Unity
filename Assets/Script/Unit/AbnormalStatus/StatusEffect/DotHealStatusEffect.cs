using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotHealStatusEffect : DotDamageStatusEffect
{
    public DotHealStatusEffect() : base("Poison") { }
    protected override void Initailize()
    {
        dotRate = 0.5f;
        isHeal = true;
    }
}
