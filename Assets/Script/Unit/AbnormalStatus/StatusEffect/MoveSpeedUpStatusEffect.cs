using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpeedUpStatusEffect : StatValueChangeStatusEffect
{
    public MoveSpeedUpStatusEffect() : base("Slow") { }
    protected override void Initailize()
    {
        type = E_BattleStat.Speed;
        value = 1.4f;
    }
}
