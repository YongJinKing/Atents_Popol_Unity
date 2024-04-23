using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunStatusEffect : StatusEffect
{
    private IStun target;

    StunStatusEffect() : base("Stun") { }
    // Start is called before the first frame update
    private void Start()
    {
        LoadEffect();

        target = GetComponentInParent<IStun>();
        if(target != null)
            target.GetStun();
    }

    private void OnDestroy()
    {
        if(target != null)
            target.OutStun();
    }
}