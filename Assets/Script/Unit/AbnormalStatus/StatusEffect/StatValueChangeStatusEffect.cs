using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatValueChangeStatusEffect : StatusEffect
{
    public E_BattleStat type;
    public float value;

    public StatValueChangeStatusEffect(string s) : base(s) { }

    protected void Start()
    {
        LoadEffect();
        Initailize();
        AddStatModifier();
    }

    protected void AddStatModifier()
    {
        GameObject child = new GameObject();
        child.name = "StatModifier";
        child.AddComponent<StatModifier>();
        child.transform.SetParent(transform);

        GameObject grandChild = new GameObject();
        grandChild.name = "Mult";
        StatModifyFeature feature = grandChild.AddComponent<StatModifyFeature>();
        feature.statType = type;
        feature.modifierType = ValueModifierType.Mult;
        feature.value = this.value;
        grandChild.transform.SetParent(child.transform);
    }
}
