using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrosionStatusEffect : StatusEffect
{
    public float value = 0.9f;
    private void Start()
    {
        gameObject.AddComponent<StatModifier>();

        GameObject child = new GameObject();
        child.name = "Mult";
        StatModifyFeature feature = child.AddComponent<StatModifyFeature>();
        feature.statType = E_BattleStat.ATK;
        feature.modifierType = ValueModifierType.Mult;
        feature.value = this.value;
        child.transform.SetParent(this.transform);
    }
}
