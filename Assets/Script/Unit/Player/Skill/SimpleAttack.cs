using UnityEngine;
public class SimpleAttack : MonoBehaviour
{
    
    GameObject Player;
    Player pl;

    void Start()
    {
        Player = GameObject.Find("Player");
        pl = Player.GetComponent<Player>();
    }

    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.layer == LayerMask.NameToLayer("Monster_Body"))
        {
            pl.EnergyGage += (int)(pl.MaxEnergyGage * 0.05);
            
            if (pl.EnergyGage > pl.MaxEnergyGage)
            {
                pl.EnergyGage = pl.MaxEnergyGage;
            }

            pl.EnergyGageCal();
        }
    }
}
