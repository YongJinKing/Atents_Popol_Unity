using UnityEngine;
using UnityEngine.Events;

//스킬의 히트박스와 피격박스를 위한 클래스
public abstract class BaseSkillType : MonoBehaviour
{
    //변수 영역
    #region Properties / Field
    //private 변수 영역
    #region Private
    #endregion

    //protected 변수 영역
    #region protected
    //OnSkillActivated로 받아온 타겟의 위치를 저장함
    protected Vector3 targetPos;
    #endregion

    //Public 변수영역
    #region public
    #endregion

    //이벤트 함수들 영역
    #region Event
    //스킬
    public UnityEvent<Collider> onSkillHitEvent;
    public UnityEvent onSkillDisactivatedEvent;
    #endregion
    #endregion


    #region Method
    //private 함수들 영역
    #region PrivateMethod
    #endregion

    //protected 함수들 영역
    #region ProtectedMethod
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
    //skill 클래스의 이벤트가 발생
    public virtual void OnSkillActivated(Vector3 targetPos)
    {
        this.targetPos = targetPos;
    }

    public virtual void OnSkillDisactivated()
    {
        onSkillDisactivatedEvent?.Invoke();
    }
    #endregion


    //유니티 함수들 영역
    #region MonoBehaviour
    protected virtual void Start()
    {
    }
    #endregion
}

