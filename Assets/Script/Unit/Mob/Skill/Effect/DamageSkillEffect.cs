using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSkillEffect : BaseSkillEffect
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
    public override void OnSkillHit(GameObject target)
    {
        IDamage damage = target.GetComponent<IDamage>();
        if (damage != null)
        {
            //int myattackPoint = GetComponentInParent<BattleSystem>().battlestat.AP;
            damage.TakeDamage((uint)power * 2);
        }
    }
    #endregion


    //����Ƽ �Լ��� ����
    #region MonoBehaviour
    #endregion

}
