using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatModifier : MonoBehaviour, IGetStatValueModifiers
{
    protected Dictionary<E_BattleStat, List<ValueModifier>> dicModifiers = new Dictionary<E_BattleStat, List<ValueModifier>>();
    public bool isActive;

    private void OnEnable()
    {
        isActive = true;
    }

    private void OnDisable()
    {
        isActive = false;
    }

    private void Start()
    {
        /*
        List<ValueModifier> modifierList = new List<ValueModifier>();
        AddValueModifier add = new AddValueModifier(1, 20);
        MultValueModifier mult = new MultValueModifier(2, 1.5f);

        modifierList.Add(add);
        modifierList.Add(mult);

        dicModifiers.Add(E_BattleStat.ATK, modifierList);
        */

        IGetStatValueModifier[] modifiers = GetComponentsInChildren<IGetStatValueModifier>();
        Info<E_BattleStat, ValueModifier> temp;
        foreach (IGetStatValueModifier data in modifiers)
        {
            temp = data.GetStatValueModifier();
            if (temp.arg1 != null)
            {
                //dicModifiers.Add(temp.arg0, temp.arg1);
            }
        }
    }

    public List<ValueModifier> GetStatValueModifiers(E_BattleStat statType)
    {
        return dicModifiers[statType];
    }
}
