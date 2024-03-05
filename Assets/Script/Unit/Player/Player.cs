using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Player : BattleSystem
{
    public UnityEvent<Vector3, float, UnityAction, UnityAction> clickAct;
    public UnityEvent<Vector3, Weapon> attackAct;
    public UnityEvent<UnityAction> stopAct;
    public GameObject jointItemR;
    public LayerMask clickMask;
    public LayerMask attackMask;
    public Rigidbody rigid;
    Weapon equipWeapon;
    float FireDelay = 0;
    bool isFireReady = true;
    
    public enum state
    {
        Fire, Dadge, Idle, Run
    }
    protected state playerstate;

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
                FireToMousePos();
                DadgeToPos();
                break;
        }
    }



    protected virtual void Start()
    {
        playerstate = state.Idle;
        rigid = GetComponent<Rigidbody>();
        equipWeapon = jointItemR.transform.GetChild(0).GetComponent<Weapon>();
    }

    public void FireToMousePos()
    {
        if (Input.GetMouseButtonDown(0))
        {
            stopAct?.Invoke(() => myAnim.SetBool("b_Moving", false));
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 1000.0f, clickMask))
            {
                if (Input.GetMouseButtonDown(0) && isFireReady)
                {
                    attackAct?.Invoke(hit.point, equipWeapon);
                    ChangeState(state.Fire);
                }
            }
        }
    }

    public void MoveToMousePos()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 1000.0f, clickMask))
            {
                ChangeState(state.Run);
                clickAct?.Invoke(hit.point, battleStat.Speed, () => myAnim.SetBool("run", true),
                    () =>
                    {
                        if (playerstate == Player.state.Fire || playerstate == Player.state.Dadge)
                        {
                            stopAct?.Invoke(() => myAnim.SetBool("run", false));
                            ChangeState(state.Idle);
                        }
                    });
            }
        }
    }
    
    public void DadgeToPos()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myAnim.SetTrigger("Dadge");
            ChangeState(state.Dadge);
            rigid.AddForce(transform.forward * 10.0f, ForceMode.Impulse);
            Invoke("setstateIdle", 0.2f);
        }
    }


    void Update()
    {
        FireDelay -= Time.deltaTime;
        isFireReady = FireDelay < 0;

        ProcessState();        
        
        
        
        

        if(myAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack") && myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f)
        {
            FireDelay = equipWeapon.rate;
            playerstate = state.Idle;
        }
    }

    void setstateIdle()
    {
        ChangeState(state.Idle);
    }
}