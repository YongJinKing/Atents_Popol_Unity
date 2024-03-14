
using System.Text;
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
    public void TakeDamage(int damage);
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
    public void TakeDamage(int dmg)
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