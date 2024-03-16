using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.PlayerSettings;

//��ų�� ���������� ó������ ����Ǵ� Ŭ����
public class Skill : MonoBehaviour
{
    //���� ����
    #region Properties / Field
    //private ���� ����
    #region Private
    private Vector3 targetPos;
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
    //�� ���������� ���� �����ؼ� overlapSphere�� ���� �����Ѵ�
    //AI������ ����Ѵ�.
    public float detectRadius;
    //AI������ ����� ����ũ
    public LayerMask targetMask;
    //UI ��
    public uiUnitSkillStatus uiSkillStatus;
    //��������
    public float preDelay;
    //�ĵ�����
    public float postDelay;
    #endregion

    //�̺�Ʈ �Լ��� ����
    #region Event
    //��ų�� ����Ǹ� SkillTypeŬ�������� ������ �����Ѵ�.
    public UnityEvent<Vector3> onSkillActivatedEvent;
    public UnityEvent onSkillHitCheckStartEvent;
    public UnityEvent onSkillHitCheckEndEvent;
    //��ų�� ��밡�������� �߻��ϴ� �̺�Ʈ
    //public UnityEvent onSkillAvailableEvent;
    //Ÿ���� �������� �˷��ִ� �̺�Ʈ
    public UnityEvent onDetectTargetEvent;
    //AI���� ��ųStart�� ��ųEnd�� ��Ͻ����ִ� �̺�Ʈ
    public UnityEvent<UnityAction<Vector3, UnityAction, UnityAction>,UnityAction ,UnityAction, LayerMask> onAddSkillEventListener;
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
    protected IEnumerator CoolDownChecking()
    {
        remainCoolDownTime = coolDownTime;
        while (remainCoolDownTime > 0)
        {
            remainCoolDownTime -= Time.deltaTime;
            yield return null;
        }


        //onSkillAvailableEvent?.Invoke();
        yield return null;
    }

    protected IEnumerator DetectingRange()
    {
        bool isDetecting = false;

        while (!isDetecting)
        {
            Collider[] tempcol = Physics.OverlapSphere(transform.position, detectRadius, targetMask);

            for (int i = 0; i < tempcol.Length; i++)
            {
                if(tempcol[i] != null)
                {
                    onDetectTargetEvent?.Invoke();
                    onDetectTargetEvent.RemoveAllListeners();
                    isDetecting = true;
                }
            }
            yield return null;
        }
    }

    protected IEnumerator ProcessDelay(float delayTime, UnityAction act)
    {
        yield return new WaitForSeconds(delayTime);
        act?.Invoke();
    }
    #endregion


    //�̺�Ʈ�� �Ͼ���� ����Ǵ� On~~�Լ�
    #region EventHandler
    public void OnRequestSkillInfo()
    {
        onAddSkillEventListener?.Invoke(OnSkillStart, OnSkillHitCheckStart,  OnSkillEnd, targetMask);
    }

    //AI�� ����� �̺�Ʈ �Լ�, collider�� �ɸ��� �̺�Ʈ�� invoke�� �ڷ�ƾ�� ��ŸƮ
    public void OnCommandDetectSkillTarget(UnityAction detectAct)
    {
        //onAddSkillEventListener?.Invoke(OnSkillStart, OnSkillEnd, targetMask);
        onDetectTargetEvent.AddListener(detectAct);
        StartCoroutine(DetectingRange());
    }

    //public void OnRequestSkill()

    //�� ��ų�� ���Ǿ�����
    public void OnSkillStart(Vector3 targetPos, UnityAction startAct, UnityAction endAct)
    {
        //��Ÿ���� ���� ���������� �ƿ� invoke ��ü�� �Ͼ�� �������μ� ��Ÿ�� ����
        if (remainCoolDownTime <= 0)
        {
            this.targetPos = targetPos;
            //preDelayTime Process
            StartCoroutine(ProcessDelay(preDelay,
                () => 
                {
                    startAct?.Invoke();
                    onSkillActivatedEvent?.Invoke(targetPos);
                }
                ));
            //For Testcode
            OnSkillEnd();
        }
    }

    public void OnSkillHitCheckStart()
    {
        onSkillHitCheckStartEvent?.Invoke();
    }

    public void OnSkillHitCheckEnd()
    {
        onSkillHitCheckEndEvent?.Invoke();
    }

    //��ų�� ���� ������ ��ų ��Ÿ�� ������
    public void OnSkillEnd()
    {
        StartCoroutine(CoolDownChecking());
    }

    #endregion


    //����Ƽ �Լ��� ����
    #region MonoBehaviour


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        //Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = new UnityEngine.Color(0, 1f, 0, 0.2f);
        Gizmos.DrawSphere(transform.position, detectRadius);
    }
#endif


    #endregion
}