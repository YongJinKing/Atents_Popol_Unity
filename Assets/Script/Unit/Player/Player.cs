using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : Unit
{
    public UnityEvent<Vector3, float, UnityAction, UnityAction> clickAct;
    public GameObject WeaponPoint;
    public LayerMask clickMask;
    bool Fire;
    bool isFireReady;
    float FireDelay;
    Weapon equipWeapon;
    // Start is called before the first frame update
    void Start()
    {
        equipWeapon = WeaponPoint.transform.GetChild(0).GetComponent<Weapon>();
        FireDelay = 0;
        Debug.Log($"equipWeapon:{equipWeapon}");
    }

    void Update()
    {
        GetInput();
        Attack();
        if(Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out RaycastHit hit, 1000.0f, clickMask))
            {
                clickAct?.Invoke(hit.point, battleStat.Speed,
                    () => myAnim.SetBool("run", true),
                    () => myAnim.SetBool("run", false));
            }
            
        }
    }

    void GetInput()
    {
        Fire = Input.GetKey(KeyCode.Mouse0);
        Debug.Log(Fire);
    }
    // Update is called once per frame
    

    void Attack()
    {
        if (equipWeapon == null) return;

        FireDelay += Time.deltaTime;
        isFireReady = equipWeapon.rate < FireDelay;
        Debug.Log("isFireReady: " + isFireReady);

        if(Fire && isFireReady)
        {
            myAnim.SetTrigger("Attack");
            equipWeapon.Use();
            FireDelay = 0;
        }
    }
}
