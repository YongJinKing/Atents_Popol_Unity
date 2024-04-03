using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonsterAnimEvent : MonoBehaviour
{
    public UnityEvent onAttackStartEvent;
    public UnityEvent onAttackEndEvent;
    public UnityEvent onAttackAnimEndEvent;

    public void OnAttackStart()
    {
        onAttackStartEvent?.Invoke();
    }

    public void OnAttackEnd()
    {
        onAttackEndEvent?.Invoke();
    }

    public void OnAttackAnimEnd()
    {
        onAttackAnimEndEvent?.Invoke();
    }
}
