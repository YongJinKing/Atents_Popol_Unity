using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TempPlayerAnimEvent : MonoBehaviour
{
    public LayerMask enemyMask;
    public Transform myAttackPoint;
    public UnityEvent<int> End;
    GameObject Effectobj;
    I_Effect i_Effect;
    I_SimpleEffect i_SimpleEffect;
    void Awake()
    {
        Effectobj = transform.parent.GetComponent<Player>().Effectobj;
        i_Effect = Effectobj.GetComponent<I_Effect>();
        i_SimpleEffect = Effectobj.GetComponent<I_SimpleEffect>();
    }
    public void OnEnd(int i)
    {
        End?.Invoke(i);
    }


    public void OnAttckEffect(string s)
    {
        i_Effect.PlayAttackEffect(s);
    }

    public void SimpleAttack(string skill)
    {
        i_SimpleEffect.SimpleEffect(skill);
    }

    public void OneHendSwordAttackSound()
    {
        SoundManager.instance.PlaySfxMusic("OneHandSwordAttack");
    }
    public void OneHendSwordSkillSound()
    {
        SoundManager.instance.PlaySfxMusic("OneHandSwordSkill");
    }
    public void TwoHendSwordAttackSound(string skillName)
    {
        SoundManager.instance.PlaySfxMusic(skillName);
    }
}
