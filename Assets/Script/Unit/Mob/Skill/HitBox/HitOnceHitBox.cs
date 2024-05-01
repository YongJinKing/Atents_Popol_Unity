using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitOnceHitBox : BaseHitBox
{
    protected HashSet<Collider> calculatedObject = new HashSet<Collider>();
    protected float _hitDuration = 2;
    public float hitDuration
    {
        get { return _hitDuration; }
        set { _hitDuration = value; }
    }

    protected override void Start()
    {
        base.Start();
        StartCoroutine(DurationChecking());
    }

    protected override void OnCollisionStay(Collision collision)
    {
        if(((1 << collision.gameObject.layer) & targetMask) > 0 )
        {
            if (!calculatedObject.Contains(collision.collider))
            {
                calculatedObject.Add(collision.collider);
                Debug.Log("onCollisionStay in HitOnceHitBox");
                onHitEvent?.Invoke(collision.collider);
            }
        }
    }

    protected IEnumerator DurationChecking()
    {
        yield return new WaitForSeconds(hitDuration);
        Destroy(gameObject);
    }
}
