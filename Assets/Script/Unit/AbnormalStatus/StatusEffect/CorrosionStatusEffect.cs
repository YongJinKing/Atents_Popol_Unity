using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrosionStatusEffect : StatValueChangeStatusEffect
{
    Player pl;

    public CorrosionStatusEffect() : base("BuSick"){}
    protected override void Start()
    {
        base.Start();
        pl = GetComponentInParent<Player>();
        if (pl != null)
            pl.DeBuffScr.transform.GetChild(2).gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        if( pl != null)
            pl.DeBuffScr.transform.GetChild(2).gameObject.SetActive(false);
    }

    protected override void Initailize()
    {
        type = E_BattleStat.ATK;
        value = 0.8f;
    }
}
