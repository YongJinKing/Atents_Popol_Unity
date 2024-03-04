using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Player : Unit
{
    public UnityEvent<Vector3, float, UnityAction, UnityAction> clickAct;
    public UnityEvent<Vector3, Weapon> attackAct;
    public GameObject jointItemR;
    public LayerMask clickMask;
    public LayerMask attackMask;
    Weapon equipWeapon;
    float FireDelay = 0;
    bool isFireReady = true;
    bool isFire = false;

    protected virtual void Start()
    {
        equipWeapon = jointItemR.transform.GetChild(0).GetComponent<Weapon>();
        Debug.Log($"¿þÆù: {equipWeapon}");
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
                Debug.Log(FireDelay);
                if(Input.GetMouseButtonDown(0) && isFireReady && !isFire)
                {
                    attackAct?.Invoke(hit.point, equipWeapon);
                    isFire = true;
                }
                else if (Input.GetMouseButtonDown(1) && !myAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                {
                    clickAct?.Invoke(hit.point, battleStat.Speed,
                        () => myAnim.SetBool("run", true),
                        () => myAnim.SetBool("run", false));
                }
            }
        }

        if(myAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack") && myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f)
        {
            FireDelay = equipWeapon.rate;
            isFire = false;
        }
    }
}