using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class TurtleShell : Monster
{
    public Transform ptarget;
    public bool isChase;
    NavMeshAgent nav;
    Animator anim;



    protected override void Start()
    {
        base.Start();

        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();

        myState = State.Idle;

        Invoke("ChaseStart", 2);
    }

    void ChaseStart()
    {
        ChangeState(State.Closing);
        isChase = true;
    }


    void Update()
    {
        if (isChase && nav.enabled && ptarget != null)
        {
            //Vector3 targetPosition = ptarget.position;
            nav.SetDestination(ptarget.position);

            // ���Ͱ� �������� �������� ��
            if (nav.remainingDistance <= nav.stoppingDistance && !nav.pathPending)
            {
                ChangeState(State.Attacking);
            }
        }
    }
    void StopMoving()
    {
        isChase = false;
        anim.SetBool("IsMoving", false); // �̵� ���� �ִϸ��̼����� ����
    }



    protected override void ChangeState(State s)
    {
        if (myState == s) return;
        myState = s;
        switch (myState)
        {
            case State.Idle:
                anim.SetBool("b_IsMoving", false);
                break;
            case State.Closing:
                anim.SetBool("b_IsMoving", true);
                break;
            case State.Attacking:
                anim.SetTrigger("t_Attack");
                anim.SetBool("b_IsMoving", false);
                break;
        }
    }

    protected override void ProcessState()
    {
        switch (myState)
        {
            //���� ������ �ٰŸ����� ��ȸ
            case State.Idle:
                break;
            //������ ����
            case State.Closing:
                break;
            //����
            case State.Attacking:
                break;
        }
    }
}