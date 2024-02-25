using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Mob : MonoBehaviour
{
    //변수 영역
    #region Properties / Field
    //private 변수 영역
        #region Private
    private Animator _myAnim = null;
        #endregion
    //protected 변수 영역
        #region protected
        #endregion
    //Public 변수영역
        #region public
    public Animator myAnim
    {
        get
        {
            if (_myAnim == null)
            {
                _myAnim = GetComponent<Animator>();
                if (_myAnim == null)
                {
                    _myAnim = GetComponentInChildren<Animator>();
                }
            }
            return _myAnim;
        }
    }
    #endregion

    //이벤트 함수들 영역
    #region Event
    //Skill을 실행시킬 이벤트
    public UnityEvent[] onSkillUseEvent;
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

    //코루틴 영역
    #region Coroutine
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
