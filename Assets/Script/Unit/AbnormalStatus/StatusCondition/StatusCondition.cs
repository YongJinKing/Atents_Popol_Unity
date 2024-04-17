using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusCondition : MonoBehaviour
{
    protected E_StatusAbnormality _myStatusAbType;
    public E_StatusAbnormality myStatusAbType
    {
        get { return _myStatusAbType; }
        set { _myStatusAbType = value;}
    }


    public virtual void Overlap(){}

    public virtual void Remove()
    {
        Status s = GetComponentInParent<Status>();
        if (s)
            s.Remove(myStatusAbType);
    }
}