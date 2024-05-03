using System.Collections;
using UnityEngine;

public class MultiHitHitBox : HitOnceHitBox
{
    [SerializeField]protected float _hitFrequency = 0.5f;
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

    protected IEnumerator Refreshing()
    {
        while (true)
        {
            yield return new WaitForSeconds(hitFrequency);
            calculatedObject.Clear();
        }
    }
}
