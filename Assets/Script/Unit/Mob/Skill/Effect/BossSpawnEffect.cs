using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawnEffect : MonoBehaviour
{
    public float ColSize;
    public float Speed;
    SphereCollider col;
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<SphereCollider>();
        col.radius = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(col.radius <= ColSize)
            col.radius += Time.deltaTime * Speed;
    }
}
