using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public struct uiMonsterSkillStatus
{
    public Sprite uiSkillSprite;
    public string uiSkillName;
    public string uiSkillDesc;
}

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
    //이 반지름으로 구를 생성해서 overlapSphere로 적을 검출한다
    //AI에서만 사용한다.
    public float detectRadius;
    //AI에서만 사용할 마스크
    public LayerMask targetMask;
    //UI 용
    public uiMonsterSkillStatus uiSkillStatus;
    #endregion

    //이벤트 함수들 영역
    #region Event
    //스킬이 실행되면 SkillType클래스에게 정보를 전달한다.
    public UnityEvent<Vector3> onSkillActivatedEvent;
    //스킬이 사용가능해지면 발생하는 이벤트
    //public UnityEvent onSkillAvailableEvent;
    //타겟이 들어왔음을 알려주는 이벤트
    public UnityEvent onDetectTargetEvent;
    //AI에게 스킬Start와 스킬End를 등록시켜주는 이벤트
    public UnityEvent<UnityAction<Vector3>, UnityAction, LayerMask> onAddSkillEventListener;
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

    #region Coroutine
    protected IEnumerator CoolDownChecking()
    {
        remainCoolDownTime = coolDownTime;
        while (remainCoolDownTime > 0)
        {
            remainCoolDownTime -= Time.deltaTime;
            yield return null;
        }


        //onSkillAvailableEvent?.Invoke();
        yield return null;
    }

    protected IEnumerator DetectingRange()
    {
        bool isDetecting = false;

        while (!isDetecting)
        {
            Collider[] tempcol = Physics.OverlapSphere(transform.position, detectRadius, targetMask);

            for (int i = 0; i < tempcol.Length; i++)
            {
                if(tempcol[i] != null)
                {
                    onDetectTargetEvent?.Invoke();
                    onDetectTargetEvent.RemoveAllListeners();
                    isDetecting = true;
                }
            }
            yield return null;
        }

    }
    #endregion


    //이벤트가 일어났을때 실행되는 On~~함수
    #region EventHandler
    //AI가 사용할 이벤트 함수, collider에 걸리면 이벤트를 invoke할 코루틴을 스타트
    public void OnCommandDetectSkillTarget(UnityAction detectAct)
    {
        onAddSkillEventListener?.Invoke(OnSkillStart, OnSkillEnd, targetMask);
        onDetectTargetEvent.AddListener(detectAct);
        StartCoroutine(DetectingRange());
    }

    //public void OnRequestSkill()

    //이 스킬이 사용되었을때
    public void OnSkillStart(Vector3 targetPos)
    {
        //쿨타임이 아직 남아있으면 아예 invoke 자체가 일어나지 않음으로서 쿨타임 구현
        if (remainCoolDownTime <= 0)
        {
            onSkillActivatedEvent?.Invoke(targetPos);
            //밑은 테스트용으로 씀
            OnSkillEnd();
        }
    }

    //스킬이 시전 끝나고 스킬 쿨타임 돌릴때
    public void OnSkillEnd()
    {
        StartCoroutine(CoolDownChecking());
    }

    #endregion


    //유니티 함수들 영역
    #region MonoBehaviour
    #endregion
}