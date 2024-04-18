using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Status : MonoBehaviour
{
    public Dictionary<E_StatusAbnormality, StatusCondition> abnormals = new Dictionary<E_StatusAbnormality, StatusCondition>();

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
                }
                break;
            case E_StatusAbnormality.Poison:
                {
                    //obj.AddComponent<CorrosionStatusEffect>();
                    obj.name = "Poison";
                    condition.myStatusAbType = E_StatusAbnormality.Poison;
                    abnormals.Add(E_StatusAbnormality.Poison, condition);
                }
                break;
            case E_StatusAbnormality.Slow:
                {
                    //obj.AddComponent<CorrosionStatusEffect>();
                    obj.name = "Slow";
                    condition.myStatusAbType = E_StatusAbnormality.Slow;
                    abnormals.Add(E_StatusAbnormality.Slow, condition);
                }
                break;
            case E_StatusAbnormality.Stun:
                {
                    //obj.AddComponent<CorrosionStatusEffect>();
                    obj.name = "Stun";
                    condition.myStatusAbType = E_StatusAbnormality.Stun;
                    abnormals.Add(E_StatusAbnormality.Stun, condition);
                }
                break;
            case E_StatusAbnormality.Bondage:
                {
                    //obj.AddComponent<CorrosionStatusEffect>();
                    obj.name = "Bondage";
                    condition.myStatusAbType = E_StatusAbnormality.Bondage;
                    abnormals.Add(E_StatusAbnormality.Bondage, condition);
                }
                break;
            case E_StatusAbnormality.Blind:
                {
                    //obj.AddComponent<CorrosionStatusEffect>();
                    obj.name = "Blind";
                    condition.myStatusAbType = E_StatusAbnormality.Blind;
                    abnormals.Add(E_StatusAbnormality.Blind, condition);
                }
                break;
        }

        obj.transform.SetParent(this.transform);
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
}
