using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BondageStatusEffect : StatValueChangeStatusEffect
{
    public BondageStatusEffect() : base("Bondage") { }

    protected override void Initailize()
    {
        type = E_BattleStat.Speed;
        value = 0.1f;
    }
}
