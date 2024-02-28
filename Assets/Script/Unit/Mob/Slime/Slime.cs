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
            //���� ������ �ٰŸ����� ��ȸ
            case State.Wandering:
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
    
    public void ProcessState()
    {
        switch (myState)
        {
            //���� ������ �ٰŸ����� ��ȸ
            case State.Wandering:
                StartCoroutine(Wanderingtarget());
                break;
            //������ ����
            case State.Closing:
                StartCoroutine(ClosingToTarget());
                break;
            //����
            case State.Attacking:
                UseSkill1();
                ChangeState(State.Wandering);
                break;
        }
    }


    //�ϴ� ���� ���İ�Ƽ�� ¥����
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


    //Ÿ�ٿ��� �������
    private IEnumerator ClosingToTarget()
    {
        //detect�� �����϶�� ����
        skill[0].GetComponent<Skill>().OnDetectSkillRange();
        while (myState == State.Closing)
        {
            //�����ϴ� �ݺ���

        }
        yield return null;
    }


    //�ϴ� Ư�� �Ÿ����� ��ٸ��� AI
    private IEnumerator Wanderingtarget()
    {
        float wanderingTime = 2.0f;

        while(wanderingTime >= 0.0f)
        {
            wanderingTime -= Time.deltaTime;

            //��ȸ�ϴ� �ڵ��


            yield return null;
        }

        ChangeState(State.Closing);
        yield return null;
    }


    //���Ⱑ ������ wandering�� ��ٷ�����Ѵ�. ���?
    //OnDetectSkillRange �Լ��� ����Ǿ���� Detecting�� ����ǹǷ� ����������?
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