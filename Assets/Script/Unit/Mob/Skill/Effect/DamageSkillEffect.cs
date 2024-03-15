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
    public AttackType Atype;
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


    //�̺�Ʈ�� �Ͼ���� ����Ǵ�?On~~�Լ�
    #region EventHandler
    public override void OnSkillHit(Collider target)
    {
        //Debug.Log(target.name);
        IDamage damage = target.GetComponentInParent<IDamage>();
        IGetDType Dtype = target.GetComponentInParent<IGetDType>();

        if (damage != null && Dtype != null)
        {
            Debug.Log($"Dmg : {power * myBattleSystem.AP}\nAtype : {Atype}");

            //int myattackPoint = GetComponentInParent<BattleSystem>().battlestat.AP;
            damage.TakeDamage((int)(power * myBattleSystem.AP), Atype, Dtype.GetDType(target));
        }
    }
    #endregion


    //����Ƽ �Լ��� ����
    #region MonoBehaviour
    #endregion

}
