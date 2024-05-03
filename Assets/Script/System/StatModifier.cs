using System.Collections;
using System.Collections.Generic;
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

        StartCoroutine(DelayLoad());
    }

    private IEnumerator DelayLoad()
    {
        yield return new WaitForEndOfFrame();

        IGetStatValueModifier[] modifiers = GetComponentsInChildren<IGetStatValueModifier>();
        Info<E_BattleStat, ValueModifier> temp;
        foreach (IGetStatValueModifier data in modifiers)
        {
            temp = data.GetStatValueModifier();
            if (temp.arg1 != null)
            {
                List<ValueModifier> tempList;
                if (dicModifiers.ContainsKey(temp.arg0))
                {
                    tempList = dicModifiers[temp.arg0];
                    tempList.Add(temp.arg1);
                }
                else
                {
                    tempList = new List<ValueModifier>();
                    tempList.Add(temp.arg1);
                    dicModifiers.Add(temp.arg0, tempList);
                }
            }
        }
    }

    public List<ValueModifier> GetStatValueModifiers(E_BattleStat statType)
    {
        //Debug.Log($"Modifiers 전달 성공, 이름 : {gameObject.name}");
        if (isActive && dicModifiers.ContainsKey(statType))
            return dicModifiers[statType];
        else
            return null;
    }
}