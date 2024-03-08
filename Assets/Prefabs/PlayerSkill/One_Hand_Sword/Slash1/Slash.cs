using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
    public float moveSpeed = 10f; // ������ ���ư��� �ӵ�
    public float destroyDelay = 1f; // ����� �ð�

    void Start()
    {
        // Rigidbody ������Ʈ�� �����ɴϴ�.
        Rigidbody rb = GetComponent<Rigidbody>();

        // Rigidbody�� �����ϰ�, ���� ���� �� �ִ� ��쿡�� ���� ���մϴ�.
        if (rb != null && rb.mass > 0)
        {
            rb.AddForce(transform.forward * moveSpeed, ForceMode.Impulse);
        }

        // destroyDelay ���Ŀ� ������Ʈ�� �ı��մϴ�.
        Destroy(gameObject, destroyDelay);
    }
}
