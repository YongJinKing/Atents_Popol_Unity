using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect : MonoBehaviour
{
    protected string EffectPrefabName = null;
    public StatusEffect()
    {
        EffectPrefabName = null;
    }
    public StatusEffect(string effectPrefabName)
    {
        EffectPrefabName = effectPrefabName;
    }

    protected GameObject LoadEffect()
    {
        if(EffectPrefabName != null)
            return Instantiate(Resources.Load<GameObject>($"Player/DeBuff/{EffectPrefabName}"));
        return null;
    }
}
