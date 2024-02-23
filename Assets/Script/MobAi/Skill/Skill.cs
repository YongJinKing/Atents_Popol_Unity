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
    public Transform attackStartPos;
    //������ Ÿ�������� �� ������Ʈ(����ü�����)
    public GameObject areaOfEffect;
    public LayerMask tempLayerMask;

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
    //�� ��ų�� ���Ǿ�����
    public void OnUse()
    {
        Vector3 size = new Vector3(
        areaOfEffect.transform.position.x * areaOfEffect.transform.lossyScale.x,
        areaOfEffect.transform.position.y * areaOfEffect.transform.lossyScale.y,
        areaOfEffect.transform.position.z * areaOfEffect.transform.lossyScale.z
        );
        Collider[] tempcol = Physics.OverlapBox(areaOfEffect.transform.position, size, Quaternion.identity, tempLayerMask);
        for(int i = 0; i < tempcol.Length; i++)
        {
            //Debug.Log(Collider.Game)
        }
    }
    #endregion


    //����Ƽ �Լ��� ����
    #region MonoBehaviour
    // Start is called before the first frame update
    void Start()
    {
        areaOfEffect.transform.position = attackStartPos.position;
    }

    // Update is called once per frame
    void Update()
    {

    }
    #endregion
}