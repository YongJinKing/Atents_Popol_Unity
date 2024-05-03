using System.Collections;
using UnityEngine;

public class ColiderBlink : MonoBehaviour
{
    public float startDelay;
    public int colCount;
    public float colDelay;
    SphereCollider col;
    SkillManager sk;

    // Start is called before the first frame update
    void Start()
    {
        sk = GetComponent<SkillManager>();
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
                sk.OnColliderBlink();
                yield return new WaitForSeconds(colDelay-0.05f);
            }
        }
    }
}
