using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackSkillType : BaseSkillType
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
    //히트박스에 들어갔는지 아닌지 체크하는 코루틴
    protected abstract IEnumerator HitChecking(GameObject hitBox);
    #endregion


    //이벤트가 일어났을때 실행되는 On~~함수
    #region EventHandler
    #endregion


    //유니티 함수들 영역
    #region MonoBehaviour
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    #endregion
}
