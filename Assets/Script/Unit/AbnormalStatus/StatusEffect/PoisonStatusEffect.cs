using System.Collections;
using UnityEngine;

public class PoisonStatusEffect : StatusEffect
{
    private BattleSystem target;
    private int dmg;
    public PoisonStatusEffect() : base("Poison") { }

    private void Start()
    {
        GameObject effect = LoadEffect();
        effect.transform.SetParent(transform, false);

        target = GetComponentInParent<BattleSystem>();
        dmg = target.MaxHP / 200;
        if (dmg <= 0)
            dmg = 1;

        StartCoroutine(Poisoning());
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private IEnumerator Poisoning()
    {
        float temp = 0;
        while (true)
        {
            temp += dmg * Time.deltaTime;
            if(temp > 1)
            {
                target.TakeDamage((int)temp, AttackType.Normal, DefenceType.Normal);
                temp = 0;
            }
            yield return null;
        }
    }
}
