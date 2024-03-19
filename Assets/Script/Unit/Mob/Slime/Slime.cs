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
    #endregion

    //Public ��������
    #region public
    public float backStapOffset;
    #endregion

    //�̺�Ʈ �Լ��� ����
    #region Event
    #endregion
    #endregion


    #region Method
    //private �Լ��� ����
    #region PrivateMethod
    private void SkillRandomSet()
    {
        //��ų�� �ٽ�����
        for(int i = 0; i < skills.Length; i++)
        {
            saveSkill[i] = UnityEngine.Random.Range(0, skills.Length);
            
            //����� �� ������ �ٽ�
            for(int j = 0; j < i; j++)
            {
                if (saveSkill[j] == saveSkill[i])
                {
                    saveSkill[i] = UnityEngine.Random.Range(0, 
                        skills.Length);
                    j = 0;
                }
            }
        }
        countUsedSkill = 0;
    }
    #endregion

    //protected �Լ��� ����
    #region ProtectedMethod
    protected override void ChangeState(State s)
    {
        if (s == myState) return;
        myState = s;

        StopAllCoroutines();
        stopEvent?.Invoke(null);

        switch (myState)
        {
            //���� ������ �ٰŸ����� ��ȸ
            case State.Idle:
                if (saveSkill[countUsedSkill] == 1)
                {
                    myAnim.SetBool("b_LoopSkill", true);
                }
                else
                {
                    myAnim.SetBool("b_LoopSkill", false);
                }
                ProcessState();
                break;
            //������ ����
            case State.Closing:
                ProcessState();
                break;
            //����
            case State.Attacking:
                Vector3 dir = target.transform.position - transform.position;
                dir = new Vector3(dir.x, 0, dir.z);
                dir.Normalize();

                rotateEvent?.Invoke(dir, 1.0f);
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
                skills[saveSkill[countUsedSkill]].OnRequestSkillInfo();
                StartCoroutine(IdleProcessing());
                break;
            //������ ����
            case State.Closing:
                //detect�� �����϶�� ����
                skills[saveSkill[countUsedSkill]].OnCommandDetectSkillTarget(
                    () => 
                    {
                        ChangeState(State.Attacking); 
                    });
                //Debug.Log(saveSkill[countUsedSkill]);
                StartCoroutine(ClosingToTarget());
                break;
            //����
            case State.Attacking:
                myAnim.SetBool("b_isSkillProgress", true);
                myAnim.SetTrigger("t_SkillStart");
                //myAnim.SetTrigger("t_ChannelingSkill");
                //Skill Start hitCheck after delayMotion
                onSkillStartAct?.Invoke(target.transform.position,
                    () => myAnim.SetTrigger("t_AttackStart"),
                    () => myAnim.SetTrigger("t_AttackEnd"),
                    () =>
                    {
                        myAnim.SetBool("b_isSkillProgress", false);
                        countUsedSkill++;
                        if (countUsedSkill >= skills.Length)
                        {
                            SkillRandomSet();
                        }
                        ChangeState(State.Idle);
                    });
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
        followEvent?.Invoke(target.transform, battleStat.Speed, null, null);


        yield return null;
    }


    //�ϴ� Ư�� �Ÿ����� ��ٸ��� AI
    private IEnumerator IdleProcessing()
    {
        //���� Ÿ���� ã�´�.
        yield return StartCoroutine(FindTarget());

        float IdleTime = 2.0f;

        //�̵��� ��ǥ ���ϱ�
        Vector3 dir = -(target.transform.position - transform.position);
        dir = new Vector3(dir.x, 0, dir.z);
        dir.Normalize();

        //�� �������� ������ ��ŭ �̵��� �ڿ� ������ �������� ��ǥ�� �� backStepPos �� ����
        Vector3 backStepPos = (transform.position + dir * backStapOffset);
        Vector3 backStepDir = Quaternion.Euler(0, UnityEngine.Random.Range(0, 360f), 0) * dir;
        backStepDir.Normalize();

        float dist = UnityEngine.Random.Range(2f, 5f);

        while(Vector3.Dot(dir, backStepDir) < 0)
        {
            backStepDir = Quaternion.Euler(0, UnityEngine.Random.Range(0, 360f), 0) * dir;
        }

        backStepPos = backStepPos + backStepDir * dist;

        //�̵� �̺�Ʈ
        onMovementEvent?.Invoke(backStepPos, battleStat.Speed, null, null);

        //idle �ð� ���
        while (IdleTime >= 0.0f)
        {
            IdleTime -= Time.deltaTime;
            yield return null;
        }
        //�ð� ������ �̵� ����
        stopEvent?.Invoke(null);

        //���� �ٲ�
        ChangeState(State.Closing);
        yield return null;
    }

    protected IEnumerator FindTarget()
    {
        if (skillMask == 0)
        {
            Debug.Log("Can`t find SkillMask");
            yield break;
        }

        Collider[] tempcol;

        bool isFindTarget = false;
        while (!isFindTarget)
        {
            tempcol = Physics.OverlapSphere(transform.position, 200, skillMask);

            if (tempcol != null)
            {
                target = tempcol[0].gameObject;
                isFindTarget = true;
            }
            yield return null;
        }
    }
    #endregion


    //�̺�Ʈ�� �Ͼ���� ����Ǵ� On~~�Լ�
    #region EventHandler

    //���ݸ���� ��ų�� �ߵ�
    public void OnAttackStartAnim()
    {
        onSkillHitCheckStartAct?.Invoke();
    }

    //���� ����߿� ��Ʈ�ڽ� �� ����
    public void OnAttackEndAnim()
    {
        onSkillHitCheckEndAct?.Invoke();
    }

    //Attack Animation End
    public void OnSkillAnimEnd()
    {
        onSkillAnimEnd?.Invoke();
    }
    #endregion

    //����Ƽ �Լ��� ����
    #region MonoBehaviour
    protected override void Start()
    {
        base.Start();

        //��ų ���� ���
        saveSkill = new int[skills.Length];
        countUsedSkill = 0;
        SkillRandomSet();

        ProcessState();
    }
    #endregion
}