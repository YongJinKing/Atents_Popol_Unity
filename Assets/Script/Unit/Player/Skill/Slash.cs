using UnityEngine;


public class Slash : MonoBehaviour
{
    public float moveSpeed = 10f;
    
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        
        if (rb != null && rb.mass > 0)
        {
            rb.AddForce(transform.forward * moveSpeed, ForceMode.Impulse);
        }
    }
}
