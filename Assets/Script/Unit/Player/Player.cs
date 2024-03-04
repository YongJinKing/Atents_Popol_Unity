using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Player : BattleSystem
{
    public UnityEvent<Vector3, float> clickAct;
    public UnityEvent<Vector3, Weapon> attackAct;
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
    public state playerstate;

    protected virtual void Start()
    {
        playerstate = state.Idle;
        rigid = GetComponent<Rigidbody>();
        equipWeapon = jointItemR.transform.GetChild(0).GetComponent<Weapon>();
    }

    

    void Update()
    {
        FireDelay -= Time.deltaTime;
        isFireReady = FireDelay < 0;
        
        if(Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out RaycastHit hit, 1000.0f, clickMask))
            {
                if(Input.GetMouseButtonDown(0) && isFireReady && state.Fire != playerstate)
                {
                    attackAct?.Invoke(hit.point, equipWeapon);
                    playerstate = state.Fire;
                }

                else if (Input.GetMouseButtonDown(1) && state.Idle == playerstate)
                {
                    playerstate = state.Run;
                    clickAct?.Invoke(hit.point, battleStat.Speed);
                }
            }
        }
        
        if(Input.GetKeyDown(KeyCode.Space) && state.Fire != playerstate)
        {
            myAnim.SetTrigger("Dadge");
            playerstate = state.Dadge;
            rigid.AddForce(transform.forward * 10.0f, ForceMode.Impulse);
            Invoke("setstateIdle", 0.2f);
        }
        

        if(myAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack") && myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f)
        {
            FireDelay = equipWeapon.rate;
            playerstate = state.Idle;
        }
    }

    void setstateIdle()
    {
        playerstate = state.Idle;
    }
}