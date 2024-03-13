using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_Effect
{
    public void PlayAttackEffect(Vector3 StartPos);
    public void PlaySkillEffect(Vector3 StartPos);
}

public class DefaultEffect : MonoBehaviour, I_Effect
{
    public void PlayAttackEffect(Vector3 StartPos)
    {
        GameObject effect = Instantiate<GameObject>(Resources.Load("Skill/Player/SimpleAttack") as GameObject);
        I_ClickPoint i_click = GetComponentInParent<I_ClickPoint>();
        Vector3 dir = i_click.GetRaycastHit();

        effect.transform.position = StartPos;

        effect.transform.rotation = Quaternion.LookRotation(dir);
        Destroy(effect.gameObject, 0.5f);
    }

    public void PlaySkillEffect(Vector3 StartPos)
    {
        GameObject effect = Instantiate<GameObject>(Resources.Load("Skill/Player/Slash") as GameObject);
        I_ClickPoint i_click = GetComponentInParent<I_ClickPoint>();
        Vector3 dir = i_click.GetRaycastHit();

        effect.transform.position = StartPos;

        effect.transform.rotation = Quaternion.LookRotation(dir);
        Destroy(effect.gameObject, 4.0f);
    }

}
