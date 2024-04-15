using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTraking : MonoBehaviour
{
    public LayerMask targetLayer;
    public Vector3 offSet;
    GameObject target;

    public bool isRot;
    public float RotSpeed;
    private void Start()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 10f, targetLayer);
        target = colliders[0].gameObject;
        Debug.Log(target.name);
        Debug.Log(target.transform.position);
    }
    void Update()
    {
        transform.position = target.transform.position + offSet;
        transform.rotation = target.transform.rotation;

        if (isRot)
        {
            transform.GetChild(0).gameObject.transform.Rotate(Vector3.up * Time.deltaTime * RotSpeed);
        }
    }
}
