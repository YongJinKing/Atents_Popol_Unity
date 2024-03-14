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
        Debug.Log(target.name);
        IDamage damage = target.GetComponent<IDamage>();

        if (damage != null)
        {
            Debug.Log(power * myBattleSystem.AP);

            //int myattackPoint = GetComponentInParent<BattleSystem>().battlestat.AP;
            damage.TakeDamage((int)(power * myBattleSystem.AP));
        }
    }
    #endregion


    //����Ƽ �Լ��� ����
    #region MonoBehaviour
    #endregion

}
