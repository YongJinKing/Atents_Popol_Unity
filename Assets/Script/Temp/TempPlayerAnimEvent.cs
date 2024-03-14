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
    public UnityEvent<int> End;
    GameObject Effectobj;
    I_Effect i_Effect;
    void Awake()
    {
        Effectobj = transform.parent.GetComponent<Player>().Effectobj;
        i_Effect = Effectobj.GetComponent<I_Effect>();
    }
    public void OnEnd(int i)
    {
        End?.Invoke(i);
    }

    public void OnAttack()
    {
        Collider[] list = Physics.OverlapBox(myAttackPoint.position, new Vector3(5.0f, 1.0f, 8.3f) / 2, Quaternion.identity, enemyMask);
    
        foreach(Collider col in list)
        {
            IDamage iDamege = col.GetComponent<IDamage>();
            if(iDamege != null)
            {
                iDamege.TakeDamage(30);
            }
        }
    }

    public void OnAttckEffect(string s)
    {
        i_Effect.PlayAttackEffect(s, 0.5f);
    }
}
