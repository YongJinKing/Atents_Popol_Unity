using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Player : BattleSystem
{
    public UnityEvent<Vector3, float, UnityAction<float>> clickAct;
    public UnityEvent<Vector3, Weapon> attackAct;
    public UnityEvent<UnityAction<float>> stopAct;
    public UnityEvent<Vector3, float> dadgeAct;
    public UnityAction<Vector3> getHitAct;
    public GameObject jointItemR;
    public LayerMask clickMask;
    public LayerMask attackMask;
    Weapon equipWeapon;
    float FireDelay = 0;
    public float DadgeDelay = 0;
    bool isFireReady = true;

    RaycastHit hit = default(RaycastHit);
    Vector3 dadgeVec;

    bool isDadgeReady = true;
    public float dadgePw;
    public enum state
    {
        Fire, Dadge, Idle, Run
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
        }
    }

    protected void ProcessState()
    {

        switch (playerstate)
        {
            case state.Fire:
                break;
            case state.Dadge:
                break;
            case state.Idle:
                MoveToMousePos();
                FireToMousePos();
                DadgeToPos();
                break;
            case state.Run:
                MoveToMousePos();
                FireToMousePos();
                DadgeToPos();
                break;
        }
    }



    protected override void Start()
    {
        base.Start();
        ChangeState(state.Idle);
        equipWeapon = jointItemR.transform.GetChild(0).GetComponent<Weapon>();
    }

    public void FireToMousePos()
    {
        if (Input.GetMouseButtonDown(0) && isFireReady)
        {
            hit = GetRaycastHit();

            ChangeState(state.Fire);
            attackAct?.Invoke(hit.point, equipWeapon);
            stopAct?.Invoke((float stop) => myAnim.SetFloat("Move", stop));
        }
    }

    public void MoveToMousePos()
    {
        if (Input.GetMouseButtonDown(1))
        {
            hit = GetRaycastHit();
            ChangeState(state.Run);
            clickAct?.Invoke(hit.point, battleStat.Speed, (float temp) => 
            {
                myAnim.SetFloat("Move", temp);
                // if (playerstate == state.Fire || playerstate == state.Dadge)
                // {
                //     stopAct?.Invoke((float stop) => myAnim.SetFloat("Move", stop));  
                // }
                if(temp < 0.05f && playerstate != state.Dadge && playerstate != state.Fire)
                {
                    ChangeState(state.Idle);
                }
            });
        }
    }

    public RaycastHit GetRaycastHit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 1000.0f, clickMask))
        {
            return hit;
        }
        return hit;
    }

    public void DadgeToPos()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isDadgeReady)
        {
            stopAct?.Invoke((float stop) => myAnim.SetFloat("Move", stop));
            hit = GetRaycastHit();
            ChangeState(state.Dadge);
            myAnim.SetTrigger("t_Dadge");
            dadgeAct?.Invoke(hit.point, dadgePw);
        }
    }

    public void OnEnd(int type)
    {
        switch (type)
        {
            case 0:
                FireDelay = equipWeapon.rate;
                break;
            case 1:
                DadgeDelay = 0.0f;
                break;
        }
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
}