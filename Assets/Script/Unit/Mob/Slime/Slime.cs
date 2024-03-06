using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Slime : Monster
{
    //변수 영역
    #region Properties / Field
    //private 변수 영역
    #region Private
    private int[] saveSkill;
    private int countUsedSkill;
    #endregion

    //protected 변수 영역
    #region protected
    #endregion

    //Public 변수영역
    #region public
    public float backStapOffset;
    #endregion

    //이벤트 함수들 영역
    #region Event
    #endregion
    #endregion


    #region Method
    //private 함수들 영역
    #region PrivateMethod
    private void SkillRandomSet()
    {
        //스킬을 다썼으면
        for(int i = 0; i < skills.Length; i++)
        {
            saveSkill[i] = UnityEngine.Random.Range(0, skills.Length);
            
            //공통된 게 있으면 다시
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

        Debug.Log(saveSkill[countUsedSkill]);
    }
    #endregion

    //protected 함수들 영역
    #region ProtectedMethod
    protected override void ChangeState(State s)
    {
        if (s == myState) return;
        myState = s;

        StopAllCoroutines();
        stopEvent?.Invoke(null);

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
                StartCoroutine(IdleProcessing());
                break;
            //적에게 접근
            case State.Closing:
                //detect를 실행하라고 지시
                //skills[saveSkill[countUsedSkill]].OnCommandDetectSkillTarget(() => ChangeState(State.Attacking));
                skills[1].OnCommandDetectSkillTarget(() => ChangeState(State.Attacking));
                //Debug.Log(saveSkill[countUsedSkill]);
                StartCoroutine(ClosingToTarget());
                break;
            //공격
            case State.Attacking:
                myAnim.SetTrigger("t_Attack");
                break;
        }
    }
    #endregion

    //public 함수들 영역
    #region PublicMethod
    #endregion
    #endregion


    //코루틴 영역
    #region Coroutine
    private IEnumerator ClosingToTarget()
    {
        followEvent?.Invoke(target.transform, battleStat.Speed, null, null);


        yield return null;
    }


    //일단 특정 거리에서 기다리는 AI
    private IEnumerator IdleProcessing()
    {
        float IdleTime = 2.0f;

        //이동할 좌표 구하기
        Vector3 dir = -(target.transform.position - transform.position);
        dir = new Vector3(dir.x, 0, dir.z);
        dir.Normalize();

        //그 방향으로 오프셋 만큼 이동한 뒤에 랜덤한 방향으로 좌표를 찍어서 backStepPos 를 생성
        Vector3 backStepPos = (transform.position + dir * backStapOffset);
        float radius = 2.0f;
        dir = new Vector3 (UnityEngine.Random.Range(0,radius), 0, UnityEngine.Random.Range(0, radius));

        backStepPos = backStepPos + dir;

        //이동 이벤트
        onMovementEvent?.Invoke(backStepPos, battleStat.Speed, null, null);

        //idle 시간 재기
        while (IdleTime >= 0.0f)
        {
            IdleTime -= Time.deltaTime;
            yield return null;
        }
        //시간 끝나면 이동 끝냄
        stopEvent?.Invoke(null);

        //상태 바꿈
        ChangeState(State.Closing);
        yield return null;
    }
    #endregion


    //이벤트가 일어났을때 실행되는 On~~함수
    #region EventHandler

    //공격모션이 스킬을 발동
    public void OnAttackStartAnim()
    {
        onSkillStartAct?.Invoke(target.transform.position);
        countUsedSkill++;
        if (countUsedSkill >= skills.Length)
        {
            SkillRandomSet();
        }
    }

    //공격 모션이 끝남
    public void OnAttackEndAnim()
    {
        onSkillEndAct?.Invoke();
        ChangeState(State.Idle);
    }
    #endregion


    //유니티 함수들 영역
    #region MonoBehaviour
    protected override void Start()
    {
        saveSkill = new int[skills.Length];
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