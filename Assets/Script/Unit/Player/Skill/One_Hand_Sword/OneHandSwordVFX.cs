using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneHandSwordVFX : MonoBehaviour
{
    public GameObject HitVFX;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Monster_Body"))
        {
            Instantiate(HitVFX, other.ClosestPoint(transform.position), Quaternion.identity);
        }
    }
}
