using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Mob : MonoBehaviour
{
    //���� ����
    #region Properties / Field
    //private ���� ����
        #region Private
    private Animator _myAnim = null;
        #endregion
    //protected ���� ����
        #region protected
        #endregion
    //Public ��������
        #region public
    public Animator myAnim
    {
        get
        {
            if (_myAnim == null)
            {
                _myAnim = GetComponent<Animator>();
                if (_myAnim == null)
                {
                    _myAnim = GetComponentInChildren<Animator>();
                }
            }
            return _myAnim;
        }
    }
    #endregion

    //�̺�Ʈ �Լ��� ����
    #region Event
    //Skill�� �����ų �̺�Ʈ
    public UnityEvent[] onSkillUseEvent;
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

    //�ڷ�ƾ ����
    #region Coroutine
    #endregion

    //�̺�Ʈ�� �Ͼ���� ����Ǵ� On~~�Լ�
    #region EventHandler

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
