using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SideMovementSkillType : SelfSkillType
{
    //���� ����
    #region Properties / Field
    //private ���� ����
    #region Private
    #endregion

    //protected ���� ����
    #region protected
    [SerializeField] protected float _radius = 10;
    #endregion

    //Public ��������
    #region public
    public float radius
    {
        get { return _radius; }
        set { _radius = value; }
    }
    #endregion

    //�̺�Ʈ �Լ��� ����
    #region Event
    //For use UnitMovement Class
    public UnityEvent<Transform, Info<float, float>, UnityAction, UnityAction> sideMoveEvent;
    //When End Move
    public UnityEvent<UnityAction> stopEvent;
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
    public override void OnSkillActivated(Transform target)
    {
        base.OnSkillActivated(target);
        sideMoveEvent?.Invoke(target, new Info<float, float>(selfBS.GetModifiedStat(E_BattleStat.Speed), radius), null, null);
    }

    public void OnSkillHitCheckEndEventHandler()
    {
        stopEvent?.Invoke(null);
    }
    #endregion


    //����Ƽ �Լ��� ����
    #region MonoBehaviour
    #endregion
}
