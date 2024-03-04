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
    //스킬이 타겟팅하는 레이어를 받아온다.
    protected LayerMask skillMask;
    #endregion

    //Public 변수영역
    #region public
    #endregion

    //이벤트 함수들 영역
    #region Event
    //Skill을 실행시킬 이벤트
    public UnityAction<Vector3> onSkillStartEvent;
    public UnityAction onSkillEndEvent;
    //Skill을 쓰기전 타겟을 detect 하기위한 이벤트 배열(여기에 스킬들 등록)
    public UnityEvent<UnityAction>[] onCommandDetectSkillTargetEvent;
    #endregion
    #endregion


    #region Method
    //private 함수들 영역
    #region PrivateMethod
    private void SkillRandomSet()
    {
        //스킬을 다썼으면
        for(int i = 0; i < onCommandDetectSkillTargetEvent.Length; i++)
        {
            saveSkill[i] = UnityEngine.Random.Range(0, onCommandDetectSkillTargetEvent.Length);
            

            //공통된 게 있으면 다시
            for(int j = 0; j < i; j++)
            {
                if (saveSkill[j] == saveSkill[i])
                {
                    saveSkill[i] = UnityEngine.Random.Range(0, onCommandDetectSkillTargetEvent.Length);
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
                onCommandDetectSkillTargetEvent[saveSkill[countUsedSkill]]?.Invoke(() => ChangeState(State.Attacking));
                Debug.Log(saveSkill[countUsedSkill]);
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
        Vector3 dir;


        float delta = 0.0f;
        while (myState == State.Closing)
        {
            //접근하는 반복자
            dir = target.transform.position - transform.position;
            dir = new Vector3(dir.x, 0, dir.z);
            dir.Normalize();

            delta += Time.deltaTime;
            transform.Translate(dir * delta * 0.1f, Space.World);

            yield return null;
        }
        yield return null;
    }


    //일단 특정 거리에서 기다리는 AI
    private IEnumerator IdleProcessing()
    {
        float IdleTime = 2.0f;

        Vector3 dir = -(target.transform.position - transform.position);
        dir = new Vector3(dir.x, 0, dir.z);
        dir.Normalize();

        float delta = 0.0f;
        while (IdleTime >= 0.0f)
        {
            IdleTime -= Time.deltaTime;

            //배회하는 코드들
            delta += Time.deltaTime;
            transform.Translate(dir * delta * 0.1f, Space.World);

            yield return null;
        }
        ChangeState(State.Closing);
        yield return null;
    }
    #endregion


    //이벤트가 일어났을때 실행되는 On~~함수
    #region EventHandler
    public void OnAddSkillEventListener(UnityAction<Vector3> skillStart, UnityAction skillEnd, LayerMask mask)
    {
        onSkillStartEvent = skillStart;
        onSkillEndEvent = skillEnd;
        skillMask = mask;
    }

    //공격모션이 스킬을 발동
    public void OnAttackStartAnim()
    {
        onSkillStartEvent?.Invoke(target.transform.position);
        countUsedSkill++;
        if (countUsedSkill >= onCommandDetectSkillTargetEvent.Length)
        {
            SkillRandomSet();
        }
    }

    //공격 모션이 끝남
    public void OnAttackEndAnim()
    {
        onSkillEndEvent?.Invoke();
        ChangeState(State.Idle);
    }
    #endregion


    //유니티 함수들 영역
    #region MonoBehaviour
    void Start()
    {
        saveSkill = new int[onCommandDetectSkillTargetEvent.Length];
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