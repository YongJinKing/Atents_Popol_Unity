public class PoisonStatusEffect : DotDamageStatusEffect
{
    Player pl;
    public PoisonStatusEffect() : base("Poison") { }
    protected override void Start()
    {
        base.Start();
        pl = GetComponentInParent<Player>();
        if (pl != null)
            pl.DeBuffScr.transform.GetChild(3).gameObject.SetActive(true);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        if (pl != null )
            pl.DeBuffScr.transform.GetChild(3).gameObject.SetActive(false);
    }
    protected override void Initailize()
    {
        dotRate = 0.5f;
        isHeal = false;
    }
}
