using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InflictStatusSkillEffect : BaseSkillEffect
{
    //변수 영역
    #region Properties / Field
    //private 변수 영역
    #region Private
    #endregion

    //protected 변수 영역
    #region protected
    #endregion

    //Public 변수영역
    #region public
    E_StatusAbnormality abType;
    #endregion

    //이벤트 함수들 영역
    #region Event
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
    #endregion


    //이벤트가 일어났을때 실행되는 On~~함수
    #region EventHandler
    public override void OnSkillHit(Collider target)
    {
        Status tStatus = target.GetComponentInParent<Status>();
        tStatus.Add(abType);
    }
    #endregion


    //유니티 함수들 영역
    #region MonoBehaviour
    #endregion

}
