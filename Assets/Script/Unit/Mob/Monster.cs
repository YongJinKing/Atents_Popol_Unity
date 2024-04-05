using UnityEngine;
using UnityEngine.Events;



public abstract class Monster : BattleSystem, ICinematicStart, ICinematicEnd
{
    public enum State
    {
        Create,
        Idle,
        Closing,
        Attacking,
        Death
    }

    //
    #region Properties / Field
    //private
    #region Private
    #endregion

    //protected
    #region protected
    [SerializeField]protected State myState;
    protected LayerMask skillMask;
    protected int animType;
    #endregion

    //Public
    #region public
    public GameObject target;
    public Transform[] attackStartPos;
    public Skill[] skills;
    #endregion
    #region Event
    public UnityEvent<Vector3, float, UnityAction, UnityAction> onMovementEvent;
    public UnityEvent<Transform, float, UnityAction, UnityAction> followEvent;
    public UnityEvent<Transform, float, UnityAction, UnityAction> sideMoveEvent;
    public UnityEvent<Vector3, float> rotateEvent;
    public UnityEvent<UnityAction> stopEvent;

    protected UnityAction<Transform, UnityAction, UnityAction, UnityAction> onSkillStartAct;
    protected UnityAction onSkillHitCheckStartAct;
    protected UnityAction onSkillHitCheckEndAct;
    protected UnityAction onSkillAnimEnd;
    protected UnityAction onDeadAct;
    #endregion
    #endregion


    #region Method
    #region PrivateMethod
    #endregion

    #region ProtectedMethod
    protected abstract void ChangeState(State s);
    protected abstract void ProcessState();
    protected override void OnDead()
    {
        deathAlarm?.Invoke(1);
        ChangeState(State.Death);
        transform.GetComponentInChildren<MonsterPartManager>().DisActiveCol();
    }
    #endregion

    #region PublicMethod
    public abstract void CinematicStart();
    //trigger on
    public abstract void CinematicEnd();
    //trigger off
    //initial()

    #endregion
    #endregion

    #region Coroutine
    #endregion


    #region EventHandler
    public void OnAddSkillEventListener(
        UnityAction<Transform, UnityAction, UnityAction, UnityAction> skillStart,
        UnityAction skillHitCheckStart,
        UnityAction skillHitCheckEnd,
        UnityAction skillAnimEnd)
    {
        onSkillStartAct = skillStart;
        onSkillHitCheckStartAct = skillHitCheckStart;
        onSkillHitCheckEndAct = skillHitCheckEnd;
        onSkillAnimEnd = skillAnimEnd;
    }

    public void OnAddSkillEvent2Listener(UnityAction skillForceEnd ,int animType ,LayerMask mask)
    {
        onDeadAct = skillForceEnd;
        this.animType = animType;
        skillMask = mask;
    }


    public virtual void OnAttackStartAnim()
    {
        onSkillHitCheckStartAct?.Invoke();
    }

    public virtual void OnAttackEndAnim()
    {
        onSkillHitCheckEndAct?.Invoke();
    }

    //Attack Animation End
    public virtual void OnSkillAnimEnd()
    {
        onSkillAnimEnd?.Invoke();
    }
    #endregion


    //����Ƽ �Լ��� ����
    #region MonoBehaviour
    #endregion
}
