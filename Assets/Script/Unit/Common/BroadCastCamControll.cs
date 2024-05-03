using UnityEngine;

public class BroadCastCamControll : MonoBehaviour
{
    public LayerMask playerLayer;
    public LayerMask monsterLayer;

    public Transform playerObject;
    public Transform monsterObject;

    public Vector3 playerOffset;
    public Vector3 monsterOffset;

    public Transform plCam;
    public Transform moCam;

    public bool RayDebug;

    bool flip = false;

    Vector3 plPos;
    Vector3 moPos;
    Vector3 plOff;
    Vector3 moOff;

    void Start()
    {
        
    }
    private void Awake()
    {
        Collider[] playerColliders = Physics.OverlapSphere(transform.position, 100f, playerLayer);
        if (playerColliders.Length > 0)
        {
            playerObject = playerColliders[0].transform;
        }

        Collider[] monsterColliders = Physics.OverlapSphere(transform.position, 100f, monsterLayer);
        if (monsterColliders.Length > 0)
        {
            monsterObject = monsterColliders[0].transform;
        }
        for (int i = 0; i<=3; ++i)
        {
            Traking();
        }
    }

    void Update()
    {
        
    }

    void Traking()
    {
        if (playerObject != null && monsterObject != null)
        {
            plPos = new Vector3(playerObject.position.x, 0, playerObject.position.z);
            moPos = new Vector3(monsterObject.position.x, 0, monsterObject.position.z);

            plOff = new Vector3(playerOffset.x, playerOffset.y, Vector3.Distance(transform.position, plPos) - playerOffset.z);
            moOff = new Vector3(monsterOffset.x, monsterOffset.y, -Vector3.Distance(transform.position, moPos) - monsterOffset.z);

            transform.position = plPos + (moPos - plPos) * 0.5f;
            transform.rotation = Quaternion.LookRotation(plPos - moPos);

            plCam.transform.localPosition = plOff;
            moCam.transform.localPosition = moOff;

            float digree = Vector3.Angle(transform.right.normalized, (Vector3.zero - transform.position).normalized);

            if (digree >= 90) flip = true;
            else flip = false;

            if (flip)
            {
                playerOffset.x = -Mathf.Abs(playerOffset.x);
                monsterOffset.x = -Mathf.Abs(monsterOffset.x);
                plCam.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
                moCam.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
            }
            else
            {
                playerOffset.x = Mathf.Abs(playerOffset.x);
                monsterOffset.x = Mathf.Abs(monsterOffset.x);
                plCam.transform.localRotation = Quaternion.Euler(0f, -90f, 0f);
                moCam.transform.localRotation = Quaternion.Euler(0f, -90f, 0f);
            }
        }
        else
        {
            plPos = new Vector3(playerObject.position.x, 0, playerObject.position.z);
            moCam.transform.position = Vector3.zero;

            transform.position = plPos + (moPos - plPos) * 0.5f;
            transform.rotation = Quaternion.LookRotation(plPos - moPos);

            plOff = new Vector3(0, playerOffset.y, Vector3.Distance(transform.position, plPos) - playerOffset.z*6);
            plCam.transform.localPosition = plOff;
            plCam.rotation = Quaternion.LookRotation(plPos - moPos);
        }
    }
    
    private void OnDrawGizmos()
    {
        if (RayDebug)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(transform.position, 1f);
            Gizmos.DrawWireSphere(plCam.transform.position, 1f);
            Gizmos.DrawWireSphere(moCam.transform.position, 1f);

            Debug.DrawRay(transform.position, transform.forward.normalized * 2, Color.blue);
            Debug.DrawRay(transform.position, transform.right.normalized * 2, Color.red);
            Debug.DrawRay(plCam.transform.position, plCam.transform.forward.normalized * 2, Color.blue);
            Debug.DrawRay(moCam.transform.position, moCam.transform.forward.normalized * 2, Color.blue);
        }
    }
}
