using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordDown : MonoBehaviour
{
    SphereCollider gameobject;
    public float delay;
    private void Start()
    {
        this.gameobject = GetComponent<SphereCollider>();
        Invoke("Col", delay);
    }

    private void Col()
    {
        gameobject.enabled = true;
    }
}
