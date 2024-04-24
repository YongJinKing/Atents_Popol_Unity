using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrosionStatusEffect : StatValueChangeStatusEffect
{
    public CorrosionStatusEffect() : base("BuSick"){}

    protected override void Initailize()
    {
        type = E_BattleStat.ATK;
        value = 0.8f;
    }
}
