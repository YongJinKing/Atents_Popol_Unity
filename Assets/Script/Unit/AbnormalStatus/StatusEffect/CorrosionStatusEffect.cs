using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrosionStatusEffect : StatusEffect
{
    public float value = 0.9f;
    private void Start()
    {
        GameObject child = new GameObject();
        child.name = "StatModifier";
        child.AddComponent<StatModifier>();
        child.transform.SetParent(transform);

        GameObject grandChild = new GameObject();
        grandChild.name = "Mult";
        StatModifyFeature feature = grandChild.AddComponent<StatModifyFeature>();
        feature.statType = E_BattleStat.ATK;
        feature.modifierType = ValueModifierType.Mult;
        feature.value = this.value;
        grandChild.transform.SetParent(child.transform);
    }
}
