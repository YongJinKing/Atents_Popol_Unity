using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


public class SkillManager : PlayerSkill
{
    public int index;
    public bool SimpleAttack;
    public int EnergyGage;
    public int Damage;
    public float SkillCalculation;
    public int WeaponType;
    public int Level;
    public float CoolTime;
    public bool CoolTimeCheck;// true : can use Skill, false : Can't use Skill
    Collider col;
    GameObject Player;
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
        OnBuff();
        
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Monster_Body"))
        {
            BattleSystem temp = other.GetComponentInParent<BattleSystem>();
            if (!target.Contains(temp))
            {
                Vector3 startPos = pl.transform.position + Vector3.up * 0.5f;
                Vector3 dir = other.bounds.center - startPos;
                Ray ray = new Ray(startPos - dir.normalized * 1.0f, dir);
                StartCoroutine(tempDebuging(ray));
                RaycastHit[] hits;
                if ((hits = Physics.RaycastAll(ray , col.bounds.extents.magnitude + 1.0f, 1 << LayerMask.NameToLayer("Monster_Body"))) != null)
                {
                    foreach(RaycastHit data in hits)
                    {
                        if (!target.Contains(data.collider.GetComponentInParent<BattleSystem>()))
                        {
                            target.Add(temp);
                            var plData = DataManager.instance.playerData;

                            switch (UnityEngine.Random.Range(0, 2))
                            {
                                case 0:
                                plData.Weapon_Duration--;
                                break;
                                case 1:
                                break;
                            }

                            if (plData.Weapon_Duration <= 0)
                            {
                                plData.Weapon_Duration = 0;
                            }

                            if(SimpleAttack)
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
                                inflict.GetCol(data.collider);
                            }

                            Debug.Log("Hit");

                            IDamage iDamage = data.collider.GetComponentInParent<IDamage>();
                            DefenceType dtype = data.collider.GetComponentInParent<IGetDType>().GetDType(data.collider);


                            if (iDamage != null)
                            {
                                Plm.totalDamege(data.collider, (int)pl.GetModifiedStat(E_BattleStat.ATK), Damage, aType, dtype, SkillCalculation);
                            }
                        }
                    }

                   
                    
                }
            }
        }
    }
    public int Buff;
    public int BTime;
    void OnBuff()
    {
        int RialATK = pl.ATK;
        pl.ATK += (int)(pl.ATK * (Buff/100));
        StartCoroutine(stopBuff(BTime, RialATK));
    }

    IEnumerator stopBuff(int BTime, int RialATK)
    {
        yield return new WaitForSeconds(BTime);
        pl.ATK = RialATK;
    }



    IEnumerator tempDebuging(Ray ray)
    {

        if (Physics.Raycast(ray, out RaycastHit hit, col.bounds.extents.magnitude, 1 << 15))
        {
            Debug.Log("Debug Hit");
        }

        float time = 5.0f;
        while(time > 0.0f)
        {
            time -= Time.deltaTime;
            Debug.DrawRay(ray.origin, ray.direction * (col.bounds.extents.magnitude + 1.0f), Color.red);
            yield return null;
        }
    }

    public void OnColliderBlink()
    {
        target.Clear();
    }
}
