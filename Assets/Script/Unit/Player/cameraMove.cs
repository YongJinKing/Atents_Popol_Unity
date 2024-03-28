using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMove : MonoBehaviour
{
    //Empty오브젝트에 스크립트를 적용시키고 자녀로 카메라를 넣어 사용
    [Header("MainCam Setting")]
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

    public static bool raycastDebug = false;

    [Space(3)]
    [Header("CineCam Setting")]
    public Transform MonsterCam;
    public Transform PlayerCam;
    Transform target;
    public float cineCamSpeed;
    public bool isCine = false;
    public bool isTracking = true;
    int DeathUnit;

    void Start()
    {
        myCam = GetComponentInChildren<Camera>().transform;
        camDist = targetDist = Mathf.Abs(myCam.localPosition.z);
        transform.rotation = Quaternion.Euler(playerAngle.x, playerAngle.y, 0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UnitDeath(0);
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

        if (!isCine && isTracking)
        {
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
        }

        if (isCine)
        {
            //StartCoroutine(CineCam());
        }
    }

    public void UnitDeath(int Unit)
    {
        //플레이어나 몬스터가 죽었을시에 실행 되는 함수
        // Unit = 0 일때 몬스터 Unit = 1 일때 플레이어
        isCine = true;
        isTracking = false;
        DeathUnit = Unit;

        switch (Unit)
        {
            case 0 : target = MonsterCam;
                break;
            case 1 : target = PlayerCam;
                break;
        }
        originPos = new Vector3(0, 0, myCam.transform.localPosition.z);
        //Debug.Log("유닛이 사망하였습니다");
    }

    public string CinecamState = "CamMove";
    Vector3 originPos;


    /*IEnumerator TimeControl() //코루틴 예시 
    {
        float slowTime = 0.5f; // 속도를 정할 변수 선언
        while(!Mathf.Approximately(slowTime, 0.1f)) // slowTime이 0.1과 근사값이 아닐때
        {
            Time.timeScale = slowTime; // 현재 시간 속도 = (변수)slowTime
            slowTime -= Time.deltaTime * 1.5f; // slowTime의 값을 서서히 줄인다.
            if (slowTime < 0.1f)    // slowTime이 0.1f 보다 적어 질때 
            {
                slowTime = 0.1f; // slowTime을 0,1로 고정
            }
            yield return null; // Update문으로 만들어 주는 return
        }
        yield return new WaitForSecondsRealtime(1.0f); // 느려진 상태로 1초 동안 유지 게임 시간이 아닌 컴퓨터시간

        slowTime = 0.5f; // 시간 속도를 0.5로 만들어줌
        while (slowTime < 1.0f) //1배속으로 정확 하게 하기 위한 조건문
        {
            slowTime += Time.deltaTime; // 초당 slowTime 값을 올림
            if(slowTime > 1.0f) // slowTime이 1.0을 넘어갔을때
            {
                slowTime = 1.0f; // slowTime을 1.0으로 고정
            }
            Time.timeScale = slowTime; //프레임 마다 slowTime값으로 타임 스캐일 조정
            yield return null; // Update문으로 만들어 주는 return
        }
    }*/

    /*IEnumerator CineCam()
    {
        if (CinecamState == "CamMove")
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
                yield return null;
            }
        }

        if (CinecamState == "TimeFaster")
        {
            if (Time.timeScale < 0.9f)
            {
                Time.timeScale += Time.deltaTime * 0.7f;
                yield return null;
            }
            else
            {
                Time.timeScale = 3f;
                CinecamState = "GoBack";
            }
        }

        if (CinecamState == "GoBack")
        {
            myCam.transform.localPosition = Vector3.Lerp(myCam.transform.localPosition, originPos, Time.deltaTime * cineCamSpeed);
            myCam.transform.localRotation = Quaternion.Lerp(myCam.transform.localRotation, Quaternion.identity, Time.deltaTime * cineCamSpeed);

            if (Vector3.Distance(myCam.transform.localPosition, originPos) <= 0.01f)
            {
                CinecamState = "CamMove";
                isCine = false;
                isTracking = true;
                yield return null;
            }
        }
        yield return null;
    }*/
}
