using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfSkillType : BaseSkillType
{
    //변???�역
    #region Properties / Field
    //private 변???�역
    #region Private
    #endregion

    //protected 변???�역
    #region protected
    //부모의 Unit??참조???�브?�트
    protected BattleSystem selfObject;
    #endregion

    //Public 변?�영??
    #region public
    #endregion

    //?�벤???�수???�역
    #region Event
    #endregion
    #endregion


    #region Method
    //private ?�수???�역
    #region PrivateMethod
    #endregion

    //protected ?�수???�역
    #region ProtectedMethod
    #endregion

    //public ?�수???�역
    #region PublicMethod
    #endregion
    #endregion


    #region Coroutine
    #endregion


    //?�벤?��? ?�어?�을???�행?�는 On~~?�수
    #region EventHandler
    #endregion


    //?�니???�수???�역
    #region MonoBehaviour
    protected override void Start()
    {
        selfObject = GetComponentInParent<BattleSystem>();
    }
    #endregion
}
