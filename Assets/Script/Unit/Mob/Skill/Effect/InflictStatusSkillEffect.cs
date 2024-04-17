using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InflictStatusSkillEffect : BaseSkillEffect
{
    //���� ����
    #region Properties / Field
    //private ���� ����
    #region Private
    #endregion

    //protected ���� ����
    #region protected
    #endregion

    //Public ��������
    #region public
    E_StatusAbnormality abType;
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
    public override void OnSkillHit(Collider target)
    {
        Status tStatus = target.GetComponentInParent<Status>();
        tStatus.Add(abType);
    }
    #endregion


    //����Ƽ �Լ��� ����
    #region MonoBehaviour
    #endregion

}
