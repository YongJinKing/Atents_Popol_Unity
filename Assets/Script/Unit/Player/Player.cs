using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;

public enum E_Skill
{
    QSkill = 0,
    WSkill = 1,
    ESkill = 2,
    RSkill = 3
}

public class Player : BattleSystem, IGetDType, ICinematicStart, ICinematicEnd
{
    ParticleSystem particle;
    SkillManager sm;

    GameObject effect;

    public GameObject Effectobj;
    
    public LayerMask clickMask;

    public DefenceType Dtype;

    
    //0 : onehand 1: twohand
    public GameObject[] Weapon;

    public UnityEvent<Vector3, float, UnityAction<float>> clickAct;
    public UnityEvent<UnityAction<float>> stopAct;
    public UnityEvent<Vector3, float> dadgeAct;
    public UnityEvent<Vector3, float> rotAct;

    public float rotSpeed = 2;
    public float DadgeDelay = 0;
    public float dadgePw;
    float FireDelay = 0;
    float bufftime;

    bool isFireReady = true;
    bool isDadgeReady = true;
    bool Check;

    int WeaponType = 0;

    Vector3 dir;
    public enum state
    {
        Fire, Dadge, Idle, Run, Skill, Cinematic, Stun
    }

    [SerializeField] protected state playerstate;
    protected void ChangeState(state s)
    {
        if (playerstate == s) return;
        playerstate = s;
        var emission = particle.emission;

        switch (s)
        {
            case state.Fire:
                emission.rateOverTime = 0;
                break;
            case state.Dadge:
                emission.rateOverTime = 0;
                break;
            case state.Idle:
                emission.rateOverTime = 0;
                break;
            case state.Run:
                emission.rateOverTime = 30f;
                break;
            case state.Skill:
                emission.rateOverTime = 0;
                break;
            case state.Cinematic:
                emission.rateOverTime = 0;
                break;
            case state.Stun:
                myAnim.SetBool("b_Stun", true);
                emission.rateOverTime = 0;
                break;
        }
    }

    protected void ProcessState()
    {

        switch (playerstate)
        {
            case state.Skill:
                break;
            case state.Fire:
                Check = AnimCheck("t_Attack");
                if (!Check)
                {
                    myAnim.SetTrigger("t_Attack");
                }
                else
                {
                    ChangeState(state.Idle);
                }
                break;
            case state.Dadge:
                break;
            case state.Idle:
                MoveToMousePos();
                FireToMousePos();
                DadgeToPos();
                Skill();
                break;
            case state.Run:
                MoveToMousePos();
                FireToMousePos();
                DadgeToPos();
                Skill();
                break;
            case state.Cinematic:
                break;
            case state.Stun:
                break;
        }
    }



    Dictionary<E_Skill, KeyCode> controllKey = new Dictionary<E_Skill, KeyCode>();
    protected override void Start()
    {
        controllKey[E_Skill.QSkill] = KeyCode.Q;
        controllKey[E_Skill.WSkill] = KeyCode.W;
        controllKey[E_Skill.ESkill] = KeyCode.E;
        controllKey[E_Skill.RSkill] = KeyCode.R;

        base.Start();
        WeaponType = DataManager.instance.playerData.WeaponType;
        switch (WeaponType)
        {
            case 0:
                Weapon[0].SetActive(true);
                myAnim.SetBool("OneHandSword", true);
                break;
            case 1:
                Weapon[1].SetActive(true);
                myAnim.SetBool("TwoHandSword", true);
                break;
        }


        particle = GetComponentInChildren<ParticleSystem>();
        ChangeState(state.Idle);
    }

    void Update()
    {
        FireDelay -= Time.deltaTime;
        isFireReady = FireDelay < 0;
        DadgeDelay -= Time.deltaTime;
        isDadgeReady = DadgeDelay < 0;

        ProcessState();
        bufftime += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debuff();
        }

        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            stopAct?.Invoke((float stop) => myAnim.SetFloat("Move", stop));
            ChangeState(state.Stun);
            Invoke("ChangeIdle", 3.0f);
        }

        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            int Dtime = 10;
            StartCoroutine(Dot(Dtime));
        }
    }

    IEnumerator Dot(int Dtime)
    {

        bufftime = 0;
        while (bufftime < Dtime)
        {
            float tick = (float)(curBattleStat.HP * 0.003);
            base.TakeDamage((int)tick, AttackType.Normal, DefenceType.Normal);
            yield return new WaitForSeconds(0.2f);
        }
    }

    void ChangeIdle()
    {
        myAnim.SetBool("b_Stun", false);
        ChangeState(state.Idle);
    }

    public bool GetRaycastHit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 1000.0f, clickMask))
        {
            dir = hit.point - transform.position;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void FireToMousePos()
    {
        if (Input.GetMouseButtonDown(0) && isFireReady && GetRaycastHit())
        {

            stopAct?.Invoke((float stop) => myAnim.SetFloat("Move", stop));

            rotAct?.Invoke(dir, rotSpeed);

            ChangeState(state.Fire);
        }
    }

    bool AnimCheck(string Anim)
    {
        // 현재 애니메이션이 체크하고자 하는 애니메이션인지 확인
        if (myAnim.GetCurrentAnimatorStateInfo(0).IsName(Anim) == true)
        {
            // 원하는 애니메이션이라면 플레이 중인지 체크
            float animTime = myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime;
            if (animTime == 0)
            {
                return false;
                // 플레이 중이 아님
            }
            if (animTime > 0 && animTime < 1.0f)
            {
                return true;
                // 애니메이션 플레이 중
            }
            else if (animTime >= 1.0f)
            {
                // 애니메이션 종료
            }
        }
        return false;
    }



    public void MoveToMousePos()
    {
        if (Input.GetMouseButtonDown(1) && GetRaycastHit())
        {
            if (dir == null) return;
            ChangeState(state.Run);
            clickAct?.Invoke(dir, curBattleStat.Speed, (float temp) =>
            {
                myAnim.SetFloat("Move", temp);
                if (temp < 0.05f && playerstate != state.Dadge && playerstate != state.Fire && playerstate != state.Skill)
                {
                    ChangeState(state.Idle);
                }
            });
        }
    }

    public void DadgeToPos()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isDadgeReady)
        {
            GetRaycastHit();
            stopAct?.Invoke((float stop) => myAnim.SetFloat("Move", stop));
            ChangeState(state.Dadge);
            myAnim.SetTrigger("t_Dadge");
            dadgeAct?.Invoke(dir, dadgePw);
        }
    }




    public void Skill()
    {
        for(int i = 0; i < 4; i++)
        {
            if (Input.GetKeyDown(controllKey[(E_Skill)i]))
            {
                var plskill = DataManager.instance.playerData;
                if (plskill.Skill.Count > 0)
                {
                    if (!string.IsNullOrWhiteSpace(plskill.Skill[i]))
                    {
                        string Weaponpos = null;
                        switch (WeaponType)
                        {
                            case 0:
                                Weaponpos = "OneHandSwordSkill";
                                break;
                            case 1:
                                Weaponpos = "TwoHandSwordSkill";
                                break;
                        }
                        Debug.Log(plskill.Skill[i]);
                        GameObject effect = Resources.Load($"Player/SkillEffect/{Weaponpos}/{plskill.Skill[i]}") as GameObject;
                        sm = effect.GetComponent<SkillManager>();
                    }
                }
                else
                {
                    Debug.Log("현재슬롯에 스킬이 없습니다.");
                    //스킬 실패 사운드
                    return;
                }

                if (curBattleStat.EnergyGage >= sm.EnergyGage)
                {
                    ChangeState(state.Skill);
                    rotSpeed = 5.0f;
                    curBattleStat.EnergyGage -= sm.EnergyGage;
                    GetRaycastHit();
                    stopAct?.Invoke((float stop) => myAnim.SetFloat("Move", stop));
                    myAnim.SetTrigger(plskill.Skill[i]);
                    rotAct?.Invoke(dir, rotSpeed);
                }
                else
                {
                    ChangeState(state.Idle);
                }
            }
        }
    }

    public void OnEnd(int type)
    {
        switch (type)
        {
            case 0:
                FireDelay = battleStat.AttackDelay;
                Check = false;
                break;
            case 1:
                DadgeDelay = 1.0f;
                break;
            case 2:
                break;
        }
        ChangeState(state.Idle);
    }


    public DefenceType GetDType(Collider col)
    {
        return Dtype;
    }

    protected override void OnDead()
    {
        deathAlarm?.Invoke(0);
        stopAct?.Invoke(null);
        StartCoroutine(TimeControl());
        myAnim.SetTrigger("t_Death");
    }

    IEnumerator TimeControl()
    {
        float slowTime = 0.5f;
        while (!Mathf.Approximately(slowTime, 0.1f))
        {
            Time.timeScale = slowTime;
            slowTime -= Time.deltaTime * 1.5f;
            if (slowTime < 0.1f)
            {
                slowTime = 0.1f;
            }
            yield return null;
        }
        yield return new WaitForSecondsRealtime(1.0f);

        slowTime = 0.5f;
        while (slowTime < 1.0f)
        {
            slowTime += Time.deltaTime;
            if (slowTime > 1.0f)
            {
                slowTime = 1.0f;
            }
            Time.timeScale = slowTime;
            yield return null;
        }
    }

    public void CinematicStart()
    {
        ChangeState(state.Cinematic);
        myAnim.SetTrigger("t_Taunt");

    }

    public void CinematicEnd()
    {
        ChangeState(state.Idle);
    }


    public void Debuff()
    {
        curBattleStat.Speed -= (float)(battleStat.Speed * 0.1);
        StartCoroutine(SlowDown());
    }

    IEnumerator SlowDown()
    {
        yield return new WaitForSeconds(5.0f);
        curBattleStat.Speed += (float)(battleStat.Speed * 0.1);
    }
}