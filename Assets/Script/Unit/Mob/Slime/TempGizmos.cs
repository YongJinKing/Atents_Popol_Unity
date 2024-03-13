using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempGizmos : MonoBehaviour
{
    private Collider myCol;

    private void Start()
    {
        myCol = GetComponent<Collider>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, myCol.bounds.extents);
    }
}