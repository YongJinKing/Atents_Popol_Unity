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
                StartCoroutine(Idletarget());
                break;
            //������ ����
            case State.Closing:
                //detect�� �����϶�� ����
                skill[0].GetComponent<Skill>().OnDetectSkillRange();
                StartCoroutine(ClosingToTarget());
                break;
            //����
            case State.Attacking:
                UseSkill1();
                ChangeState(State.Idle);
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
        Vector3 dir;


        float delta = 0.0f;
        while (myState == State.Closing)
        {
            //�����ϴ� �ݺ���
            dir = tempTarget.position - transform.position;
            dir = new Vector3(dir.x, 0, dir.z);
            dir.Normalize();

            delta += Time.deltaTime;
            transform.Translate(dir * delta * 0.1f);

            yield return null;
        }
        yield return null;
    }


    //�ϴ� Ư�� �Ÿ����� ��ٸ��� AI
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

            //��ȸ�ϴ� �ڵ��
            delta += Time.deltaTime;
            transform.Translate(dir * delta *0.1f);

            yield return null;
        }

        ChangeState(State.Closing);
        yield return null;
    }


    //���Ⱑ ������ Idle�� ��ٷ�����Ѵ�. ���?
    //OnDetectSkillRange �Լ��� ����Ǿ���� Detecting�� ����ǹǷ� ����������?
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