using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    public Transform myTarget;
    public float zoomSpeed = 2.0f;
    public float rotSpeed = 5.0f;
    public Vector2 verticalRotRange = new Vector2(0.0f, 70.0f);
    public Vector2 zoomRange = new Vector2(1.0f, 8.0f);
    Vector3 offset;
    float dist = 0.0f;
    Vector2 valueRot;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - myTarget.position;
        dist = offset.magnitude;
        offset.Normalize();
        valueRot.x = Vector3.Angle(new Vector3(0, offset.y, offset.z).normalized, Vector3.back);
        valueRot.y = Vector3.Angle(new Vector3(offset.x, 0, offset.z).normalized, Vector3.back);
    }

    // Update is called once per frame
    void Update()
    {
        float data = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        dist -= data;
        dist = Mathf.Clamp(dist, zoomRange.x, zoomRange.y);

        if (Input.GetMouseButton(1))
        {
            valueRot.y += Input.GetAxis("Mouse X") * rotSpeed;
            valueRot.x -= Input.GetAxis("Mouse Y") * rotSpeed;
            valueRot.x = Mathf.Clamp(valueRot.x, verticalRotRange.x, verticalRotRange.y);            
        }

        Quaternion rotX = Quaternion.Euler(0, valueRot.y, 0);
        Quaternion rotY = Quaternion.Euler(valueRot.x, 0, 0);
        transform.position = myTarget.position + rotX * rotY * Vector3.back * dist;
        transform.LookAt(myTarget);
    }
}
