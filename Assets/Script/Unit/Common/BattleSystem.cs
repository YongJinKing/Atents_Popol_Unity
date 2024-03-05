using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct BattleStat
{
    public uint Level;
    public uint AP;
    public uint maxHP;
    public uint Exp;
    public float Speed;
    public float AttackRange;
    public float AttackDelay;
}
public interface IDamage
{
    public void TakeDamage(uint damage);
}

public class BattleSystem : CharacterProperty, IDamage
{
    [SerializeField] protected BattleStat battleStat;
    [SerializeField] protected float curHP = 0.0f;
    protected float battleTime = 0.0f;
    public event UnityAction deathAlarm;
    Transform _target = null;

    public UnityEvent<Vector3, float> rotAct;
    protected virtual void Start()
    {
        curHP = battleStat.maxHP;
    }

    protected Transform myTarget
    {
        get => _target;
        set 
        {
            _target = value;
            if(_target != null)
            {
                BattleSystem bs = _target.GetComponent<BattleSystem>();
                if(bs != null)
                {
                    bs.deathAlarm += TargetDead;
                }
            }
        }
    }

    public uint Exp
    {
        get
        {
            return this.battleStat.Exp;
        }
        set
        {
            this.battleStat.Exp = value;
        }
    }

    void TargetDead()
    {
        StopAllCoroutines();
    }

    protected void Initialize()
    {
        curHP = battleStat.maxHP;
    }
    public void TakeDamage(uint dmg)
    {
        curHP -= dmg;
        if (curHP <= 0.0f)
        {
            //Die
            OnDead();
            myAnim.SetTrigger("DeathTrigger");
        }
        else
        {
            myAnim.SetTrigger("HitTrigger");
        }
    }

    //For Player
    public void OnGetExp(uint Exp)
    {
        this.Exp += Exp;
        if (this.Exp >= 100.0f)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        battleStat.Level++;
    }

    public void OnAttack(Vector3 target, Weapon equipWeapon, UnityAction endAct)
    {
        
        Vector3 dir = target - transform.position;
        float Speed = 2;

        rotAct?.Invoke(dir, Speed);

        equipWeapon.Use();
        myAnim.SetTrigger("t_Attack");

        StartCoroutine(Delay(endAct,0.1f));
    }

    public IEnumerator Delay(UnityAction act , float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        act?.Invoke();
    }

    protected virtual void OnDead()
    {
        deathAlarm?.Invoke();
        GetComponent<Collider>().enabled = false;
    }

    public bool IsLive()
    {
        return curHP > 0.0f;
    }
}