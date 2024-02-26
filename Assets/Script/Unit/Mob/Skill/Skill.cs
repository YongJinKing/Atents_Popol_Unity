using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//��ų�� ���������� ó������ ����Ǵ� Ŭ����
public class Skill : MonoBehaviour
{
    //���� ����
    #region Properties / Field
    //private ���� ����
    #region Private

    #endregion

    //protected ���� ����
    #region protected
    //��Ÿ�ӿ�
    [SerializeField] protected float coolDownTime;
    //�⺻������ 0���� �������� �������μ� ��Ÿ���� �� �������� ǥ��
    protected float remainCoolDownTime = -1f;
    #endregion

    //Public ��������
    #region public
    //��ų �Ʒ��� �ݶ��̴��� ���� ������Ʈ�� �־ �� ������Ʈ�� Ʈ���� �Ǹ� ��ų ���� ����
    //�÷��̾��� ��� �� ������Ʈ�� ������� �ʴ´�
    public Transform detectRange;
    #endregion
    //�̺�Ʈ �Լ��� ����
    #region Event
    //��ų�� ����Ǹ� SkillTypeŬ�������� ������ �����Ѵ�.
    public UnityEvent<Vector3> onSkillActivatedEvent;
    #endregion
    #endregion

    //private �Լ��� ����
    #region PrivateMethod
    #endregion


    //protected �Լ��� ����
    #region ProtectedMethod
    #endregion

    //public �Լ��� ����
    #region PublicMethod
    #endregion

    #region Coroutine
    protected IEnumerator CoolDownChecking()
    {
        remainCoolDownTime = coolDownTime;
        while (remainCoolDownTime > 0)
        {
            remainCoolDownTime -= Time.deltaTime;
            yield return null;
        }

        yield return null;
    }
    #endregion


    //�̺�Ʈ�� �Ͼ���� ����Ǵ� On~~�Լ�
    #region EventHandler
    //�� ��ų�� ���Ǿ�����
    public void OnUse(Vector3 targetPos)
    {
        //��Ÿ���� ���� ���������� �ƿ� invoke ��ü�� �Ͼ�� �������μ� ��Ÿ�� ����
        if (remainCoolDownTime <= 0)
        {
            onSkillActivatedEvent?.Invoke(targetPos);
            StartCoroutine(CoolDownChecking());
        }
    }
    #endregion


    //����Ƽ �Լ��� ����
    #region MonoBehaviour
    private void Awake()
    {
        
    }
    #endregion
}