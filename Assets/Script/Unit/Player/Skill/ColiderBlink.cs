using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColiderBlink : MonoBehaviour
{
    public float startDelay;
    public int colCount;
    public float colDelay;
    SphereCollider col;

    // Start is called before the first frame update
    void Start()
    {
        col=GetComponent<SphereCollider>();
        StartCoroutine(colBlink());
    }

    IEnumerator colBlink()
    {
        yield return new WaitForSeconds(startDelay);
        while(colCount>0)
        {
            if (!col.enabled)
            {
                col.enabled = true;
                yield return null;
                yield return new WaitForSeconds(0.05f);
            }
            else
            {
                col.enabled = false;
                --colCount;
                yield return new WaitForSeconds(colDelay-0.05f);
            }
        }
    }
}
