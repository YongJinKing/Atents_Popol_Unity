using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class TurtleShell : Monster
{
    public Transform ptarget;



    protected override void Start()
    {
        base.Start();

        myState = State.Idle;

        Invoke("ChaseStart", 2);
    }

    void ChaseStart()
    {
        ChangeState(State.Closing);
    }


    void Update()
    {
    }
    void StopMoving()
    {
        myAnim.SetBool("IsMoving", false); // �̵� ���� �ִϸ��̼����� ����
    }



    protected override void ChangeState(State s)
    {
        if (myState == s) return;
        myState = s;

        stopEvent?.Invoke(() => myAnim.SetBool("b_IsMoving", false));

        switch (myState)
        {
            case State.Idle:
                ProcessState();
                break;
            case State.Closing:
                ProcessState();
                break;
            case State.Attacking:
                myAnim.SetTrigger("t_Attack");
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
                {
                    float dist = 2.0f;
                    //��ǥ ���ؼ�
                    Vector3 dir = new Vector3(1,1,1);
                    dir.Normalize();
                    

                    Vector3 targetPos = transform.position + dir * dist;
                    Vector3 temp = new Vector3(UnityEngine.Random.Range(0, 2f), 0, UnityEngine.Random.Range(0, 2f));

                    targetPos = targetPos + temp;

                    //��ǥ�� ����
                    onMovementEvent?.Invoke(targetPos, 
                        battleStat.Speed, 
                        () => myAnim.SetBool("b_IsMoving", true),
                        () => myAnim.SetBool("b_IsMoving", false));

                    StartCoroutine(DelayChangeState(State.Closing, 3f));
                }
                break;
            //������ ����
            case State.Closing:
                {
                    followEvent?.Invoke(
                        ptarget,
                        battleStat.Speed,
                        () => myAnim.SetBool("b_IsMoving", true),
                        () =>
                        {
                            myAnim.SetBool("b_IsMoving", false);
                            ChangeState(State.Attacking);
                        });
                    skills[0].OnCommandDetectSkillTarget(() => ChangeState(State.Attacking));
                }
                break;
            //����
            case State.Attacking:
                onSkillStartAct?.Invoke(target.transform.position);
                StartCoroutine(DelayChangeState(State.Idle, 2.0f));
                break;
        }
    }

    private IEnumerator DelayChangeState(State s, float time)
    {
        yield return new WaitForSeconds(time);

        ChangeState(s);
    }
}