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

    protected void LoadEffect()
    {
        if (EffectPrefabName == null)
            return;
        
        GameObject Effect = Instantiate(Resources.Load<GameObject>($"Player/DeBuff/{EffectPrefabName}"));
        Effect.transform.SetParent(transform, false);
        Effect.transform.localPosition += Vector3.up * 0.5f;
    }
}
