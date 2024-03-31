using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroadCastCamControll : MonoBehaviour
{
    public Transform PlayerCam;
    public Transform MonsterCam;
    Collider[] colliders;
    LayerMask player;
    LayerMask Monster;

    void Start()
    {
        colliders = Physics.OverlapSphere(transform.position, 10f, player);
    }
    
    void Update()
    {
        
    }
}
