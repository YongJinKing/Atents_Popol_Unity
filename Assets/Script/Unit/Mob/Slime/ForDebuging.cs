using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForDebuging : MonoBehaviour
{
    public void OnSkillHitboxStart()
    {
        Debug.Log("OnSkillHitCheckStart");
    }
    public void OnSkillHitboxEnd()
    {
        Debug.Log("OnSkillHitCheckEnd");
    }

    public void OnAttackEnd()
    {
        Debug.Log("OnAttackEnd");
    }
}
