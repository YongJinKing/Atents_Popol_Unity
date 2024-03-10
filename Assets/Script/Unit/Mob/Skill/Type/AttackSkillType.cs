using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackSkillType : BaseSkillType
{
    //���� ����
    #region Properties / Field
    //private ���� ����
    #region Private

    #endregion

    //protected ���� ����
    #region protected
    //Ÿ���� �÷��̾�� �÷��� ���̾�� �ϰ� ���͸� ���� ���̾�
    [SerializeField] protected LayerMask targetMask;
    //��Ʈ�ڽ��� ���ӵǴ� �ð�
    [SerializeField] protected float hitDuration;
    //���� ���ӽð��� ����ϱ� ���� ���� serial�� �׳� ���� �پ����� Ȯ���ϱ� ���Ѱ����� �����Ϸ��� ������� �ƴϴ�.
    [SerializeField] protected float remainDuration;
    //�ν���Ÿ����ȭ�� areaOfEffectPrefeb�� �����ϴ°�
    [SerializeField] protected GameObject[] areaOfEffect;
    //areaOfEffect�� �ִ� ����� ������̳ĸ� �����ϱ� ����
    //������� ����ü Ŭ������ ��쿡�� 2�� �̻����� ����� ������ �߻� �Ҽ� �ֵ���
    [SerializeField] protected int maxIndex;
    #endregion

    //Public ��������
    #region public
    //��ų ���ݹ���������� ������ ������Ʈ ������
    public GameObject areaOfEffectPrefeb;
    //������ �¾����� ������ ����Ʈ
    public GameObject hitEffectPrefeb;
    //��ų�� ���۵� ��ġ
    public Transform[] attackStartPos;
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
    protected virtual void InitAreaOfEffect()
    {
        for (int i = 0; i < maxIndex; i++)
        {
            if (areaOfEffect[i] == null)
            {
                areaOfEffect[i] = Instantiate(areaOfEffectPrefeb);
                areaOfEffect[i].transform.SetParent(attackStartPos[i].transform, false);
                areaOfEffect[i].transform.position = attackStartPos[i].position;
                areaOfEffect[i].SetActive(false);
            }
        }
    }

    //�������� ����Ʈ�� �������� �ϴ� �Լ�
    protected virtual void HitEffectPlay(Vector3 hitBoxPos, Vector3 targetPos)
    {
        if (hitEffectPrefeb != null)
        {
            Ray ray = new Ray(hitBoxPos, targetPos - hitBoxPos);
            if (Physics.Raycast(ray, out RaycastHit hit, 10f, targetMask))
            {
                Instantiate(hitEffectPrefeb, hit.point, Quaternion.identity);
            }
        }
    }
    #endregion

    //public �Լ��� ����
    #region PublicMethod
    #endregion
    #endregion


    #region Coroutine
    //��Ʈ�ڽ��� ������ �ƴ��� üũ�ϴ� �ڷ�ƾ
    protected abstract IEnumerator HitChecking(GameObject hitBox);
    #endregion


    //�̺�Ʈ�� �Ͼ���� ����Ǵ� On~~�Լ�
    #region EventHandler
    #endregion


    //����Ƽ �Լ��� ����
    #region MonoBehaviour
    protected override void Start()
    {
        InitAreaOfEffect();
    }
    #endregion
}
