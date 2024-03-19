using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordWheel : MonoBehaviour
{
    SphereCollider col;
    int count = 2;
    float now;
    void Start()
    {
        col = GetComponent<SphereCollider>();
    }
    private void Update()
    {
        now += Time.deltaTime;
        if (count > 0 && now >= 0.1f)
        {
            if (col.enabled)
            {
                col.enabled = false;
                count--;
            }
            col.enabled = true;
            now = 0;
        }
    }
}
