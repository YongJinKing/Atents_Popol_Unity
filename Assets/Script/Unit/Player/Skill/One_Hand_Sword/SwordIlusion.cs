using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordIlusion : MonoBehaviour
{
    public float delay;
    public int hitCount;
    float nowDelay = -0.3f;
    SphereCollider col;
    public float destroy;

    private void Start()
    {
        col = GetComponent<SphereCollider>();
        Destroy(this.gameObject, destroy);
    }
    void Update()
    {
        nowDelay += Time.deltaTime;
        if (nowDelay >= delay && hitCount * 2 >= 0)
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
