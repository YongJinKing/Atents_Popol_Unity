using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BondageStatusEffect : StatusEffect
{
    public float value = 0.0f;

    public BondageStatusEffect() : base("Bondage") { }

    private void Start()
    {
        GameObject Effect = LoadEffect();
        Effect.transform.SetParent(transform, false);

        GameObject child = new GameObject();
        child.name = "StatModifier";
        child.AddComponent<StatModifier>();
        child.transform.SetParent(transform);

        GameObject grandChild = new GameObject();
        grandChild.name = "Mult";
        StatModifyFeature feature = grandChild.AddComponent<StatModifyFeature>();
        feature.statType = E_BattleStat.Speed;
        feature.modifierType = ValueModifierType.Mult;
        feature.value = this.value;
        grandChild.transform.SetParent(child.transform);
    }
}
