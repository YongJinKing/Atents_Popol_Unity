using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TempPlayerAnimEvent : MonoBehaviour
{
    public LayerMask enemyMask;
    public Transform myAttackPoint;
    public UnityEvent attackAct;
    public UnityEvent deadAct;
    public UnityEvent EffectAct;
    public UnityEvent SkillAct;
    public UnityEvent<int> End;
    
    public void OnEnd(int i)
    {
        End?.Invoke(i);
    }

    public void OnAttack()
    {
        Collider[] list = Physics.OverlapBox(myAttackPoint.position, new Vector3(5.0f, 1.0f, 8.3f) / 2, Quaternion.identity, enemyMask);
    
        foreach(Collider col in list)
        {
            BattleSystem bs = col.GetComponent<BattleSystem>();
            if(bs != null)
            {
                bs.TakeDamage(30);
            }
        }
    }

    public void OnAttckEffect()
    {
        EffectAct?.Invoke();
    }

    public void OnSkillEffect()
    {
        SkillAct?.Invoke();
    }
}
