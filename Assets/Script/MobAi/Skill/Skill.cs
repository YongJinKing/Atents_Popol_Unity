using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//스킬이 실행됬을때 처음으로 실행되는 클래스
public class Skill : MonoBehaviour
{
    //변수 영역
    #region Properties / Field
    //private 변수 영역
    #region Private

    #endregion

    //protected 변수 영역
    #region protected
    //쿨타임용
    [SerializeField] protected float coolDownTime;
    //기본적으로 0보다 작은값을 가짐으로서 쿨타임이 다 돌았음을 표시
    protected float remainCoolDownTime = -1f;
    #endregion

    //Public 변수영역
    #region public
    //스킬 아래에 콜라이더를 가진 오브젝트를 넣어서 이 오브젝트에 트리거 되면 스킬 시전 시작
    //플레이어의 경우 이 오브젝트는 사용하지 않는다
    public Transform detectRange;
    #endregion
    //이벤트 함수들 영역
    #region Event
    //스킬이 실행되면 SkillType클래스에게 정보를 전달한다.
    public UnityEvent<Vector3> onSkillActivatedEvent;
    #endregion
    #endregion

    //private 함수들 영역
    #region PrivateMethod
    #endregion


    //protected 함수들 영역
    #region ProtectedMethod
    #endregion

    //public 함수들 영역
    #region PublicMethod
    #endregion

    #region Coroutine
    protected IEnumerator CoolDownChecking()
    {
        remainCoolDownTime = coolDownTime;
        while (remainCoolDownTime > 0)
        {
            remainCoolDownTime -= Time.deltaTime;
            yield return null;
        }

        yield return null;
    }
    #endregion


    //이벤트가 일어났을때 실행되는 On~~함수
    #region EventHandler
    //이 스킬이 사용되었을때
    public void OnUse(Vector3 targetPos)
    {
        //쿨타임이 아직 남아있으면 아예 invoke 자체가 일어나지 않음으로서 쿨타임 구현
        if (remainCoolDownTime <= 0)
        {
            onSkillActivatedEvent?.Invoke(targetPos);
            StartCoroutine(CoolDownChecking());
        }
    }
    #endregion


    //유니티 함수들 영역
    #region MonoBehaviour
    private void Awake()
    {
        
    }
    #endregion
}