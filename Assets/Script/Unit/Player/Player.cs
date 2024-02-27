using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator myAnim;
    public GameObject WeaponPoint;
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
