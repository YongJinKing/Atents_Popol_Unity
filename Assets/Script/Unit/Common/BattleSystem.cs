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
    public uint HP;
    public uint Exp;
    public float Speed;
}
public interface IDamage
{
    public void TakeDamage(uint damage);
}

public class BattleSystem : CharacterProperty, IDamage
{
    [SerializeField] protected BattleStat battleStat;
    [SerializeField] protected BattleStat curBattleStat;
    protected float battleTime = 0.0f;
    public event UnityAction deathAlarm;
    Transform _target = null;

    public UnityEvent<Vector3, float> rotAct;
    protected virtual void Start()
    {
        Initialize();
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

    public uint AP
    {
        get
        {
            return this.curBattleStat.AP;
        }
        set
        {
            this.curBattleStat.AP = value;
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
        curBattleStat = battleStat;
        curBattleStat.HP = battleStat.maxHP;
    }
    public void TakeDamage(uint dmg)
    {
        curBattleStat.HP -= dmg;
        if (curBattleStat.HP <= 0.0f)
        {
            //Die
            OnDead();
            //myAnim.SetTrigger("DeathTrigger");
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

        rotAct?.Invoke(dir, Speed);

        //equipWeapon.Use();
        myAnim.SetTrigger("t_Attack");
    }

    protected virtual void OnDead()
    {
        deathAlarm?.Invoke();
        GetComponent<Collider>().enabled = false;
    }

    public bool IsLive()
    {
        return curBattleStat.HP > 0.0f;
    }
}