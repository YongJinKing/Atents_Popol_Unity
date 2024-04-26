using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffect : MonoBehaviour
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

    protected abstract void Initailize();

    protected void LoadEffect()
    {
        if (EffectPrefabName == null)
            return;
        
        GameObject Effect = (Resources.Load<GameObject>($"DeBuff/{EffectPrefabName}")) ?? 
            (Resources.Load<GameObject>($"Buff/{EffectPrefabName}"));
        
        Effect = Instantiate(Effect);
        Effect.transform.SetParent(transform, false);
        Effect.transform.localPosition += Vector3.up * 0.5f;
    }
}
