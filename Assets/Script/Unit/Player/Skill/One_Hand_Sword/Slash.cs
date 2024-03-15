using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;


public class Slash : MonoBehaviour
{
    public float moveSpeed = 10f; 
    public float destroyDelay = 1f;
    
    void Start()
    {
        
        Rigidbody rb = GetComponent<Rigidbody>();
        
        if (rb != null && rb.mass > 0)
        {
            rb.AddForce(transform.forward * moveSpeed, ForceMode.Impulse);
        }

        Destroy(gameObject, destroyDelay);
    }
}
