using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public enum E_Skill
{
    QSkill = 0,
    WSkill = 1,
    ESkill = 2,
    RSkill = 3
}

public interface IDebuff
{
    public void Debuff(int type);
}

public class Player : BattleSystem, IGetDType, ICinematicStart, ICinematicEnd, IDebuff
{
    ParticleSystem particle;
    SkillManager sm;

    GameObject DebuffEffect;
    GameObject DebuffScreen;

    public Canvas DeBuffScr;

    public GameObject Effectobj;

    public LayerMask clickMask;

    public DefenceType Dtype;


    //0 : onehand 1: twohand
    public GameObject[] Weapon;

    public UnityEvent<Vector3, float, UnityAction<float>> clickAct;
    public UnityEvent<UnityAction<float>> stopAct;
    public UnityEvent<Vector3, float> dadgeAct;
    public UnityEvent<Vector3, float> rotAct;
    public UnityEvent<int> BuffAct;
    public UnityEvent<int, int> EnergyGageAct;
    public UnityEvent<int> SkillAct;
    public UnityEvent DurabilityAct;
    public float rotSpeed = 2;
    public float DadgeDelay = 0;
    public float dadgePw;
    float FireDelay = 0;
    float bufftime;

    bool isFireReady = true;
    bool isDadgeReady = true;
    bool isRun = true;
    bool Check;
    bool Death;
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
            Debuff(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debuff(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debuff(2);
        }
        
        if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            Debuff(3);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Debuff(4);
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Debuff(5);
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            float HillBuffTime = 10;
            BuffType(1001);
            StartCoroutine(Hill(HillBuffTime));
        }

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            float SpeedBuffTime = 10;
            BuffType(1002);
        }

        if(Input.GetKeyDown(KeyCode.Alpha9))
        {
            float AttackSpeedBuffTime = 10;
            BuffType(1003);
            myAnim.SetFloat("AttackSpeed", 2f);
            StartCoroutine(StopAttackSpeed(AttackSpeedBuffTime));

        }

    }


    IEnumerator StopAttackSpeed(float BuffTime)
    {
        yield return new WaitForSeconds(BuffTime);
        myAnim.SetFloat("AttackSpeed", 1.0f);
    }



    private void Corrosion()
    {
        /*
        float Corrosiontime = 10;
        BuffType(2000);
        DeBuffScr.transform.Find("BuSick").gameObject.SetActive(true);
        PlayBuffEffect("BuSick");
        StopScr("BuSick", Corrosiontime);
        */
        gameObject.GetComponent<Status>().Add(E_StatusAbnormality.Corrosion);
    }
    private void Poison()
    {
        float DotTime = 10;
        StartCoroutine(Dot(DotTime));
        DeBuffScr.transform.Find("Poison").gameObject.SetActive(true);
        BuffType(2001);
        PlayBuffEffect("Poison");
        StopScr("Poison", DotTime);
    }
    private void Slow()
    {
        float SlowDebuffTime = 10;
        BuffType(2002);
        PlayBuffEffect("Slow");
    }
    private void Stun()
    {
        float StunDebuffTime = 10;
        stopAct?.Invoke((float stop) => myAnim.SetFloat("Move", stop));
        ChangeState(state.Stun);
        Invoke("ChangeIdle", StunDebuffTime);
        BuffType(2003);
        PlayBuffEffect("Stun");
    }
    private void Bondage()
    {
        isRun = false;
        float BondageDebuffTime = 10;
        stopAct?.Invoke((float stop) => myAnim.SetFloat("Move", stop));
        DeBuffScr.transform.Find("Bondage").gameObject.SetActive(true);
        StartCoroutine(Bondage(BondageDebuffTime));
        BuffType(2004);
        PlayBuffEffect("Bondage");
        StopScr("Bondage", BondageDebuffTime);
    }
    private void Blind()
    {
        DeBuffScr.transform.Find("Blind").gameObject.SetActive(true);
        float BlindDebuffTime = 10;
        BuffType(2005);
        PlayBuffEffect("Blind");
        StopScr("Blind", BlindDebuffTime);
    }

    public void Debuff(int type)
    {
        switch (type)
        {
            case 0:
                Corrosion();
                break;
            case 1:
                Poison();
                break;
            case 2:
                Slow();
                break;
            case 3:
                Stun();
                break;
            case 4:
                Bondage();
                break;
            case 5:
                Blind();
                break;
        }
    }

    IEnumerator Hill(float HillBufftime)
    {
        bufftime = 0;
        while (bufftime < HillBufftime)
        {
            float tick = (float)(battleStat.HP * 0.003);
            HP += (int)tick;
            hpbarChangeAct?.Invoke(MaxHP, HP);
            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator Bondage(float Debufftime)
    {
        yield return new WaitForSeconds(Debufftime);
        isRun = true;
    }

    

    IEnumerator Dot(float Debufftime)
    {

        bufftime = 0;
        while (bufftime < Debufftime)
        {
            float tick = (float)(curBattleStat.HP * 0.03);
            base.TakeDamage((int)tick, AttackType.Normal, DefenceType.Normal);
            yield return new WaitForSeconds(1.0f);
        }
    }

    

    void ChangeIdle()
    {
        myAnim.SetBool("b_Stun", false);
        ChangeState(state.Idle);
    }

    void StopScr(string name, float Time)
    {
        StartCoroutine(C_Stopscr(name, Time));
    }

    IEnumerator C_Stopscr(string name, float Debufftime)
    {
        yield return new WaitForSeconds(Debufftime);
        DeBuffScr.transform.Find(name).gameObject.SetActive(false);
    }
    void BuffType(int Type)
    {
        BuffAct?.Invoke(Type);
    }

    void PlayBuffEffect(string DebuffName)
    {
        PlayerEffect Pe;
        DebuffEffect = Instantiate<GameObject>(Resources.Load($"Player/DeBuff/{DebuffName}") as GameObject);
        DebuffEffect.transform.SetParent(transform);
        Pe = Effectobj.GetComponent<PlayerEffect>();
        Pe.Effectpos(DebuffEffect);
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
        if (Input.GetMouseButtonDown(0) && isFireReady && GetRaycastHit() && !EventSystem.current.IsPointerOverGameObject())
        {

            stopAct?.Invoke((float stop) => myAnim.SetFloat("Move", stop));

            rotAct?.Invoke(dir, rotSpeed);

            ChangeState(state.Fire);
        }
    }

    bool AnimCheck(string Anim)
    {
        // ?�재 ?�니메이?�이 체크?�고???�는 ?�니메이?�인지 ?�인
        if (myAnim.GetCurrentAnimatorStateInfo(0).IsName(Anim) == true)
        {
            // ?�하???�니메이?�이?�면 ?�레??중인지 체크
            float animTime = myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime;
            if (animTime == 0)
            {
                return false;
                // ?�레??중이 ?�님
            }
            if (animTime > 0 && animTime < 1.0f)
            {
                return true;
                // ?�니메이???�레??�?
            }
            else if (animTime >= 1.0f)
            {
                // ?�니메이??종료
            }
        }
        return false;
    }



    public void MoveToMousePos()
    {
        if (Input.GetMouseButton(1) && GetRaycastHit() && isRun )
        {
            if (dir == null) return;
            ChangeState(state.Run);
            clickAct?.Invoke(dir, GetModifiedStat(E_BattleStat.Speed), (float temp) =>
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
                if (!string.IsNullOrWhiteSpace(plskill.InGameSkill[i]))
                {
                    Debug.Log(string.IsNullOrWhiteSpace(plskill.InGameSkill[i]));
                    GameObject effect = Resources.Load($"Player/SkillEffect/{ItemTypeIntToString.IntToStringSkillFileName(WeaponType)}/{plskill.InGameSkill[i]}") as GameObject;
                    sm = effect.GetComponent<SkillManager>();
                }
                else
                {
                    Debug.Log("���� ��ų���Կ� ��ų�� �����ϴ�.");
                    return;
                }

                if (curBattleStat.EnergyGage >= sm.EnergyGage && !sm.CoolTimeCheck)
                {
                    SkillAct?.Invoke(i);
                    ChangeState(state.Skill);
                    rotSpeed = 5.0f;
                    curBattleStat.EnergyGage -= sm.EnergyGage;
                    EnergyGageCal();
                    GetRaycastHit();
                    stopAct?.Invoke((float stop) => myAnim.SetFloat("Move", stop));
                    myAnim.SetTrigger(plskill.InGameSkill[i]);
                    rotAct?.Invoke(dir, rotSpeed);
                }
                else
                {
                    ChangeState(state.Idle);
                }
            }
        }
    }

    public void EnergyGageCal()
    {
        EnergyGageAct?.Invoke(battleStat.EnergyGage, curBattleStat.EnergyGage);
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
        if(!Death)
        {
            var emission = particle.emission;

            emission.rateOverTime = 0;
            deathAlarm?.Invoke(0, gameObject);
            stopAct?.Invoke(null);
            StartCoroutine(TimeControl());
            myAnim.SetTrigger("t_Death");

            Death = true;
        }
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

    public void ArmorDurability()
    {
        var plData = DataManager.instance.playerData;

        if(plData.Armor_Duration <= 0)
        {
            plData.Armor_Duration = 0;
        }


        switch (Random.Range(0, 2))
        {
            case 0:
                plData.Armor_Duration--;
                break;
            case 1:
                break;
        }
        if (plData.Armor_Duration == 40 || plData.Armor_Duration == 20 || plData.Armor_Duration == 0)
        {
            DurabilityAct?.Invoke();
        }
    }

    public void WeaponDurability()
    {
        var plData = DataManager.instance.playerData;
        if (plData.Weapon_Duration == 40 || plData.Weapon_Duration == 20 || plData.Weapon_Duration == 0)
        {
            DurabilityAct?.Invoke();
        }
    }

    public void CinematicStart()
    {
        stopAct?.Invoke((float stop) => myAnim.SetFloat("Move", stop));
        ChangeState(state.Cinematic);
        myAnim.SetTrigger("t_Taunt");

    }

    public void CinematicEnd()
    {
        ChangeState(state.Idle);
    }

    /*
    public void SlowDebuff(float DeBuffTime)
    {
        curBattleStat.Speed -= (float)(battleStat.Speed * 0.1);
        StartCoroutine(SlowDown(DeBuffTime));
    }
    */
    IEnumerator SlowDown(float DeBuffTime)
    {
        yield return new WaitForSeconds(DeBuffTime);
        curBattleStat.Speed += (float)(battleStat.Speed * 0.1);
    }
}