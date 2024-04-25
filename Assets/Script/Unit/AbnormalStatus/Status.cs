using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

public class Status : MonoBehaviour
{
    public UnityEvent<int> BuffAct;
    public Dictionary<E_StatusAbnormality, StatusCondition> abnormals = new Dictionary<E_StatusAbnormality, StatusCondition>();
    public Dictionary<E_Buff, StatusCondition> buffs = new Dictionary<E_Buff, StatusCondition>();

    public void Add(E_Buff buffType)
    {
        if(buffs.ContainsKey(buffType))
        {
            buffs[buffType].Overlap();
            return;
        }

        GameObject obj = new GameObject();
        StatusCondition condition = obj.AddComponent<DurationStatusCondition>();
        switch (buffType)
        {
            case E_Buff.AtkUp:
                {
                    obj.AddComponent<AtkUpStatusEffect>();
                    obj.name = "AtkUp";
                    condition.myBuffType = E_Buff.AtkUp;
                    buffs.Add(E_Buff.AtkUp, condition);
                    BuffType(1000);
                }
                break;
            case E_Buff.DotHeal:
                {
                    obj.AddComponent<DotHealStatusEffect>();
                    obj.name = "DotHeal";
                    condition.myBuffType = E_Buff.DotHeal;
                    buffs.Add(E_Buff.DotHeal, condition);
                    BuffType(1001);
                }
                break;
            case E_Buff.MoveSpeedUp:
                {
                    obj.AddComponent<MoveSpeedUpStatusEffect>();
                    obj.name = "MoveSpeedUp";
                    condition.myBuffType = E_Buff.MoveSpeedUp;
                    buffs.Add(E_Buff.MoveSpeedUp, condition);
                    BuffType(1002);
                }
                break;
            case E_Buff.AttackSpeedUp:
                {
                    obj.AddComponent<AttackSpeedUpStatusEffect>();
                    obj.name = "AttackSpeedUp";
                    condition.myBuffType = E_Buff.AttackSpeedUp;
                    buffs.Add(E_Buff.AttackSpeedUp, condition);
                    BuffType(1003);
                }
                break;
        }

        obj.transform.SetParent(this.transform, false);
    }

    public void Add(E_StatusAbnormality eType)
    {
        if (abnormals.ContainsKey(eType))
        {
            abnormals[eType].Overlap();
            return;
        }

        GameObject obj = new GameObject();
        StatusCondition condition = obj.AddComponent<DurationStatusCondition>();
        switch (eType)
        {
            case E_StatusAbnormality.Corrosion:
                {
                    obj.AddComponent<CorrosionStatusEffect>();
                    obj.name = "Corrosion";
                    condition.myStatusAbType = E_StatusAbnormality.Corrosion;
                    abnormals.Add(E_StatusAbnormality.Corrosion, condition);
                    BuffType(2000);
                }
                break;
            case E_StatusAbnormality.Poison:
                {
                    obj.AddComponent<PoisonStatusEffect>();
                    obj.name = "Poison";
                    condition.myStatusAbType = E_StatusAbnormality.Poison;
                    abnormals.Add(E_StatusAbnormality.Poison, condition);
                    BuffType(2001);
                }
                break;
            case E_StatusAbnormality.Slow:
                {
                    obj.AddComponent<SlowStatusEffect>();
                    obj.name = "Slow";
                    condition.myStatusAbType = E_StatusAbnormality.Slow;
                    abnormals.Add(E_StatusAbnormality.Slow, condition);
                    BuffType(2002);
                }
                break;
            case E_StatusAbnormality.Stun:
                {
                    obj.AddComponent<StunStatusEffect>();
                    obj.name = "Stun";
                    condition.myStatusAbType = E_StatusAbnormality.Stun;
                    abnormals.Add(E_StatusAbnormality.Stun, condition);
                    BuffType(2003);
                }
                break;
            case E_StatusAbnormality.Bondage:
                {
                    obj.AddComponent<BondageStatusEffect>();
                    obj.name = "Bondage";
                    condition.myStatusAbType = E_StatusAbnormality.Bondage;
                    abnormals.Add(E_StatusAbnormality.Bondage, condition);
                    BuffType(2004);
                }
                break;
            case E_StatusAbnormality.Blind:
                {
                    //obj.AddComponent<CorrosionStatusEffect>();
                    obj.name = "Blind";
                    condition.myStatusAbType = E_StatusAbnormality.Blind;
                    abnormals.Add(E_StatusAbnormality.Blind, condition);
                    BuffType(2005);
                }
                break;
        }

        obj.transform.SetParent(this.transform, false);
    }

    public void Remove(E_Buff btype)
    {
        if (buffs.ContainsKey(btype))
        {
            if (buffs[btype] != null)
            {
                GameObject obj = buffs[btype].gameObject;
                buffs.Remove(btype);
                Destroy(obj);
            }
        }
    }

    public void Remove(E_StatusAbnormality eType)
    {
        if (abnormals.ContainsKey(eType))
        {
            if (abnormals[eType] != null)
            {
                GameObject obj = abnormals[eType].gameObject;
                abnormals.Remove(eType);
                Destroy(obj);
            }
        }
    }

    public void RemoveAll()
    {
        foreach(StatusCondition status in abnormals.Values)
        {
            if(status != null)
            {
                Destroy(status.gameObject);
            }
        }
        abnormals.Clear();
    }

    void BuffType(int Type)
    {
        BuffAct?.Invoke(Type);
    }
}
