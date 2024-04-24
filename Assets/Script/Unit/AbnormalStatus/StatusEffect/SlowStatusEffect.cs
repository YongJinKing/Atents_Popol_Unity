using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowStatusEffect : StatValueChangeStatusEffect
{
    public SlowStatusEffect() : base("Slow") { }
    protected override void Initailize()
    {
        type = E_BattleStat.Speed;
        value = 0.6f;
    }
}
