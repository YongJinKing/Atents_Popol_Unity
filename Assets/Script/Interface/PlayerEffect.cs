using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_Effect
{
    public void PlayAttackEffect(string skill, float destroy);
    // public void PlayerMoveEffect();
}

public class PlayerEffect : MonoBehaviour, I_Effect
{
    GameObject effect;
    
    void Attackpos(GameObject effect)
    {
        Vector3 dir = transform.parent.transform.forward;
        effect.transform.position = transform.position;

        effect.transform.rotation = Quaternion.LookRotation(dir);
    }

    // public void PlayerMoveEffect()
    // {
    //     particle = GetComponent<ParticleSystem>();
    //     particle.Play();
    //     particle.Stop();
    // }

    public void PlayAttackEffect(string skill, float destroy)
    {
        effect = Instantiate<GameObject>(Resources.Load("Skill/Player/" + skill) as GameObject);
        Attackpos(effect);
    }
}
