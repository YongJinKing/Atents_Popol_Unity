using UnityEngine;

public class KnockBackSkillEffect : BaseSkillEffect
{
    //���� ����
    #region Properties / Field
    //private ���� ����
    #region Private
    #endregion

    //protected ���� ����
    #region protected
    [SerializeField] protected float _knockBackPower;
    [SerializeField] protected float _knockUpPower;
    #endregion

    //Public ��������
    #region public
    public float knockBackPower
    {
        get { return _knockBackPower; } 
        set { _knockBackPower = value;}
    }
    public float knockUpPower
    {
        get { return _knockUpPower; }
        set { _knockUpPower = value; }
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


    //�̺�Ʈ�� �Ͼ���� ����Ǵ� On~~�Լ�
    #region EventHandler
    public override void OnSkillHit(Collider target, Vector3 pos)
    {
        Rigidbody rb = target.attachedRigidbody;
        if(rb == null) return;

        Vector3 dir = target.transform.position - transform.position;
        dir.Normalize();

        rb.AddForce(dir * knockBackPower, ForceMode.Impulse);
        rb.AddForce(Vector3.up * knockUpPower, ForceMode.Impulse);
    }
    #endregion


    //����Ƽ �Լ��� ����
    #region MonoBehaviour
    #endregion

}
