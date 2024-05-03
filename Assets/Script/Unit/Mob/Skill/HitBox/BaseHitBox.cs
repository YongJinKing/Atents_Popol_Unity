using UnityEngine;
using UnityEngine.Events;

public abstract class BaseHitBox : MonoBehaviour, IEnrollEvent<Collider, Vector3>
{
    protected Collider myCol;
    protected BattleSystem myBattleSystem;
    protected LayerMask targetMask;
    public UnityEvent<Collider, Vector3> onHitEvent;

    protected virtual void Start()
    {
        myCol = GetComponent<Collider>();
    }

    protected abstract void OnTriggerStay(Collider other);
    protected abstract void OnEnable();
    protected abstract void OnDisable();


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

    public void Enroll(UnityAction<Collider, Vector3> action)
    {
        //Debug.Log("BaseHitBox Enroll");
        onHitEvent.AddListener(action);
    }
}
