using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    //Ÿ���� �÷��̾�� �÷��� ���̾�� �ϰ� ���͸� ���� ���̾�
    [SerializeField]private LayerMask targetMask;
    //��ų �Ʒ��� �ݶ��̴��� ���� ������Ʈ�� �־ �� ������Ʈ�� Ʈ���� �Ǹ� ��ų ���� ����
    //�÷��̾��� ��� �� ������Ʈ�� ������� �ʴ´�
    public Transform detectRange;
    //������ Ÿ�������� �� ������Ʈ(����ü�����)
    public Transform areaOfEffect;

}