using UnityEngine;
using UnityEngine.Events;

public abstract class Monster : BattleSystem
{
    public enum State
    {
        Create,
        Idle,
        Closing,
        Attacking,
        Death
    }

    //���� ����
    #region Properties / Field
    //private ���� ����
    #region Private
    #endregion

    //protected ���� ����
    #region protected
    [SerializeField]protected State myState;
    //��ų�� Ÿ�����ϴ� ���̾ �޾ƿ´�.
    protected LayerMask skillMask;
    protected bool isLoopAnim;
    #endregion

    //Public ��������
    #region public
    public GameObject target;
    public Transform[] attackStartPos;
    //�ϴ� �ӽ÷�
    public Skill[] skills;
    #endregion

    //�̺�Ʈ �Լ��� ����
    #region Event
    //Skill�� �����ų �̺�Ʈ �迭
    //detect�� ������ ��� ���� ���� �ϴ� ����
    //public UnityEvent<Vector3>[] onSkillUseEvent;
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
    //private �Լ��� ����
    #region PrivateMethod
    #endregion

    //protected �Լ��� ����
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

    //public �Լ��� ����
    #region PublicMethod
    #endregion
    #endregion


    //�ڷ�ƾ ����
    #region Coroutine
    #endregion


    //�̺�Ʈ�� �Ͼ���� ����Ǵ� On~~�Լ�
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

    public void OnAddSkillEvent2Listener(UnityAction skillForceEnd ,bool isLoopAnim,LayerMask mask)
    {
        onDeadAct = skillForceEnd;
        this.isLoopAnim = isLoopAnim;
        skillMask = mask;
    }


    //���ݸ���� ��ų�� �ߵ�
    public virtual void OnAttackStartAnim()
    {
        onSkillHitCheckStartAct?.Invoke();
    }

    //���� ����߿� ��Ʈ�ڽ� �� ����
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
