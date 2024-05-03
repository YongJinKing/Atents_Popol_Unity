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
    [SerializeField] protected float _power;
    [SerializeField] protected AttackType _Atype;
    #endregion

    //Public ��������
    #region public
    public float power
    {
        get { return _power; }
        set { _power = value; }
    }
    public AttackType Atype
    {
        get { return _Atype; }
        set { _Atype = value; }
    }
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

        if (damage != null)
        {
            float temp = myBattleSystem.GetModifiedStat(E_BattleStat.ATK);
            //Debug.Log($"Dmg : {power * temp}\nAtype : {Atype}");

            if (Dtype != null)
            {
                damage.TakeDamage((int)(power * temp), Atype, Dtype.GetDType(target));
            }
            else
            {
                damage.TakeDamage((int)(power * temp), Atype, DefenceType.HeavyArmor);
            }
        }
    }
    #endregion


    //����Ƽ �Լ��� ����
    #region MonoBehaviour
    #endregion

}
