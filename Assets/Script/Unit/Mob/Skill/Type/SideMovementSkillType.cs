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
    #endregion

    //Public ��������
    #region public
    #endregion

    //�̺�Ʈ �Լ��� ����
    #region Event
    //For use UnitMovement Class
    public UnityEvent<Transform, float, UnityAction, UnityAction> sideMoveEvent;
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
    /*
    protected IEnumerator MovingPos()
    {
        Vector3 dir = targetPos - selfObject.transform.position;
        //XZ ��鿡���� �����Ӹ��� ����.
        dir = new Vector3(dir.x, 0, dir.z);
        dir.Normalize();

        moveToPosEvent?.Invoke(selfObject.transform.position + dir * maxDist, moveSpeed, null, () => onMoveEndEvent?.Invoke());

        yield return null;
    }
    */
    #endregion


    //�̺�Ʈ�� �Ͼ���� ����Ǵ� On~~�Լ�
    #region EventHandler
    public override void OnSkillActivated(Transform target)
    {
        base.OnSkillActivated(target);
        sideMoveEvent?.Invoke(target, selfBS.Speed, null, null);
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
