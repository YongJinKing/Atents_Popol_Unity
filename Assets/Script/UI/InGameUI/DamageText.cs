using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    void Start()
    {
        StartCoroutine("TxtAnim");
    }

    IEnumerator TxtAnim()
    {
        float scale = this.transform.localScale.x;
        while (true)
        {
            scale = Mathf.Lerp(scale,1f,Time.deltaTime * 15);
            transform.localScale = new Vector3(scale,scale,1);
            if(scale <= 1.01f)
            {
                Destroy(gameObject,0.5f);

            }
            yield return null;
        }
    }
}
