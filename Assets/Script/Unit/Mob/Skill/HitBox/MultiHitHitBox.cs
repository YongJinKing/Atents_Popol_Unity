using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiHitHitBox : HitOnceHitBox
{
    protected float _hitFrequency = 0.5f;
    public float hitFrequency
    {
        set { _hitFrequency = value; }
        get { return _hitFrequency; }
    }

    protected override void Start()
    {
        base.Start();
        StartCoroutine(Refreshing());
    }

    protected override void OnCollisionStay(Collision collision)
    {
        base.OnCollisionStay(collision);
    }

    protected IEnumerator Refreshing()
    {
        while (true)
        {
            yield return new WaitForSeconds(hitFrequency);
            calculatedObject.Clear();
        }
    }
}
