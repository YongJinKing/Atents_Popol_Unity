using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Monster
{
    public enum State
    {
        Create,
        Wandering,
        Closing,
        Attacking
    }

    
    [SerializeField]private State myState;

    public void ChangeState(State s)
    {
        if (s == myState) return;
        myState = s;

        switch (myState)
        {
            //대충 적당히 근거리에서 배회
            case State.Wandering:
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
    
    public void ProcessState()
    {
        switch (myState)
        {
            //대충 적당히 근거리에서 배회
            case State.Wandering:
                StartCoroutine(Wanderingtarget());
                break;
            //적에게 접근
            case State.Closing:
                StartCoroutine(ClosingToTarget());
                break;
            //공격
            case State.Attacking:
                UseSkill1();
                ChangeState(State.Wandering);
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
        //detect를 실행하라고 지시
        skill[0].GetComponent<Skill>().OnDetectSkillRange();
        while (myState == State.Closing)
        {
            //접근하는 반복자

        }
        yield return null;
    }


    //일단 특정 거리에서 기다리는 AI
    private IEnumerator Wanderingtarget()
    {
        float wanderingTime = 2.0f;

        while(wanderingTime >= 0.0f)
        {
            wanderingTime -= Time.deltaTime;

            //배회하는 코드들


            yield return null;
        }

        ChangeState(State.Closing);
        yield return null;
    }


    //여기가 문제다 wandering을 기다려줘야한다. 어떻게?
    //OnDetectSkillRange 함수가 실행되어야지 Detecting이 실행되므로 가능할지도?
    public void OnDetectTarget()
    {
        ChangeState(State.Attacking);
    }


    // Start is called before the first frame update
    void Start()
    {
        ChangeState(State.Wandering);
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
}