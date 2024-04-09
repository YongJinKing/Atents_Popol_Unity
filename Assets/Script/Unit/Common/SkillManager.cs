using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class SkillManager : PlayerSkill
{
    public int EnergyGage;
    public int Damage;
    public float SkillCalculation;
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
                Ray ray = new Ray(col.bounds.center, other.ClosestPoint(col.bounds.center) - col.bounds.center);
                StartCoroutine(tempDebuging(ray));
                if (Physics.Raycast(ray, out RaycastHit hit , 1000.0f, 1 << LayerMask.NameToLayer("Monster_Body")))
                {
                    DataManager.instance.playerData.Weapon_Duration -= 1;
                    target.Add(temp);
                    Debug.Log("Hit");
                    IDamage iDamage = hit.collider.GetComponentInParent<IDamage>();
                    DefenceType dtype = hit.collider.GetComponentInParent<IGetDType>().GetDType(hit.collider);


                    if (iDamage != null)
                    {
                        Plm.totalDamege(hit.collider, pl.ATK, Damage, aType, dtype, SkillCalculation);
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

        if (Physics.Raycast(ray, out RaycastHit hit, 1000.0f, 1 << 15))
        {
            Debug.Log("Debug Hit");
        }

        float time = 2.0f;
        while(time > 0.0f)
        {
            time -= Time.deltaTime;
            Debug.DrawRay(ray.origin, ray.direction * 1000f, Color.red);
            yield return null;
        }
    }

    
}
