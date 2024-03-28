using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfSkillType : BaseSkillType
{
    //ë³€???ì—­
    #region Properties / Field
    //private ë³€???ì—­
    #region Private
    #endregion

    //protected ë³€???ì—­
    #region protected
    //ë¶€ëª¨ì˜ Unit??ì°¸ì¡°???¤ë¸Œ?íŠ¸
    protected BattleSystem selfObject;
    #endregion

    //Public ë³€?˜ì˜??
    #region public
    #endregion

    //?´ë²¤???¨ìˆ˜???ì—­
    #region Event
    #endregion
    #endregion


    #region Method
    //private ?¨ìˆ˜???ì—­
    #region PrivateMethod
    #endregion

    //protected ?¨ìˆ˜???ì—­
    #region ProtectedMethod
    #endregion

    //public ?¨ìˆ˜???ì—­
    #region PublicMethod
    #endregion
    #endregion


    #region Coroutine
    #endregion


    //?´ë²¤?¸ê? ?¼ì–´?¬ì„???¤í–‰?˜ëŠ” On~~?¨ìˆ˜
    #region EventHandler
    #endregion


    //? ë‹ˆ???¨ìˆ˜???ì—­
    #region MonoBehaviour
    protected override void Start()
    {
        selfObject = GetComponentInParent<BattleSystem>();
    }
    #endregion
}
