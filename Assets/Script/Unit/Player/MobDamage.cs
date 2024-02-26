using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MobDamage : MonoBehaviour
{
    public int maxHealth;
    public int curHealth;

    Rigidbody rigid;
    SphereCollider SpCollider;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        SpCollider = GetComponent<SphereCollider>();
    }
    
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("아픔");
    }
}
