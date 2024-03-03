using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Slime : Monster
{
    //���� ����
    #region Properties / Field
    //private ���� ����
    #region Private
    #endregion

    //protected ���� ����
    #region protected
    //��ų�� Ÿ�����ϴ� ���̾ �޾ƿ´�.
    protected LayerMask skillMask;
    #endregion

    //Public ��������
    #region public
    #endregion

    //�̺�Ʈ �Լ��� ����
    #region Event
    //Skill�� �����ų �̺�Ʈ
    public UnityAction<Vector3> onSkillStartEvent;
    public UnityAction onSkillEndEvent;
    //Skill�� ������ Ÿ���� detect �ϱ����� �̺�Ʈ �迭(���⿡ ��ų�� ���)
    public UnityEvent<UnityAction>[] onDetectSkillTargetEvent;
    #endregion
    #endregion


    #region Method
    //private �Լ��� ����
    #region PrivateMethod
    #endregion

    //protected �Լ��� ����
    #region ProtectedMethod
    protected override void ChangeState(State s)
    {
        if (s == myState) return;
        myState = s;

        StopAllCoroutines();
        switch (myState)
        {
            //���� ������ �ٰŸ����� ��ȸ
            case State.Idle:
                ProcessState();
                break;
            //������ ����
            case State.Closing:
                ProcessState();
                break;
            //����
            case State.Attacking:
                ProcessState();
                break;
        }
    }
    protected override void ProcessState()
    {
        switch (myState)
        {
            //���� ������ �ٰŸ����� ��ȸ
            case State.Idle:
                StartCoroutine(IdleProcessing());
                break;
            //������ ����
            case State.Closing:
                //detect�� �����϶�� ����
                onDetectSkillTargetEvent[1]?.Invoke(() => ChangeState(State.Attacking));
                StartCoroutine(ClosingToTarget());
                break;
            //����
            case State.Attacking:
                myAnim.SetTrigger("t_Attack");
                onSkillStartEvent?.Invoke(target.transform.position);
                ChangeState(State.Idle);
                break;
        }
    }
    #endregion

    //public �Լ��� ����
    #region PublicMethod
    #endregion
    #endregion


    //�ڷ�ƾ ����
    #region Coroutine
    private IEnumerator ClosingToTarget()
    {
        Vector3 dir;


        float delta = 0.0f;
        while (myState == State.Closing)
        {
            //�����ϴ� �ݺ���
            dir = target.transform.position - transform.position;
            dir = new Vector3(dir.x, 0, dir.z);
            dir.Normalize();

            delta += Time.deltaTime;
            transform.Translate(dir * delta * 0.1f);

            yield return null;
        }
        yield return null;
    }


    //�ϴ� Ư�� �Ÿ����� ��ٸ��� AI
    private IEnumerator IdleProcessing()
    {
        float IdleTime = 2.0f;

        Vector3 dir = -(target.transform.position - transform.position);
        dir = new Vector3(dir.x, 0, dir.z);
        dir.Normalize();

        float delta = 0.0f;
        while (IdleTime >= 0.0f)
        {
            IdleTime -= Time.deltaTime;

            //��ȸ�ϴ� �ڵ��
            delta += Time.deltaTime;
            transform.Translate(dir * delta * 0.1f);

            yield return null;
        }
        ChangeState(State.Closing);
        yield return null;
    }
    #endregion


    //�̺�Ʈ�� �Ͼ���� ����Ǵ� On~~�Լ�
    #region EventHandler
    public void OnAddSkillEventListener(UnityAction<Vector3> skillStart, UnityAction skillEnd, LayerMask mask)
    {
        onSkillStartEvent = skillStart;
        onSkillEndEvent = skillEnd;
        skillMask = mask;
    }
    #endregion


    //����Ƽ �Լ��� ����
    #region MonoBehaviour
    void Start()
    {
        ProcessState();
    }

    // Update is called once per frame
    void Update()
    {
        /*

        if (Input.GetButtonDown("Jump"))
        {
            UseSkill1();
        }
        if (Input.GetButtonDown("Fire1"))
        {
            UseSkill2();
        }
        */
    }
    #endregion
}