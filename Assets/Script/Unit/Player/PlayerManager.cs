using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
   int totalDmg;
   public void totalDamege(Collider other, int Pldmg, int skillDamage)
   {
      IDamage iDamage = other.GetComponent<IDamage>();
      totalDmg = Pldmg * skillDamage;
      iDamage.TakeDamage(totalDmg);
   }
}