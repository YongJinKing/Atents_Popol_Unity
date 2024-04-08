using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneHandSwordVFX : MonoBehaviour
{
    public GameObject HitVFX;
    public Camera myCam;
    public LayerMask targetMask;
    public float one = 1;
    public float two = 2;

    cameraMove camMove;

    public void Start()
    {
        myCam = Camera.main;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(((1 << other.gameObject.layer) & targetMask) != 0)
        {
            Instantiate(HitVFX, other.ClosestPoint(transform.position), Quaternion.identity);
            StartCoroutine(Shake(one, two));
        }
    }

    public IEnumerator Shake(float duration, float magnitud)
    {
        Vector3 oriPosition = myCam.transform.localPosition;
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-1, 1) * magnitud;
            float y = Random.Range(-1, 1) * magnitud;

            myCam.transform.localPosition = new Vector3(x, y, oriPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        myCam.transform.localPosition = oriPosition;
    }
}
