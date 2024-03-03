using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSkillEffect : MonoBehaviour
{
    //���� ����
    #region Properties / Field
    //private ���� ����
    #region Private

    #endregion

    //protected ���� ����
    #region protected
    //���ݷ� * power�� ��ų�� ����
    [SerializeField] protected float power;
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
    public abstract void OnSkillHit(GameObject target);
    #endregion


    //����Ƽ �Լ��� ����
    #region MonoBehaviour
    #endregion

}
