using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Slime : Monster
{
    //���� ����
    #region Properties / Field
    //private ���� ����
    #region Private
    private int[] saveSkill;
    private int countUsedSkill;
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
    public UnityEvent<UnityAction>[] onCommandDetectSkillTargetEvent;
    #endregion
    #endregion


    #region Method
    //private �Լ��� ����
    #region PrivateMethod
    private void SkillRandomSet()
    {
        //��ų�� �ٽ�����
        for(int i = 0; i < onCommandDetectSkillTargetEvent.Length; i++)
        {
            saveSkill[i] = UnityEngine.Random.Range(0, onCommandDetectSkillTargetEvent.Length);
            

            //����� �� ������ �ٽ�
            for(int j = 0; j < i; j++)
            {
                if (saveSkill[j] == saveSkill[i])
                {
                    saveSkill[i] = UnityEngine.Random.Range(0, onCommandDetectSkillTargetEvent.Length);
                    j = 0;
                }
            }
        }
        countUsedSkill = 0;

        Debug.Log(saveSkill[countUsedSkill]);
    }
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
                onCommandDetectSkillTargetEvent[saveSkill[countUsedSkill]]?.Invoke(() => ChangeState(State.Attacking));
                Debug.Log(saveSkill[countUsedSkill]);
                StartCoroutine(ClosingToTarget());
                break;
            //����
            case State.Attacking:
                myAnim.SetTrigger("t_Attack");
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
            transform.Translate(dir * delta * 0.1f, Space.World);

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
            transform.Translate(dir * delta * 0.1f, Space.World);

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

    //���ݸ���� ��ų�� �ߵ�
    public void OnAttackStartAnim()
    {
        onSkillStartEvent?.Invoke(target.transform.position);
        countUsedSkill++;
        if (countUsedSkill >= onCommandDetectSkillTargetEvent.Length)
        {
            SkillRandomSet();
        }
    }

    //���� ����� ����
    public void OnAttackEndAnim()
    {
        onSkillEndEvent?.Invoke();
        ChangeState(State.Idle);
    }
    #endregion


    //����Ƽ �Լ��� ����
    #region MonoBehaviour
    void Start()
    {
        saveSkill = new int[onCommandDetectSkillTargetEvent.Length];
        countUsedSkill = 0;
        SkillRandomSet();
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