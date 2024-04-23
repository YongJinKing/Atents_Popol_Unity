using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Monster
{
    //���� ����
    #region Properties / Field
    //private ���� ����
    #region Private
    private int[] saveSkill;
    private int countUsedSkill;

    private int[] saveMoveType;
    private int countIdle = 0;

    private Dictionary<string, int> dicIntAnims = new Dictionary<string, int>();
    private Dictionary<string, int> dicBoolAnims = new Dictionary<string, int>();
    private Dictionary<string, int> dicTriggerAnims = new Dictionary<string, int>();
    #endregion

    //protected ���� ����
    #region protected
    #endregion

    //Public ��������
    #region public
    public float backStapOffset = 5.0f;
    #endregion

    //�̺�Ʈ �Լ��� ����
    #region Event
    #endregion
    #endregion


    #region Method
    //private �Լ��� ����
    #region PrivateMethod
    private void SkillRandomSet()
    {
        //��ų�� �ٽ�����
        for(int i = 0; i < skills.Length; i++)
        {
            saveSkill[i] = UnityEngine.Random.Range(0, skills.Length);
            
            //�����?�� ������ �ٽ�
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

    private void IdleProcessRandomSet()
    {
        for (int i = 0; i < saveMoveType.Length; ++i)
        {
            saveMoveType[i] = UnityEngine.Random.Range(0, saveMoveType.Length);

            //�����?�� ������ �ٽ�
            for (int j = 0; j < i; ++j)
            {
                if (saveMoveType[j] == saveMoveType[i])
                {
                    saveMoveType[i] = UnityEngine.Random.Range(0,
                        saveMoveType.Length);
                    j = 0;
                }
            }
        }
        countIdle = 0;
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
                if (countIdle > saveMoveType.Length - 1)
                {
                    IdleProcessRandomSet();
                }
                ProcessState();
                break;
            //������ ����
            case State.Closing:
                ProcessState();
                break;
            //����
            case State.Attacking:
                Vector3 dir = target.transform.position - transform.position;
                dir = new Vector3(dir.x, 0, dir.z);
                dir.Normalize();

                rotateEvent?.Invoke(dir, 1.0f);
                ProcessState();
                break;
            case State.Stun:
                onDeadAct?.Invoke();
                myAnim.SetBool("b_Stun", true);
                break;
            case State.Death:
                onDeadAct?.Invoke();
                Debug.Log("Monster Death");
                myAnim.SetTrigger("t_Death");
                Collider temp = transform.GetComponent<Collider>();
                temp.attachedRigidbody.isKinematic = true;
                temp.attachedRigidbody.useGravity = false;
                temp.enabled = false;
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
                //detect�� �����϶��?����
                skills[saveSkill[countUsedSkill]].OnCommandDetectSkillTarget(
                    () => 
                    {
                        ChangeState(State.Attacking); 
                    });
                //Debug.Log(saveSkill[countUsedSkill]);
                StartCoroutine(ClosingToTarget());
                break;
            //����
            case State.Attacking:
                myAnim.SetBool("b_isSkillProgress", true);
                myAnim.SetTrigger("t_SkillStart");
                //myAnim.SetTrigger("t_ChannelingSkill");
                //Skill Start hitCheck after delayMotion
                onSkillStartAct?.Invoke(target.transform,
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
            case State.Stun:
                break;
            case State.Death:
                StartCoroutine(DeadActing());
                break;
        }
    }

    protected override void Initialize()
    {
        base.Initialize();

        saveSkill = new int[skills.Length];
        SkillRandomSet();


        //idleAI = new List<int>();
        //idleAI.Add(0);
        
        Debug.Log($"idleAI.Count : {idleAI.Count}");
        foreach(int data in idleAI)
        {
            Debug.Log($"inside the idleAI : {data}");
        }
        

        saveMoveType = new int[idleAI.Count];
        IdleProcessRandomSet();

        ChangeState(State.Idle);
    }
    #endregion

    //public �Լ��� ����
    #region PublicMethod
    public override void CinematicStart()
    {
        myAnim.SetTrigger("t_CinematicStart");
    }
    public override void CinematicEnd()
    {
        myAnim.SetTrigger("t_CinematicEnd");
        Initialize();
    }
    #endregion
    #endregion


    //�ڷ�ƾ ����
    #region Coroutine
    private IEnumerator ClosingToTarget()
    {
        followEvent?.Invoke(target.transform, GetModifiedStat(E_BattleStat.Speed),
            () => myAnim.SetBool("b_Moving",true),
            () => myAnim.SetBool("b_Moving", false));

        yield return null;
    }


    //�ϴ� Ư�� �Ÿ����� ��ٸ���?AI
    private IEnumerator IdleProcessing()
    {
        skills[saveSkill[countUsedSkill]].OnRequestSkillInfo();

        yield return new WaitForEndOfFrame();
        myAnim.SetInteger("i_SkillType", animType);

        //���� Ÿ���� ã�´�.
        yield return StartCoroutine(FindTarget());

        float IdleTime = 3.0f;

        //int type = 0;     //UnityEngine.Random.Range(0,2);

        switch (idleAI[saveMoveType[countIdle]])
        {
            case 0:
                {
                    //�̵��� ��ǥ ���ϱ�
                    Vector3 dir = -(target.transform.position - transform.position);
                    dir = new Vector3(dir.x, 0, dir.z);
                    dir.Normalize();

                    //�� �������� ������ ��ŭ �̵��� �ڿ� ������ �������� ��ǥ�� ���?backStepPos �� ����
                    Vector3 backStepPos = (transform.position + dir * backStapOffset);
                    Vector3 backStepDir = Quaternion.Euler(0, UnityEngine.Random.Range(0, 360f), 0) * dir;
                    backStepDir.Normalize();

                    float dist = UnityEngine.Random.Range(2f, 5f);

                    while (Vector3.Dot(dir, backStepDir) < 0)
                    {
                        backStepDir = Quaternion.Euler(0, UnityEngine.Random.Range(0, 360f), 0) * dir;
                    }

                    backStepPos = backStepPos + backStepDir * dist;

                    //�̵� �̺�Ʈ
                    onMovementEvent?.Invoke(backStepPos, GetModifiedStat(E_BattleStat.Speed), 
                        () => myAnim.SetBool("b_Moving", true),
                        () => myAnim.SetBool("b_Moving", false));
                }
                break;
            case 1:
                {
                    sideMoveEvent?.Invoke(target.transform, new Info<float, float>(GetModifiedStat(E_BattleStat.Speed), 10.0f), null, null);
                }
                break;
            case 2:
                {
                    followEvent?.Invoke(target.transform, GetModifiedStat(E_BattleStat.Speed),
                        () => myAnim.SetBool("b_Moving", true),
                        () => myAnim.SetBool("b_Moving", false));
                }
                break;
            default:
                break;
        }


        //idle �ð� ���?
        while (IdleTime >= 0.0f)
        {
            IdleTime -= Time.deltaTime;
            yield return null;
        }


        //�ð� ������ �̵� ����
        stopEvent?.Invoke(null);

        //���� �ٲ�
        ++countIdle;
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

    protected IEnumerator DeadActing()
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(gameObject);
    }
    #endregion


    //�̺�Ʈ�� �Ͼ���� ����Ǵ�?On~~�Լ�
    #region EventHandler
    #endregion

    //����Ƽ �Լ��� ����
    #region MonoBehaviour
    protected override void Start()
    {
        //base.Start();

        dicIntAnims.Add("SkillType", Animator.StringToHash("i_SkillType"));
        dicBoolAnims.Add("isSkillProgress", Animator.StringToHash("b_isSkillProgress"));
        dicBoolAnims.Add("isMoving", Animator.StringToHash("b_Moving"));
        dicBoolAnims.Add("isStun", Animator.StringToHash("b_Stun"));
        dicTriggerAnims.Add("SkillStart", Animator.StringToHash("t_SkillStart"));
        dicTriggerAnims.Add("AttackStart", Animator.StringToHash("t_AttackStart"));
        dicTriggerAnims.Add("AttackEnd", Animator.StringToHash("t_AttackEnd"));
        dicTriggerAnims.Add("Death", Animator.StringToHash("t_Death"));
        dicTriggerAnims.Add("CinematicStart", Animator.StringToHash("t_CinematicStart"));
        dicTriggerAnims.Add("CinematicEnd", Animator.StringToHash("t_CinematicEnd"));
    }
    #endregion
}