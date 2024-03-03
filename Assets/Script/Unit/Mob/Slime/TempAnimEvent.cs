using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TempAnimEvent : MonoBehaviour
{
    public UnityEvent onAttackStartEvent;
    public UnityEvent onAttackEndEvent;

    public void OnAttackStart()
    {
        onAttackStartEvent?.Invoke();
    }

    public void OnAttackEnd()
    {
        onAttackEndEvent?.Invoke();
    }
}
