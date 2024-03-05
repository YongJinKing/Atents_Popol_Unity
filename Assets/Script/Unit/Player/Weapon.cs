using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float rate;
    public BoxCollider meleeArea;
    public TrailRenderer trailEffect;
    Coroutine Swing = null;
    public void Use()
    {
        if(Swing != null)
        {
            StopCoroutine(Swing);
            Swing = null;
        }
        
        Swing = StartCoroutine(ToSwing());
    }
    
    IEnumerator ToSwing()
    {
        yield return new WaitForSeconds(0.1f);
        meleeArea.enabled = true;
        trailEffect.enabled = true;

        yield return new WaitForSeconds(0.4f);
        meleeArea.enabled = false;
        yield return new WaitForSeconds(0.4f);
        trailEffect.enabled = false;
        
    }

    
}
