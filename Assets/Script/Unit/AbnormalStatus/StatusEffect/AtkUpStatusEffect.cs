public class AtkUpStatusEffect : StatValueChangeStatusEffect
{
    public AtkUpStatusEffect() : base("AtkUp") { }

    protected override void Initailize()
    {
        type = E_BattleStat.ATK;
        value = 1.5f;
    }
}
