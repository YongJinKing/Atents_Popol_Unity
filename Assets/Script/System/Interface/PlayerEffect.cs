using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public interface I_Effect
{
    public void PlayAttackEffect(string skill);
}

public interface I_SimpleEffect
{
    public void SimpleEffect(string simpleSkill);
}

public class PlayerEffect : MonoBehaviour, I_Effect, I_SimpleEffect
{
    GameObject effect;
    
    void Attackpos(GameObject effect)
    {
        Vector3 dir = transform.parent.transform.forward;
        effect.transform.position = transform.position;

        effect.transform.rotation = Quaternion.LookRotation(dir);
    }

    public void PlayAttackEffect(string skill)
    {
        string Weaponpos = null;
        switch(DataManager.instance.playerData.WeaponType)
        {
            case 0:
                Weaponpos = "OneHandSwordSkill";
                break;
            case 1:
                Weaponpos = "TwoHandSwordSkill";
                break;
        }
        effect = Instantiate<GameObject>(Resources.Load($"Player/SkillEffect/{Weaponpos}/{skill}") as GameObject);
        Attackpos(effect);
    }

    public void SimpleEffect(string SimpleSkill)
    {
        effect = Instantiate<GameObject>(Resources.Load($"Player/Effect/{SimpleSkill}") as GameObject);
        Attackpos(effect);
    }


}
