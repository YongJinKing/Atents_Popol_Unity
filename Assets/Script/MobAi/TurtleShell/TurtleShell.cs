using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TurtleShell : MonoBehaviour
{
    public Transform target;
    public bool isChase;
    NavMeshAgent nav;
    Animator anim;

    public enum State
    { Idle, Walk, Attack }

    public State myState;

    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();

        myState = State.Idle;

        Invoke("ChaseStart", 2);
    }

    void ChaseStart()
    {
        ChangeState(State.Walk);
        isChase = true;
    }

    void Update()
    {
        if (isChase && nav.enabled)
        {
            nav.SetDestination(target.position);

            // 몬스터가 목적지에 도착했을 때
            if (nav.remainingDistance <= nav.stoppingDistance && !nav.pathPending)
            {
                StopMoving();
                ChangeState(State.Attack);
            }
        }
    }
    void StopMoving()
    {
        isChase = false;
        anim.SetBool("IsMoving", false); // 이동 중지 애니메이션으로 변경
    }

    void ChangeState(State s)
    {
        if (myState == s) return;
        myState = s;
        switch (myState)
        {
            case State.Idle:
                anim.SetBool("IsMoving", false);
                break;
            case State.Walk:
                anim.SetBool("IsMoving", true);
                break;
            case State.Attack:
                anim.SetTrigger("Attack");
                anim.SetBool("IsMoving", false);
                break;
        }
    }
}