using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BaseHitBox : MonoBehaviour
{
    protected Collider myCol;
    protected BattleSystem myBattleSystem;
    protected LayerMask targetMask;
    public UnityEvent<Collider> onHitEvent;

    protected virtual void Start()
    {
        myCol = GetComponent<Collider>();
    }

    public void Initialize(BattleSystem myBattleSystem, LayerMask mask)
    {
        if(myBattleSystem == null)
        {
            return;
        }
        this.myBattleSystem = myBattleSystem;
        GetComponentsInChildren<BaseSkillEffect>(myBattleSystem);
        targetMask = mask;
    }

    protected abstract void OnCollisionEnter(Collision collision);
}
