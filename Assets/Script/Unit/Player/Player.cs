using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public interface I_ClickPoint
{
    public Vector3 GetRaycastHit();
}

public interface I_Skill
{
    void Do(string Anim);
}

public class QSkill : CharacterProperty, I_Skill
{
    public void Do(string Anim)
    {
        myAnim.SetTrigger(Anim);
    }
}
/*public class QSkill : CharacterProperty, I_Skill
{
    public void Do(string Anim)
    {
        myAnim.SetTrigger(Anim);
    }
}
public class QSkill : CharacterProperty, I_Skill
{
    public void Do(string Anim)
    {
        myAnim.SetTrigger(Anim);
    }
}
public class QSkill : CharacterProperty, I_Skill
{
    public void Do(string Anim)
    {
        myAnim.SetTrigger(Anim);
    }
}*/

public class Player : BattleSystem, I_ClickPoint, IGetDType
{
    ParticleSystem particle;
    public GameObject Effectobj;
    public LayerMask clickMask;
    public DefenceType Dtype;
    public UnityEvent<Vector3, float, UnityAction<float>> clickAct;
    public UnityEvent<UnityAction<float>> stopAct;
    public UnityEvent<Vector3, float> dadgeAct;
    public UnityEvent<Vector3, float> rotAct;
    public float rotSpeed = 2;
    public float DadgeDelay = 0;
    public float dadgePw;
    float FireDelay = 0;
    bool isFireReady = true;
    bool isDadgeReady = true;
    Vector3 dir;
    public enum state
    {
        Fire, Dadge, Idle, Run, Skill
    }

    [SerializeField]protected state playerstate;
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
        }
    }

    protected void ProcessState()
    {

        switch (playerstate)
        {
            case state.Skill:
                break;
            case state.Fire:
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
        }
    }


    protected override void Start()
    {
        base.Start();
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
    }

    public Vector3 GetRaycastHit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 1000.0f, clickMask))
        {
            dir = hit.point - transform.position;
        }

        return dir;
    }

    public void FireToMousePos()
    {
        bool Check = AnimCheck("t_Attack");

        if (Input.GetMouseButtonDown(0) && isFireReady && !Check)
        {
            GetRaycastHit();

            stopAct?.Invoke((float stop) => myAnim.SetFloat("Move", stop));
            
            myAnim.SetTrigger("t_Attack");

            ChangeState(state.Fire);

            rotAct?.Invoke(dir, rotSpeed);
        }
    }

    bool AnimCheck(string Anim)
    {
        // 현재 애니메이션이 체크하고자 하는 애니메이션인지 확인
        if(myAnim.GetCurrentAnimatorStateInfo(0).IsName(Anim) == true)
        {
            // 원하는 애니메이션이라면 플레이 중인지 체크
            float animTime = myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime;
            if(animTime == 0)
            {
                return false;
                // 플레이 중이 아님
            }
            if(animTime > 0 && animTime < 1.0f)
            {
                return true;
                // 애니메이션 플레이 중
            }
            else if(animTime >= 1.0f)
            {
                // 애니메이션 종료
            }
        }
        return false;
    }



    public void MoveToMousePos()
    {
        if (Input.GetMouseButtonDown(1))
        {
            GetRaycastHit();
            ChangeState(state.Run);
            clickAct?.Invoke(dir, battleStat.Speed, (float temp) => 
            {
                myAnim.SetFloat("Move", temp);
                if(temp < 0.05f && playerstate != state.Dadge && playerstate != state.Fire && playerstate != state.Skill)
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
        if(Input.GetKeyDown(KeyCode.Q) && curBattleStat.EnergyGage >= 20)
        {
            curBattleStat.EnergyGage -= 20;
            GetRaycastHit();
            stopAct?.Invoke((float stop) => myAnim.SetFloat("Move", stop));
            ChangeState(state.Skill);
            myAnim.SetTrigger("t_QSkill");
            
            rotAct?.Invoke(dir, rotSpeed);
        }
        if(Input.GetKeyDown(KeyCode.W) && curBattleStat.EnergyGage >= 40)
        {
            curBattleStat.EnergyGage -= 40;
            rotSpeed = 3.0f;
            GetRaycastHit();
            stopAct?.Invoke((float stop) => myAnim.SetFloat("Move", stop));
            ChangeState(state.Skill);
            myAnim.SetTrigger("t_WSkill");
            
            rotAct?.Invoke(dir, rotSpeed);
        }
        if (Input.GetKeyDown(KeyCode.E) && curBattleStat.EnergyGage >= 60)
        {
            curBattleStat.EnergyGage -= 60;
            rotSpeed = 3.0f;
            GetRaycastHit();
            stopAct?.Invoke((float stop) => myAnim.SetFloat("Move", stop));
            ChangeState(state.Skill);
            myAnim.SetTrigger("t_ESkill");

            rotAct?.Invoke(dir, rotSpeed);
        }

        // if (Input.GetKeyDown(KeyCode.R) && curBattleStat.EnergyGage >= 60)
        // {
        //     curBattleStat.EnergyGage -= 60;
        //     GetRaycastHit();
        //     stopAct?.Invoke((float stop) => myAnim.SetFloat("Move", stop));
        //     ChangeState(state.Skill);
        //     myAnim.SetTrigger("t_RSkill");
        // }
    }

    public void OnEnd(int type)
    {
        switch (type)
        {
            case 0:
                FireDelay = battleStat.AttackDelay;
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
        while(!Mathf.Approximately(slowTime, 0.1f))
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
            if(slowTime > 1.0f)
            {
                slowTime = 1.0f;
            }
            Time.timeScale = slowTime;
            yield return null;
        }
    }

    /*protected override void LevelUp()
    {
        var plstat = playerdata.playerstatdata;
        var plLvstat = playerdata.dicPlayerLevelData[++plstat.Character_CurrentLevel];

        plstat.Character_AttackPower += plLvstat.AttackPower;
        plstat.Character_Hp += plLvstat.Hp;
    }*/
}