using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Slime : Monster
{
    //���� ����
    #region Properties / Field
    //private ���� ����
    #region Private
    private int[] saveSkill;
    private int countUsedSkill;
    #endregion

    //protected ���� ����
    #region protected
    //��ų�� Ÿ�����ϴ� ���̾ �޾ƿ´�.
    protected LayerMask skillMask;
    #endregion

    //Public ��������
    #region public
    public float backStapOffset;
    #endregion

    //�̺�Ʈ �Լ��� ����
    #region Event
    //Skill�� �����ų �̺�Ʈ
    public UnityAction<Vector3> onSkillStartAct;
    public UnityAction onSkillEndAct;
    //Skill�� ������ Ÿ���� detect �ϱ����� �̺�Ʈ �迭(���⿡ ��ų�� ���)
    public UnityEvent<UnityAction>[] onCommandDetectSkillTargetEvent;
    public UnityEvent<Vector3, float, UnityAction, UnityAction> moveToPosEvent;
    public UnityEvent<Transform, float, UnityAction, UnityAction> followEvent;
    public UnityEvent<UnityAction> stopEvent;
    #endregion
    #endregion


    #region Method
    //private �Լ��� ����
    #region PrivateMethod
    private void SkillRandomSet()
    {
        //��ų�� �ٽ�����
        for(int i = 0; i < onCommandDetectSkillTargetEvent.Length; i++)
        {
            saveSkill[i] = UnityEngine.Random.Range(0, onCommandDetectSkillTargetEvent.Length);
            
            //����� �� ������ �ٽ�
            for(int j = 0; j < i; j++)
            {
                if (saveSkill[j] == saveSkill[i])
                {
                    saveSkill[i] = UnityEngine.Random.Range(0, 
                        onCommandDetectSkillTargetEvent.Length);
                    j = 0;
                }
            }
        }
        countUsedSkill = 0;

        Debug.Log(saveSkill[countUsedSkill]);
    }
    #endregion

    //protected �Լ��� ����
    #region ProtectedMethod
    protected override void ChangeState(State s)
    {
        if (s == myState) return;
        myState = s;

        StopAllCoroutines();
        stopEvent?.Invoke(null);

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
                StartCoroutine(IdleProcessing());
                break;
            //������ ����
            case State.Closing:
                //detect�� �����϶�� ����
                onCommandDetectSkillTargetEvent[saveSkill[countUsedSkill]]?.Invoke
                    (() => ChangeState(State.Attacking));
                Debug.Log(saveSkill[countUsedSkill]);
                StartCoroutine(ClosingToTarget());
                break;
            //����
            case State.Attacking:
                myAnim.SetTrigger("t_Attack");
                break;
        }
    }
    #endregion

    //public �Լ��� ����
    #region PublicMethod
    #endregion
    #endregion


    //�ڷ�ƾ ����
    #region Coroutine
    private IEnumerator ClosingToTarget()
    {
        Vector3 dir;

        followEvent?.Invoke(target.transform, battleStat.Speed, null, null);


        yield return null;
    }


    //�ϴ� Ư�� �Ÿ����� ��ٸ��� AI
    private IEnumerator IdleProcessing()
    {
        float IdleTime = 2.0f;

        //�̵��� ��ǥ ���ϱ�
        Vector3 dir = -(target.transform.position - transform.position);
        dir = new Vector3(dir.x, 0, dir.z);
        dir.Normalize();

        //�� �������� ������ ��ŭ �̵��� �ڿ� ������ �������� ��ǥ�� �� backStepPos �� ����
        Vector3 backStepPos = (transform.position + dir * backStapOffset);
        float radius = 2.0f;
        dir = new Vector3 (UnityEngine.Random.Range(0,radius), 0, UnityEngine.Random.Range(0, radius));

        backStepPos = backStepPos + dir;

        //�̵� �̺�Ʈ
        moveToPosEvent?.Invoke(backStepPos, battleStat.Speed, null, null);

        //idle �ð� ���
        while (IdleTime >= 0.0f)
        {
            IdleTime -= Time.deltaTime;
            yield return null;
        }
        //�ð� ������ �̵� ����
        stopEvent?.Invoke(null);

        //���� �ٲ�
        ChangeState(State.Closing);
        yield return null;
    }
    #endregion


    //�̺�Ʈ�� �Ͼ���� ����Ǵ� On~~�Լ�
    #region EventHandler
    public void OnAddSkillEventListener(
        UnityAction<Vector3> skillStart, 
        UnityAction skillEnd, 
        LayerMask mask)
    {
        onSkillStartAct = skillStart;
        onSkillEndAct = skillEnd;
        skillMask = mask;
    }

    //���ݸ���� ��ų�� �ߵ�
    public void OnAttackStartAnim()
    {
        onSkillStartAct?.Invoke(target.transform.position);
        countUsedSkill++;
        if (countUsedSkill >= onCommandDetectSkillTargetEvent.Length)
        {
            SkillRandomSet();
        }
    }

    //���� ����� ����
    public void OnAttackEndAnim()
    {
        onSkillEndAct?.Invoke();
        ChangeState(State.Idle);
    }
    #endregion


    //����Ƽ �Լ��� ����
    #region MonoBehaviour
    protected override void Start()
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