using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfSkillType : BaseSkillType
{
    //���� ����
    #region Properties / Field
    //private ���� ����
    #region Private
    #endregion

    //protected ���� ����
    #region protected
    //�θ��� Unit�� ������ ������Ʈ
    protected BattleSystem selfObject;
    #endregion

    //Public ��������
    #region public
    #endregion

    //�̺�Ʈ �Լ��� ����
    #region Event
    #endregion
    #endregion


    #region Method
    //private �Լ��� ����
    #region PrivateMethod
    #endregion

    //protected �Լ��� ����
    #region ProtectedMethod
    #endregion

    //public �Լ��� ����
    #region PublicMethod
    #endregion
    #endregion


    #region Coroutine
    #endregion


    //�̺�Ʈ�� �Ͼ���� ����Ǵ� On~~�Լ�
    #region EventHandler
    #endregion


    //����Ƽ �Լ��� ����
    #region MonoBehaviour
    protected override void Start()
    {
        selfObject = GetComponentInParent<BattleSystem>();
    }
    #endregion
}
