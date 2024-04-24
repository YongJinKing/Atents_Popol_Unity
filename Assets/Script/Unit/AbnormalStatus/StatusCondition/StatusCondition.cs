using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusCondition : MonoBehaviour
{
    protected E_Buff _myBuffType = E_Buff.None;
    protected E_StatusAbnormality _myStatusAbType = E_StatusAbnormality.None;
    public E_StatusAbnormality myStatusAbType
    {
        get { return _myStatusAbType; }
        set { _myStatusAbType = value;}
    }
    public E_Buff myBuffType
    {
        get { return _myBuffType;}
        set { _myBuffType = value; }
    }


    public virtual void Overlap(){}

    public virtual void Remove()
    {
        Status s = GetComponentInParent<Status>();
        if (s)
        {
            if(myBuffType != E_Buff.None)
            {
                s.Remove(myBuffType);
            }
            else if(myStatusAbType != E_StatusAbnormality.None)
                s.Remove(myStatusAbType);
        }
            
    }
}