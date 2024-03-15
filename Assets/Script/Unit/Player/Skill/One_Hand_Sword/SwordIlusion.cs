using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordIlusion : MonoBehaviour
{
    public float delay;
    public int hitCount;
    float nowDelay;
    public SphereCollider col;

    private void Start()
    {
        col = GetComponentInChildren<SphereCollider>();
        Destroy(this.gameObject, 4f);
    }
    void Update()
    {
        nowDelay += Time.deltaTime;
        if (nowDelay >= delay && hitCount*2 >= 0)
        {
            if (col.enabled)
            {
                col.enabled = false;
            }
            else
            {
                col.enabled = true;
            }
            hitCount--;
            nowDelay = 0;
        }
    }
}
