using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitOnceHitBox : BaseHitBox
{
    protected HashSet<Collider> calculatedObject = new HashSet<Collider>();
    [SerializeField]protected float _hitDuration = 2;
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
    protected override void OnEnable()
    {
        GetComponent<Collider>().enabled = true;
        if(onHitEvent != null)
        {
            onHitEvent.RemoveAllListeners();
            BaseSkillEffect[] effects = GetComponentsInChildren<BaseSkillEffect>();
            for (int i = 0; i < effects.Length; ++i)
            {
                onHitEvent.AddListener(effects[i].OnSkillHit);
            }
        }
    }

    protected override void OnDisable()
    {
        GetComponent<Collider>().enabled = false;
    }

    protected override void OnTriggerStay(Collider other)
    {
        //Debug.Log($"HitOnceHitBox, targetMask : {targetMask.value} \ncollision layer : {other.gameObject.layer}");
        if (((1 << other.gameObject.layer) & targetMask) > 0)
        {
            if (!calculatedObject.Contains(other))
            {
                calculatedObject.Add(other);
                if (Physics.Raycast(transform.position, other.bounds.center - transform.position, out RaycastHit hit, myCol.bounds.extents.magnitude, targetMask))
                    onHitEvent?.Invoke(other, hit.point);
            }
        }
    }

    protected IEnumerator DurationChecking()
    {
        yield return new WaitForSeconds(hitDuration);
        Destroy(gameObject);
    }
}
