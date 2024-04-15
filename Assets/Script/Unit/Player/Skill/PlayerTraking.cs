using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTraking : MonoBehaviour
{
    public LayerMask player;
    public Vector3 offSet;
    Vector3 targetPosition;
    Player pl;
    public bool isRot;
    public float RotSpeed;
    private void Start()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 10f, player);
        pl = colliders[0].GetComponent<Player>();
        Debug.Log(colliders[0].name);
        Debug.Log(pl.name);
    }
    void Update()
    {
        targetPosition = pl.Effectobj.transform.position;
        transform.position = targetPosition + offSet;
        
        
        if (isRot)
        {
            transform.Rotate(Vector3.up * Time.deltaTime * RotSpeed);
        }
    }
}
