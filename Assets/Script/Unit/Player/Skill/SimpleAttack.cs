using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAttack : MonoBehaviour
{
    GameObject Player;
    Player pl;

    void Start()
    {
        Player = GameObject.Find("Player");
        pl = Player.GetComponent<Player>();
        Destroy(gameObject, 0.5f);
    }

    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        int Count = 0;

        if (other.gameObject.layer == LayerMask.NameToLayer("Monster_Body"))
        {
            if (Count <= 1)
            {
                pl.EnergyGage += (int)(pl.MaxEnergyGage / 10);
            }
            if (pl.EnergyGage > pl.MaxEnergyGage)
            {
                pl.EnergyGage = pl.MaxEnergyGage;
            }
        }
    }
}
