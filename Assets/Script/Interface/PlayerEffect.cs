using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_Effect
{
    public void PlayAttackEffect();
    public void PlaySkillEffect();
}

public class PlayerEffect : MonoBehaviour, I_Effect
{
    
    public void PlayAttackEffect()
    {
        GameObject effect = Instantiate<GameObject>(Resources.Load("Skill/Player/SimpleAttack") as GameObject);
        Vector3 dir = transform.parent.transform.forward;
        effect.transform.position = transform.position;

        effect.transform.rotation = Quaternion.LookRotation(dir);
        Destroy(effect.gameObject, 0.5f);
    }

    public void PlaySkillEffect()
    {
        GameObject effect = Instantiate<GameObject>(Resources.Load("Skill/Player/Slash") as GameObject);
        Vector3 dir = transform.parent.transform.forward;
        effect.transform.position = transform.position;

        effect.transform.rotation = Quaternion.LookRotation(dir);
        Destroy(effect.gameObject, 4.0f);
    }

}
