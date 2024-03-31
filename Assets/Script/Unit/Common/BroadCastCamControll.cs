using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroadCastCamControll : MonoBehaviour
{
    public LayerMask playerLayer;
    public LayerMask monsterLayer;

    public GameObject playerObject;
    public GameObject monsterObject;

    public Vector3 playerOffset;
    public Vector3 monsterOffset;

    public Transform plCam;
    public Transform moCam;

    void Start()
    {
        Collider[] playerColliders = Physics.OverlapSphere(transform.position, 100f, playerLayer);
        if (playerColliders.Length > 0)
        {
            playerObject = playerColliders[0].gameObject;
        }

        Collider[] monsterColliders = Physics.OverlapSphere(transform.position, 100f, monsterLayer);
        if (monsterColliders.Length > 0)
        {
            monsterObject = monsterColliders[0].gameObject;
        }
    }
    
    void Update()
    {
        transform.position = playerObject.transform.position + (monsterObject.transform.position - playerObject.transform.position) / 2;

        plCam.transform.position = playerObject.transform.position + playerOffset;
        moCam.transform.position = monsterObject.transform.position + monsterOffset;

        plCam.transform.LookAt(playerObject.transform);
        moCam.transform.LookAt(monsterObject.transform);
    }
}
