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
    public UnityEvent<Vector3, float, UnityAction<float>> clickAct;
    public UnityEvent<UnityAction<float>> stopAct;
    public UnityEvent<Vector3, float> dadgeAct;
    public UnityEvent<Vector3, float> rotAct;
    public GameObject jointItemR;
    public LayerMask clickMask;
    public float rotSpeed = 2;
    float FireDelay = 0;
    public float DadgeDelay = 0;
    public float dadgePw;
    bool isFireReady = true;
    bool isDadgeReady = true;
    public GameObject Effectobj;

    public DefenceType Dtype;
    
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

        switch (s)
        {
            case state.Fire:
                break;
            case state.Dadge:
                break;
            case state.Idle:
                break;
            case state.Run:
                break;
            case state.Skill:
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
        if(Input.GetKeyDown(KeyCode.Q))
        {
            GetRaycastHit();
            stopAct?.Invoke((float stop) => myAnim.SetFloat("Move", stop));
            ChangeState(state.Skill);
            myAnim.SetTrigger("t_Skill");
            
            rotAct?.Invoke(dir, rotSpeed);
        }
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
}