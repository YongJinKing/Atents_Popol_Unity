public class DotHealStatusEffect : DotDamageStatusEffect
{
    public DotHealStatusEffect() : base("Heal") { }
    protected override void Initailize()
    {
        dotRate = 1.0f;
        isHeal = true;
    }
}
