using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    //���� ����
    #region Properties / Field
    //private ���� ����
    #region Private
    //Ÿ���� �÷��̾�� �÷��� ���̾�� �ϰ� ���͸� ���� ���̾�
    [SerializeField] private LayerMask targetMask;
    #endregion

    //protected ���� ����
    #region protected

    #endregion

    //Public ��������
    #region public
    //��ų �Ʒ��� �ݶ��̴��� ���� ������Ʈ�� �־ �� ������Ʈ�� Ʈ���� �Ǹ� ��ų ���� ����
    //�÷��̾��� ��� �� ������Ʈ�� ������� �ʴ´�
    public Transform detectRange;
    //������ Ÿ�������� �� ������Ʈ(����ü�����)
    public Transform areaOfEffect;

    #endregion
    //�̺�Ʈ �Լ��� ����
    #region Event
    #endregion
    #endregion

    //public �Լ��� ����
    #region PublicMethod
    #endregion

    //private �Լ��� ����
    #region PrivateMethod
    #endregion

    //�̺�Ʈ�� �Ͼ���� ����Ǵ� On~~�Լ�
    #region EventHandler
    public void OnUse()
    {

    }
    #endregion


    //����Ƽ �Լ��� ����
    #region MonoBehaviour
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    #endregion
}