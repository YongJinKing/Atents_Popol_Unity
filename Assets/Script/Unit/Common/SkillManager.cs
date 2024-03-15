using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkillManager : PlayerSkill
{
    public int Damage;
    GameObject Player;
    Player pl;
    PlayerManager Plm;

    void Start()
    {
        Player = GameObject.Find("Player");
        Plm = Player.GetComponent<PlayerManager>();
        pl = Player.GetComponent<Player>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Monster_Body"))
        {
            IDamage iDamage = other.GetComponent<IDamage>();
            if(iDamage != null)
            {
                Plm.totalDamege(other, pl.AP, Damage);
            }
        }
    }
}
