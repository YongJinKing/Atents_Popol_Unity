using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum Type {Melee, Range};
    public Type type;
    public int damage;
    public float rate;
    public BoxCollider meleeArea;
    public TrailRenderer trailEffect;

    //             yield return new WaitForSeconds(0.1f);
    //             meleeArea.enabled = true;
    //             trailEffect.enabled = true;
    //             yield return new WaitForSeconds(0.3f);
    //             meleeArea.enabled = false;
    //             yield return new WaitForSeconds(0.3f);
    //             trailEffect.enabled = false;
    public void Use()
    {
        if(type == Type.Melee)
        {
            StopCoroutine("Swing");
            StartCoroutine("Swing");
        }
    }
    
    IEnumerator Swing()
    {
        yield return new WaitForSeconds(0.1f);
        meleeArea.enabled = true;
        trailEffect.enabled = true;

        yield return new WaitForSeconds(1.5f);
        meleeArea.enabled = false;
        yield return new WaitForSeconds(1.5f);
        trailEffect.enabled = false;
        
    }

    
}
