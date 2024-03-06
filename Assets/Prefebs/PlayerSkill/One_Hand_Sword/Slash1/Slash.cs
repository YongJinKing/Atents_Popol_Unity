using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
    public float moveSpeed = 10f; // 앞으로 날아가는 속도
    public float destroyDelay = 1f; // 사라질 시간

    void Start()
    {
        // Rigidbody 컴포넌트를 가져옵니다.
        Rigidbody rb = GetComponent<Rigidbody>();

        // Rigidbody가 존재하고, 힘을 가할 수 있는 경우에만 힘을 가합니다.
        if (rb != null && rb.mass > 0)
        {
            rb.AddForce(transform.forward * moveSpeed, ForceMode.Impulse);
        }

        // destroyDelay 이후에 오브젝트를 파괴합니다.
        Destroy(gameObject, destroyDelay);
    }
}
