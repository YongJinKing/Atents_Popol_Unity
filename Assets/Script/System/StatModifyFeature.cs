using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ValueModifierType
{
    Add,Mult
}

public interface IGetStatValueModifier
{
    public Info<E_BattleStat, ValueModifier> GetStatValueModifier();
}

public class StatModifyFeature : MonoBehaviour, IGetStatValueModifier
{
    public E_BattleStat statType;
    public ValueModifierType modifierType = ValueModifierType.Add;
    public ValueModifier VM = null;
    public int sortOrder = 1;
    public float value = 0;

    private void Start()
    {
        switch (modifierType)
        {
            case ValueModifierType.Add:
                {
                    VM = new AddValueModifier(sortOrder, value);
                }
                break;
            case ValueModifierType.Mult:
                {
                    VM = new MultValueModifier(sortOrder, value);
                }
                break;
        }
    }

    public Info<E_BattleStat , ValueModifier> GetStatValueModifier()
    {
        /*
        Debug.Log($"Modifier 전달, 이름 : {gameObject.name}, 값 ${statType}, {value}");
        if(VM != null)
        {
            Debug.Log($"Feature 값 전달 성공");
        }
        */
        return new Info<E_BattleStat,ValueModifier>(statType,VM);
    }
}
