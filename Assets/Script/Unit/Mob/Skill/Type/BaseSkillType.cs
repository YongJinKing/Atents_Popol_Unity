using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    //타겟이 플레이어면 플레어 레이어로 하고 몬스터면 몬스터 레이어
    [SerializeField] protected LayerMask targetMask;
    //OnSkillActivated로 받아온 타겟의 위치를 저장함
    protected Vector3 targetPos;
    //인스턴타이즈화된 areaOfEffectPrefeb을 저장하는곳
    [SerializeField] protected GameObject[] areaOfEffect;
    //areaOfEffect를 최대 몇개까지 만들것이냐를 결정하기 위함
    //예를들어 투사체 클래스의 경우에는 2개 이상으로 만들면 여러개 발사 할수 있도록
    [SerializeField] protected int maxIndex;
    #endregion

    //Public 변수영역
    #region public
    //스킬이 시작될 위치
    public Transform[] attackStartPos;
    #endregion

    //이벤트 함수들 영역
    #region Event
    //스킬
    public UnityEvent onSkillHitEvent;
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
    #endregion


    //유니티 함수들 영역
    #region MonoBehaviour
    protected virtual void Awake()
    {
    }
    #endregion
}

