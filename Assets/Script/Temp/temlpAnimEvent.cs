using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class temlpAnimEvent : MonoBehaviour
{
    public UnityEvent onAttackEvent;

    public void OnAttack()
    {
        onAttackEvent?.Invoke();
    }
}
