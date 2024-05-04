using System.Collections.Generic;
using UnityEngine;


public class SkillManager : PlayerSkill
{
    public bool SimpleAttack;
    public bool isBoxCol;
    public int EnergyGage;
    public int Damage;
    public float SkillCalculation;
    public int WeaponType;
    public int Level;
    public float CoolTime;
    public bool CoolTimeCheck;// true : can use Skill, false : Can't use Skill
    Collider col;
    public GameObject Player;
    Player pl;
    PlayerManager Plm;
    public AttackType aType;

    private HashSet<BattleSystem> target;

    void Start()
    {
        target = new HashSet<BattleSystem>();
        col = GetComponent<Collider>();
        Player = GameObject.Find("Player");
        Plm = Player.GetComponent<PlayerManager>();
        pl = Player.GetComponent<Player>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Monster_Body"))
        {
            Collider[] tempcol;
            if(isBoxCol)
            {
                tempcol = Physics.OverlapBox(col.bounds.center, col.bounds.extents, col.transform.rotation,1 << 12);
            }
            else
            {
                tempcol = Physics.OverlapSphere(col.bounds.center, col.bounds.extents.x, 1 << 12);
            }
            int min = 0;
            for (int i = 0; i < tempcol.Length; ++i)
            {
                min = i;
                for(int j = i; j < tempcol.Length; ++j)
                {
                    if((pl.transform.position - tempcol[j].bounds.center).sqrMagnitude < (pl.transform.position - tempcol[min].bounds.center).sqrMagnitude)
                    {
                        min = j;
                    }
                }
                Collider temp = tempcol[i];
                tempcol[i] = tempcol[min];
                tempcol[min] = temp;
            }

            foreach(Collider data in tempcol)
            {
                Debug.Log($"distance : {(pl.transform.position - data.bounds.center).sqrMagnitude} \nname : {data.name}");
            }

            for (int i = 0; i < tempcol.Length; i++)
            {
                BattleSystem temp = tempcol[i].GetComponentInParent<BattleSystem>();

                if (!target.Contains(temp))
                {
                    target.Add(temp);

                    var plData = DataManager.instance.playerData;

                    switch (UnityEngine.Random.Range(0, 11))
                    {
                        case 0:
                            plData.Rigging_Weapon_Duration--;
                            break;
                        default:
                            break;
                    }

                    if (plData.Rigging_Weapon_Duration <= 0)
                    {
                        plData.Rigging_Weapon_Duration = 0;
                    }

                    if (SimpleAttack)
                    {
                        pl.EnergyGage += (int)(pl.MaxEnergyGage * 0.05);

                        if (pl.EnergyGage > pl.MaxEnergyGage)
                        {
                            pl.EnergyGage = pl.MaxEnergyGage;
                        }
                        pl.EnergyGageCal();
                    }
                    pl.WeaponDurability();

                    InflictDeBuff inflict = GetComponent<InflictDeBuff>();
                    if (inflict != null)
                    {
                        inflict.GetCol(tempcol[i]);
                    }

                    IDamage iDamage = tempcol[i].GetComponentInParent<IDamage>();
                    DefenceType dtype = tempcol[i].GetComponentInParent<IGetDType>().GetDType(tempcol[i]);


                    if (iDamage != null)
                    {
                        Plm.totalDamege(tempcol[i], (int)pl.GetModifiedStat(E_BattleStat.ATK), Damage, aType, dtype, SkillCalculation);
                    }
                }
            }
        }
    }

    public void OnColliderBlink()
    {
        target.Clear();
    }
}
