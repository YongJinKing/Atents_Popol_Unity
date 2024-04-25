using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InflictBuff : MonoBehaviour
{
    public E_StatusAbnormality myab;
    public void GetCol(Collider target)
    {
        Status st = target.GetComponentInParent<Status>();
        st.Add(myab);
    }
}
