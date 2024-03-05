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
    protected State myState;
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
    #endregion


    //����Ƽ �Լ��� ����
    #region MonoBehaviour
    #endregion
}
