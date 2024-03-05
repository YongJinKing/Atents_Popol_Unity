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
    public UnityEvent<Vector3, float> dadgeAct;
    public GameObject jointItemR;
    public LayerMask clickMask;
    public LayerMask attackMask;
    Weapon equipWeapon;
    float FireDelay = 0;
    public float DadgeDelay = 0;
    bool isFireReady = true;
    
    bool isDadgeReady = true;
    
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
            stopAct?.Invoke(() => myAnim.SetBool("b_Moving", false));
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 1000.0f, clickMask))
            {
                ChangeState(state.Fire);
                attackAct?.Invoke(hit.point, equipWeapon);
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
                clickAct?.Invoke(hit.point, battleStat.Speed, () => myAnim.SetBool("b_Moving", true),
                    () =>
                    {
                        if (playerstate == state.Fire || playerstate == state.Dadge)
                        {
                            stopAct?.Invoke(() => 
                            {
                                myAnim.SetBool("b_Moving", false);
                                ChangeState(state.Idle);
                            });
                        }
                        ChangeState(state.Idle);
                        myAnim.SetBool("b_Moving", false);
                    });
            }
        }
    }
    
    public void DadgeToPos()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isDadgeReady)
        {
            stopAct?.Invoke(() => myAnim.SetBool("b_Moving", false));
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 1000.0f, clickMask))
            {
                myAnim.SetTrigger("t_Dadge");
                ChangeState(state.Dadge);
                dadgeAct?.Invoke(hit.point, 20.0f);
            }
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
                DadgeDelay = 1.0f;
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