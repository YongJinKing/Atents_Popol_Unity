using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlindStatusEffect : StatusEffect
{
    Player pl;

    public BlindStatusEffect() : base("Blind") { }

    private void Start()
    {
        LoadEffect();
        pl = GetComponentInParent<Player>();
        if(pl != null)
            pl.DeBuffScr.transform.GetChild(0).gameObject.SetActive(true); 
    }

    private void OnDestroy()
    {
        if(pl != null)
            pl.DeBuffScr.transform.GetChild(0).gameObject.SetActive(false);
    }

    protected override void Initailize()
    {
    }

}
