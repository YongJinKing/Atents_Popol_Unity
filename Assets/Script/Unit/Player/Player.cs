using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject WeaponPoint;
    public Transform myHend;
    Vector3 dir;
    float dist;
    bool Fire;
    bool isFireReady;
    float FireDelay;
    Weapon equipWeapon;
    // Start is called before the first frame update
    void Start()
    {
        equipWeapon = WeaponPoint.transform.GetChild(0).GetComponent<Weapon>();
        dir = Vector3.up;
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
            StopAllCoroutines();
            StartCoroutine("Attect");
            Debug.Log("누름");
            equipWeapon.Use();
            FireDelay = 0;
        }
    }
    IEnumerator Attect()
    {
        float delta;
        dist = 150.0f;

        while(true)
        {
            delta = Time.deltaTime * 900.0f;
            if(dist < delta) delta = dist;

            dist -= delta;
            if(dist > 0.0f)
            {
                myHend.Rotate(dir * delta);
            }
            else
            {
                dir = -dir;
                Debug.Log(dir);
                yield break;
            }
            yield return null;
        }
    }
}
