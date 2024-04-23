using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InflictStun : MonoBehaviour
{
    public void InStun(Collider target)
    {
        target.GetComponentInParent<IStun>().GetStun();
    }
}
