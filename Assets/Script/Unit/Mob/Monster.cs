using UnityEngine;
using UnityEngine.Events;

public abstract class Monster : BattleSystem
{
    public enum State
    {
        Idle,
        Closing,
        Attacking
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
    #endregion

    //Public ��������
    #region public
    public GameObject target;
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
    public UnityEvent<UnityAction> stopEvent;
    public UnityAction<Vector3, UnityAction, UnityAction> onSkillStartAct;
    public UnityAction onSkillHitCheckStartAct;
    public UnityAction onSkillHitCheckEndAct;
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
        UnityAction<Vector3, UnityAction, UnityAction> skillStart,
        UnityAction skillHitCheckStart,
        UnityAction skillHitCheckEnd,
        LayerMask mask)
    {
        onSkillStartAct = skillStart;
        onSkillHitCheckStartAct = skillHitCheckStart;
        onSkillHitCheckEndAct = skillHitCheckEnd;
        skillMask = mask;
    }

    public virtual void OnSkillEndEventListener()
    {

    }
    #endregion


    //����Ƽ �Լ��� ����
    #region MonoBehaviour
    #endregion
}
