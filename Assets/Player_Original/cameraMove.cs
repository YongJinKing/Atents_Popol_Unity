using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMove : MonoBehaviour
{
    public Transform playerTransform;


    Transform cameraTransform;
    public float FallowSpeed = 1.0f;
    public Vector3 offset;


    private void Awake()
    {
        cameraTransform = GetComponent<Transform>();
    }

    private void Start()
    {
        offset = playerTransform.position - cameraTransform.position;
    }

    private void LateUpdate()
    {
        cameraTransform.position = Vector3.Lerp(cameraTransform.position,playerTransform.position - offset, Time.deltaTime * FallowSpeed);
    }
}
