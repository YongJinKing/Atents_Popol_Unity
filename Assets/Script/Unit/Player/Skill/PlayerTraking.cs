using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTraking : MonoBehaviour
{
    public LayerMask player;
    public Vector3 offSet;
    public bool isRot;
    public float RotSpeed;
    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 10f, player);
        Vector3 targetPosition = colliders[0].transform.position;
        transform.position = targetPosition + offSet;
        if (isRot)
        {
            transform.Rotate(Vector3.up * Time.deltaTime * RotSpeed);
        }
    }
}
