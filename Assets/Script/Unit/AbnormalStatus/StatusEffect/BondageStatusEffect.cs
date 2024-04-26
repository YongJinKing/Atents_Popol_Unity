using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BondageStatusEffect : StatValueChangeStatusEffect
{
    Player pl;
    public BondageStatusEffect() : base("Bondage") { }

    protected override void Start()
    {
        base.Start();
        pl = GetComponentInParent<Player>();
        if (pl != null)
            pl.DeBuffScr.transform.GetChild(1).gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        if (pl != null)
            pl.DeBuffScr.transform.GetChild(1).gameObject.SetActive(false);
    }

    protected override void Initailize()
    {
        type = E_BattleStat.Speed;
        value = 0.1f;
    }
}
