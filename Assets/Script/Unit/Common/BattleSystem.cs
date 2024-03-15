
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct BattleStat
{
    public int Level;
    public int AP;
    public int maxHP;
    public int HP;
    public int Exp;
    public float Speed;
    public float AttackDelay;
}
public interface IDamage
{
    public void TakeDamage(int damage, AttackType Atype, DefenceType Dtype);
}

public class BattleSystem : CharacterProperty, IDamage
{
    [SerializeField] protected BattleStat battleStat;
    [SerializeField] protected BattleStat curBattleStat;
    protected float battleTime = 0.0f;
    public event UnityAction deathAlarm;
    Transform _target = null;

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

    public int AP
    {
        get
        {
            //debuf [] temp = GetComponentinchildren()
            //int temp ap
            //for()
            //ap * TempAnimEvent dsa

            //return ap;
            return this.curBattleStat.AP;
        }
        set
        {
            this.curBattleStat.AP = value;
        }
    }

    public int Exp
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
    public void TakeDamage(int dmg, AttackType Atype, DefenceType Dtype)
    {
        int totaldmg;
        float computed = ComputeCompatibility(Atype, Dtype);
        totaldmg = (int)((float)dmg * computed);

        Debug.Log($"total : {totaldmg}");

        curBattleStat.HP -= totaldmg;
        if (curBattleStat.HP <= 0.0f)
        {
            //Die
            OnDead();
            //myAnim.SetTrigger("DeathTrigger");
        }
    }

    private float ComputeCompatibility(AttackType Atype, DefenceType Dtype)
    {
        float computed = 0;
        switch (Atype)
        {
            case AttackType.Slash:
                switch (Dtype)
                {
                    case DefenceType.Leather:
                        computed = 2.0f;
                        break;
                    case DefenceType.HeavyArmor:
                        computed = 0.5f;
                        break;
                    case DefenceType.CompositeArmor:
                        computed = 1.0f;
                        break;
                }
                break;
            case AttackType.Blunt:
                switch (Dtype)
                {
                    case DefenceType.Leather:
                        computed = 0.5f;
                        break;
                    case DefenceType.HeavyArmor:
                        computed = 2.0f;
                        break;
                    case DefenceType.CompositeArmor:
                        computed = 1.0f;
                        break;
                }
                break;
            case AttackType.Penetration:
                switch (Dtype)
                {
                    case DefenceType.Leather:
                        computed = 1.0f;
                        break;
                    case DefenceType.HeavyArmor:
                        computed = 0.5f;
                        break;
                    case DefenceType.CompositeArmor:
                        computed = 2.0f;
                        break;
                }
                break;
            default:
                computed = 1.0f;
                break;
        }

        return computed;
    }

    //For Player
    public void OnGetExp(int Exp)
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