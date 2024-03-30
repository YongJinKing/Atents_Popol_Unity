using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;

public interface I_ClickPoint
{
    public Vector3 GetRaycastHit();
}

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
        Fire, Dadge, Idle, Run, Skill , Death
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
            case state.Death:
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
            case state.Death:
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
        if (Input.GetMouseButtonDown(0) && isFireReady)
        {
            
            GetRaycastHit();
            ChangeState(state.Fire);
            
            myAnim.SetTrigger("t_Attack");
            
            stopAct?.Invoke((float stop) => myAnim.SetFloat("Move", stop));
            
            rotAct?.Invoke(dir, rotSpeed);
        }
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
            case 3:
                Invoke("SetActiveFalse", 1.0f);
                return;
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
        ChangeState(state.Death);
    }
    
    private void SetActiveFalse()
    {
        gameObject.SetActive(false);
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