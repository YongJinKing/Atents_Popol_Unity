using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        foreach(BaseSkillEffect data in GetComponentsInChildren<BaseSkillEffect>())
        {
            data.InitializeBattleSystem(myBattleSystem);
        }
        targetMask = mask;
        //Debug.Log($"BaseHitBox, targetMask : {targetMask.value}");
    }


    protected abstract void OnTriggerStay(Collider other);
}
