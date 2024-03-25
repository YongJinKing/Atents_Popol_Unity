using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMove : MonoBehaviour
{
    //Empty오브젝트에 스크립트를 적용시키고 자녀로 카메라를 넣어 사용

    public LayerMask crashMask;

    float targetDist;
    float camDist = 8;
    Transform myCam = null;
    public Transform playerPos;
    public Vector2 playerAngle;
    public Vector3 playeroffSet;
    public float trackSpeed = 7.0f;
    public Vector2 zoomRange = new Vector2(1, 15);
    public float zoomSpeed = 5.0f;
    public bool wheelClickRot = true;
    public float rotationSpeed = 0.1f; // 회전 속도
    private Vector3 lastMousePosition; // 마우스 이전 위치

    public bool raycastDebug = false;

    void Start()
    {
        myCam = GetComponentInChildren<Camera>().transform;
        camDist = targetDist = Mathf.Abs(myCam.localPosition.z);
        transform.rotation = Quaternion.Euler(playerAngle.x, playerAngle.y, 0);
    }

    void Update()
    {
        //스크롤 줌
        targetDist -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        targetDist = Mathf.Clamp(targetDist, zoomRange.x, zoomRange.y);

        //카메라 벽 충돌 감지
        float offSet = 0.5f;
        Vector3 rayoffSet = new Vector3(0, -1, 0);
        if (Physics.Raycast(new Ray(transform.position + rayoffSet, -transform.forward),
            out RaycastHit hit, camDist + offSet, crashMask))
        {
            camDist = hit.distance - offSet;
        }
        myCam.localPosition = new Vector3(0, 0, -camDist);

        //RayCast Debug
        if (raycastDebug)
        {
            Debug.DrawRay(transform.position + rayoffSet, -transform.forward.normalized * zoomRange.y, Color.green);
        }

        //카메라 이동
        camDist = Mathf.Lerp(camDist, targetDist, Time.deltaTime * zoomSpeed);
        transform.rotation = Quaternion.Euler(playerAngle.x, playerAngle.y, 0);

        //플레이어 트레킹
        transform.position = Vector3.Lerp(transform.position, playerPos.position + playeroffSet, Time.deltaTime * trackSpeed);

        //WheelClick Rotation
        if (wheelClickRot & Input.GetMouseButton(2))
        {
            Vector3 curMousePosition = Input.mousePosition;
            Vector3 deltaMousePosition = curMousePosition - lastMousePosition;
            playerAngle.y += deltaMousePosition.x * rotationSpeed;
            lastMousePosition = curMousePosition;
        }
        else { lastMousePosition = Input.mousePosition; }
    }



    public void UnitDeath()
    {
        //플레이어나 몬스터가 죽었을시에 실행 되는 함수

        Debug.Log("유닛이 사망하였습니다");
    }
}
