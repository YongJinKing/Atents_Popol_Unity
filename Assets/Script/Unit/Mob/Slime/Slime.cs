using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Monster
{
    protected override void ChangeState(State s)
    {
        if (s == myState) return;
        myState = s;

        StopAllCoroutines();
        switch (myState)
        {
            //대충 적당히 근거리에서 배회
            case State.Idle:
                ProcessState();
                break;
            //적에게 접근
            case State.Closing:
                ProcessState();
                break;
            //공격
            case State.Attacking:
                ProcessState();
                break;
        }
    }
    
    protected override void ProcessState()
    {
        switch (myState)
        {
            //대충 적당히 근거리에서 배회
            case State.Idle:
                StartCoroutine(Idletarget());
                break;
            //적에게 접근
            case State.Closing:
                //detect를 실행하라고 지시
                skill[0].GetComponent<Skill>().OnDetectSkillRange();
                StartCoroutine(ClosingToTarget());
                break;
            //공격
            case State.Attacking:
                UseSkill1();
                ChangeState(State.Idle);
                break;
        }
    }


    //일단 완전 스파게티로 짜보자
    public Transform meleeAttackStartPos;
    public GameObject[] skill = new GameObject[2];
    public Transform tempTarget;

    private void UseSkill1()
    {
        myAnim.SetTrigger("AttackTrigger");
        skill[0].GetComponent<Skill>().OnSkillStart(tempTarget.transform.position);
    }
    private void UseSkill2()
    {
        myAnim.SetTrigger("AttackTrigger");
        skill[1].GetComponent<Skill>().OnSkillStart(tempTarget.transform.position);
    }


    //타겟에게 가까워짐
    private IEnumerator ClosingToTarget()
    {
        Vector3 dir;


        float delta = 0.0f;
        while (myState == State.Closing)
        {
            //접근하는 반복자
            dir = tempTarget.position - transform.position;
            dir = new Vector3(dir.x, 0, dir.z);
            dir.Normalize();

            delta += Time.deltaTime;
            transform.Translate(dir * delta * 0.1f);

            yield return null;
        }
        yield return null;
    }


    //일단 특정 거리에서 기다리는 AI
    private IEnumerator Idletarget()
    {
        float IdleTime = 2.0f;

        Vector3 dir = -(tempTarget.position - transform.position);
        dir = new Vector3(dir.x, 0, dir.z);
        dir.Normalize();

        float delta = 0.0f;
        while(IdleTime >= 0.0f)
        {
            IdleTime -= Time.deltaTime;

            //배회하는 코드들
            delta += Time.deltaTime;
            transform.Translate(dir * delta *0.1f);

            yield return null;
        }

        ChangeState(State.Closing);
        yield return null;
    }


    //여기가 문제다 Idle을 기다려줘야한다. 어떻게?
    //OnDetectSkillRange 함수가 실행되어야지 Detecting이 실행되므로 가능할지도?
    public void OnDetectTarget()
    {
        ChangeState(State.Attacking);
    }


    // Start is called before the first frame update
    void Start()
    {
        //ChangeState(State.Idle);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetButtonDown("Jump"))
        {
            UseSkill1();
        }
        if (Input.GetButtonDown("Fire1"))
        {
            UseSkill2();
        }
        
    }
}