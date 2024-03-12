using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    void OnTriggerEnter (Collider other)
    {
        Debug.Log(other);
        if(other.gameObject.layer == LayerMask.NameToLayer("Monster_Body"))
        {
            BattleSystem bs = other.GetComponent<BattleSystem>();
            if(bs != null)
            {
                bs.TakeDamage(1000);
            }
        }
        
    }
}
