using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TempPlayerAnimEvent : MonoBehaviour
{
    public LayerMask enemyMask;
    public Transform myAttackPoint;
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


    public void OnAttckEffect(string s)
    {
        i_Effect.PlayAttackEffect(s);
    }
}
