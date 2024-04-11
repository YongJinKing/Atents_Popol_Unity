using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Status : MonoBehaviour
{
    public void Remove(StatusCondition target)
    {
        StatusEffect effect = target.GetComponentInParent<StatusEffect>();
        target.transform.SetParent(null);
        Destroy(target.gameObject);
        StatusCondition condition = effect.GetComponentInChildren<StatusCondition>();
        if (condition == null)
        {
            effect.transform.SetParent(null);
            Destroy(effect.gameObject);
        }
    }
}
