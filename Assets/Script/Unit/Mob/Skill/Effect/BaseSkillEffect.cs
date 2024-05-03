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
    protected BattleSystem myBattleSystem = null;
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
    public void InitializeBattleSystem(BattleSystem bs)
    {
        myBattleSystem = bs;
    }
    #endregion
    #endregion


    #region Coroutine
    #endregion


    //�̺�Ʈ�� �Ͼ���� ����Ǵ� On~~�Լ�
    #region EventHandler
    public abstract void OnSkillHit(Collider target);
    #endregion


    //����Ƽ �Լ��� ����
    #region MonoBehaviour
    protected virtual void Start()
    {
        if(myBattleSystem == null)
            myBattleSystem = GetComponentInParent<BattleSystem>();
    }
    #endregion

}
