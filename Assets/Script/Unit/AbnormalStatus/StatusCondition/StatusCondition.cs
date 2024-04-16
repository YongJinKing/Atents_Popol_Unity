using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusCondition : MonoBehaviour
{
    public virtual void Remove()
    {
        Status s = GetComponent<Status>();
        if (s)
            s.Remove(this);
    }
}