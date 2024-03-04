using System.Collections;
using System.Collections.Generic;
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
    UnitMovement Movement;
    [SerializeField] protected BattleStat battleStat;
    [SerializeField] protected float curHP = 0.0f;
    protected float battleTime = 0.0f;
    public event UnityAction deathAlarm;
    Coroutine rotate = null;
    Transform _target = null;

    protected virtual void Awake()
    {
        Movement = GetComponent<UnitMovement>();
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

    public void OnAttack(Vector3 target, Weapon equipWeapon)
    {
        
        Vector3 dir = target - transform.position;
        float Speed = 2;
       
        equipWeapon.Use();
        myAnim.SetTrigger("Attack");

        if(rotate != null) StopCoroutine(rotate);
        rotate = StartCoroutine(Movement.Rotating(dir, Speed));
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