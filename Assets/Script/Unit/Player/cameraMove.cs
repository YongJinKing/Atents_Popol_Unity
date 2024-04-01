using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class cameraMove : MonoBehaviour
{
    [Header("MainCam Setting")]
    public LayerMask crashMask;

    public float targetDist;
    public float camDist = 8;
    public Transform myCam = null;
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

    [Space(3)]
    [Header("CineCam Setting")]
    public Transform MonsterCam;
    public Transform PlayerCam;
    Transform target;
    public float cineCamSpeed;
    public bool isCine = false;
    public bool isTracking = true;
    public string CinecamState = "CamMove";
    public float rotateSpeed;

    void Start()
    {
        //myCam = GetComponentInChildren<Camera>().transform;
        camDist = targetDist = Mathf.Abs(myCam.localPosition.z - camDist);
        transform.rotation = Quaternion.Euler(playerAngle.x, playerAngle.y, 0);
        Debug.Log("start"+camDist);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F8))
        {
            UnitDeath(1);
        }

        if (!isCine)     //마우스 입력과 관련된 코드
        {
            //스크롤 줌
            targetDist -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
            targetDist = Mathf.Clamp(targetDist, zoomRange.x, zoomRange.y);

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

        if (isTracking)
        {
            //카메라 벽 충돌 감지
            float offSet = 0.5f;
            Vector3 rayoffSet = new Vector3(0, -1, 0);
            if (Physics.Raycast(new Ray(transform.position + rayoffSet, -transform.forward),
                out RaycastHit hit, camDist + offSet, crashMask))
            {
                camDist = hit.distance - offSet;
            }
            

            //RayCast Debug
            if (raycastDebug)
            {
                Debug.DrawRay(transform.position + rayoffSet, -transform.forward.normalized * zoomRange.y, Color.green);
            }

            //카메라 이동
            camDist = Mathf.Lerp(camDist, targetDist, Time.deltaTime * zoomSpeed);
            transform.rotation = Quaternion.Euler(playerAngle.x, playerAngle.y, 0);
            myCam.localPosition = new Vector3(0, 0, -camDist);

            //플레이어 트레킹
            transform.position = Vector3.Lerp(transform.position, playerPos.position + playeroffSet, Time.deltaTime * trackSpeed);
        }

        if (isCine && CinecamState == "EndingRotate")
        {
            playerAngle.y += Time.deltaTime;
            transform.rotation = Quaternion.Euler(playerAngle.x, playerAngle.y * rotateSpeed, 0);
        }
    }

    public void UnitDeath(int Unit)
    {
        //플레이어나 몬스터가 죽었을시에 실행 되는 함수
        // Unit = 0 일때 몬스터 Unit = 1 일때 플레이어
        isCine = true;
        isTracking = false;

        switch (Unit)
        {
            case 0 : target = MonsterCam;
                targetDist = 2;
                playerAngle.x = 0;
                playeroffSet.y = 1f;
                break;
            case 1 : target = PlayerCam;
                targetDist = 5;
                playerAngle.x = 70;
                playeroffSet.y = 1f;
                break;
        }
        originPos = new Vector3(0, 0, myCam.transform.localPosition.z);
        StartCoroutine(CineCam());
    }

    
    Vector3 originPos;

    IEnumerator CineCam()
    {
        while (CinecamState == "CamMove")
        {
            Time.timeScale = 0.01f;

            myCam.transform.position = Vector3.Lerp(myCam.transform.position, target.position, Time.deltaTime * cineCamSpeed);
            myCam.transform.rotation = Quaternion.Lerp(myCam.transform.rotation, target.rotation, Time.deltaTime * cineCamSpeed);
            float dis = Vector3.Distance(myCam.transform.position, target.position);

            cineCamSpeed = 60;

            if (dis <= 0.5f)
            {
                cineCamSpeed = 1f;
                CinecamState = "TimeFaster";
            }
            yield return null;
        }

        while (CinecamState == "TimeFaster")
        {
            if (Time.timeScale < 0.9f)
            {
                Time.timeScale += Time.deltaTime * 0.7f;
                
            }
            else
            {
                Time.timeScale = 4f;
                CinecamState = "GoBack";
            }
            yield return null;
        }

        while (CinecamState == "GoBack")
        {
            myCam.transform.localPosition = Vector3.Lerp(myCam.transform.localPosition, originPos, Time.deltaTime * cineCamSpeed);
            myCam.transform.localRotation = Quaternion.Lerp(myCam.transform.localRotation, Quaternion.identity, Time.deltaTime * cineCamSpeed);

            if (Vector3.Distance(myCam.transform.localPosition, originPos) <= 0.01f)
            {
                Time.timeScale = 1f;
                CinecamState = "EndingRotate";
                isTracking = true;
            }
            yield return null;
        }
    }
}
