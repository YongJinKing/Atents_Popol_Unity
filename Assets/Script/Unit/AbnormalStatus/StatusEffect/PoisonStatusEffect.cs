using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class PoisonStatusEffect : DotDamageStatusEffect
{
    public PoisonStatusEffect() : base("Poison") { }

    protected override void Initailize()
    {
        dotRate = 0.5f;
        isHeal = false;
    }
}
