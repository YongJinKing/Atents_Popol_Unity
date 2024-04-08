
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct BattleStat
{
    public int Level;
    public int ATK;
    public int HP;
    public int Exp;
    public int MaxExp;
    public int EnergyGage;
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
    public UnityAction<int> deathAlarm;

    protected virtual void Start()
    {
        Initialize();
    }

    public BattleStat battlestat
    {
        get { return battleStat; }
        set { battleStat = value; }
    }
    public int MaxHP
    {
        get
        {
            return this.battleStat.HP;
        }
        set
        {
            this.battleStat.HP = value;
        }
    }
    
    public int MaxExp
    {
        get
        {
            return this.curBattleStat.MaxExp;
        }
        set
        {
            this.curBattleStat.MaxExp = value;
        }
    }
    public int EnergyGage
    {
        get
        {
            return this.curBattleStat.EnergyGage;
        }
        set
        {
            this.curBattleStat.EnergyGage = value;
        }
    }
    public int MaxEnergyGage
    {
        get
        {
            return this.battleStat.EnergyGage;
        }
        set
        {
            this.battleStat.EnergyGage = value;
        }
    }

    public float Speed
    {
        get
        {
            return this.curBattleStat.Speed;
        }
        set
        {
            this.curBattleStat.Speed = value;
        }
    }
    public int HP
    {
        get
        {
            return this.curBattleStat.HP;
        }
        set
        {
            if(value > MaxHP)
            {
                value = MaxHP;
            }
            this.curBattleStat.HP = value;
        }
    }
    public int Level
    {
        get
        {
            return this.curBattleStat.Level;
        }
        set
        {
            this.curBattleStat.Level = value;
        }
    }

    public int ATK
    {
        get
        {
            return this.curBattleStat.ATK;
        }
        set
        {
            this.curBattleStat.ATK = value;
        }
    }

    public int Exp
    {
        get
        {
            return this.curBattleStat.Exp;
        }
        set
        {
            this.curBattleStat.Exp = value;
        }
    }

    protected virtual void Initialize()
    {
        curBattleStat = battleStat;
        GameObject obj;
        if ((obj = GameObject.Find("GameManager")) != null)
        {
            deathAlarm += obj.GetComponent<GameManager>().OnGameEnd;
        }
    }
    
    public void TakeDamage(int dmg, AttackType Atype, DefenceType Dtype)
    {
        int totaldmg;
        float computed = ComputeCompatibility(Atype, Dtype);
        totaldmg = (int)((float)dmg * computed);
        Debug.Log("BattleSystem.TakeDamage");
        Debug.Log($"Atype : {Atype}, Dtype: {Dtype}");
        Debug.Log($"total : {totaldmg}");
        
        curBattleStat.HP -= totaldmg;
        if (curBattleStat.HP <= 0.0f)
        {
            //Die
            OnDead();
        }
    }

    private float ComputeCompatibility(AttackType Atype, DefenceType Dtype)
    {
        float computed = 1.0f;
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


    protected virtual void OnDead()
    {
    }
}