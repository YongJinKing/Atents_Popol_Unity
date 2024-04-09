using System.Collections.Generic;
using System.Diagnostics;

public interface IGetStatValueModifiers
{
    public List<ValueModifier> GetStatValueModifiers(E_BattleStat statType);
}

public class ValueChanger
{
    #region Fields / Properties
    public readonly float fromValue;
    public float delta { get { return GetModifiedValue() - fromValue; } }
    List<ValueModifier> modifiers;
    #endregion
    #region Constructor
    public ValueChanger(float fromValue)
    {
        this.fromValue = fromValue;
    }
    #endregion
    #region Public
    public void AddModifier(ValueModifier m)
    {
        if (modifiers == null)
            modifiers = new List<ValueModifier>();
        modifiers.Add(m);
    }

    public void AddModifiers(List<ValueModifier> modifiers)
    {
        if (this.modifiers == null)
            this.modifiers = new List<ValueModifier>();
        this.modifiers.AddRange(modifiers);
    }

    public float GetModifiedValue()
    {
        float value = fromValue;
        if (modifiers == null)
        {
            return value;
        }
            

        modifiers.Sort(Compare);
        for (int i = 0; i < modifiers.Count; ++i)
            value = modifiers[i].Modify(value);

        //UnityEngine.Debug.Log($"sccessed in VC value : {modifiers[1].sortOrder}");

        return value;
    }
    #endregion
    #region Private
    int Compare(ValueModifier x, ValueModifier y)
    {
        return x.sortOrder.CompareTo(y.sortOrder);
    }
    #endregion
}