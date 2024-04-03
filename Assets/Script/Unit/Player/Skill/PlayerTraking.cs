using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTraking : MonoBehaviour
{
    public LayerMask player;
    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 10f, player);
        Vector3 targetPosition = colliders[0].transform.position;
        transform.position = targetPosition;

    }
}
