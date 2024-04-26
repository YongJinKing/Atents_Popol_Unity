using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BleedStatusEffect : DotDamageStatusEffect
{
    public BleedStatusEffect() : base("Bleeding") { }
    protected override void Initailize()
    {
        dotRate = 0.5f;
        isHeal = false;
    }
}
