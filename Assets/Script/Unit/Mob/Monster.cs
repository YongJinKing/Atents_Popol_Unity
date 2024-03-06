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

    //변수 영역
    #region Properties / Field
    //private 변수 영역
    #region Private
    #endregion

    //protected 변수 영역
    #region protected
    [SerializeField]protected State myState;
    #endregion

    //Public 변수영역
    #region public
    public GameObject target;
    //일단 임시로
    public Skill[] skills;
    #endregion

    //이벤트 함수들 영역
    #region Event
    //Skill을 실행시킬 이벤트 배열
    //detect랑 연결을 어떻게 할지 몰라서 일단 놔둠
    //public UnityEvent<Vector3>[] onSkillUseEvent;
    public UnityEvent<Vector3, float, UnityAction, UnityAction> onMovementEvent;
    #endregion
    #endregion


    #region Method
    //private 함수들 영역
    #region PrivateMethod
    #endregion

    //protected 함수들 영역
    #region ProtectedMethod
    protected abstract void ChangeState(State s);
    protected abstract void ProcessState();
    #endregion

    //public 함수들 영역
    #region PublicMethod
    #endregion
    #endregion


    //코루틴 영역
    #region Coroutine
    #endregion


    //이벤트가 일어났을때 실행되는 On~~함수
    #region EventHandler
    #endregion


    //유니티 함수들 영역
    #region MonoBehaviour
    #endregion
}
