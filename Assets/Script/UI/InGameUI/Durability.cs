using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Durability : MonoBehaviour
{
    public float maxDurability = 100.0f;
    public float curDurability;
    // Start is called before the first frame update
    void Start()
    {
        curDurability = maxDurability;
    }

    public void HitSomething()
    {
        curDurability -= 5;

        if (curDurability <= 0)
        {
            Debug.Log(gameObject.name + " is broken!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
