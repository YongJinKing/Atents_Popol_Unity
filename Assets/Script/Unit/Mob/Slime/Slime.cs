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
            //적에게 접근
            case State.Closing:
                ProcessState();
                break;
            //공격
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
            //대충 적당히 근거리에서 배회
            case State.Idle:
                skills[saveSkill[countUsedSkill]].OnRequestSkillInfo();
                StartCoroutine(IdleProcessing());
                break;
            //적에게 접근
            case State.Closing:
                //detect를 실행하라고 지시
                skills[saveSkill[countUsedSkill]].OnCommandDetectSkillTarget(
                    () => 
                    {
                        ChangeState(State.Attacking); 
                    });
                //Debug.Log(saveSkill[countUsedSkill]);
                StartCoroutine(ClosingToTarget());
                break;
            //공격
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
        //먼저 타겟을 찾는다.
        yield return StartCoroutine(FindTarget());

        float IdleTime = 2.0f;

        //이동할 좌표 구하기
        Vector3 dir = -(target.transform.position - transform.position);
        dir = new Vector3(dir.x, 0, dir.z);
        dir.Normalize();

        //그 방향으로 오프셋 만큼 이동한 뒤에 랜덤한 방향으로 좌표를 찍어서 backStepPos 를 생성
        Vector3 backStepPos = (transform.position + dir * backStapOffset);
        Vector3 backStepDir = Quaternion.Euler(0, UnityEngine.Random.Range(0, 360f), 0) * dir;
        backStepDir.Normalize();

        float dist = UnityEngine.Random.Range(2f, 5f);

        while(Vector3.Dot(dir, backStepDir) < 0)
        {
            backStepDir = Quaternion.Euler(0, UnityEngine.Random.Range(0, 360f), 0) * dir;
        }

        backStepPos = backStepPos + backStepDir * dist;

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


    //이벤트가 일어났을때 실행되는 On~~함수
    #region EventHandler

    //공격모션이 스킬을 발동
    public void OnAttackStartAnim()
    {
        onSkillHitCheckStartAct?.Invoke();
    }

    //공격 모션중에 히트박스 가 끝남
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

    //유니티 함수들 영역
    #region MonoBehaviour
    protected override void Start()
    {
        base.Start();

        //스킬 랜덤 등록
        saveSkill = new int[skills.Length];
        countUsedSkill = 0;
        SkillRandomSet();

        ProcessState();
    }
    #endregion
}