public class StunStatusEffect : StatusEffect
{
    private IStun target;

    StunStatusEffect() : base("Stun") { }
    // Start is called before the first frame update
    private void Start()
    {
        LoadEffect();
        Initailize();

        if(target != null)
            target.GetStun();
    }

    private void OnDestroy()
    {
        if(target != null)
            target.OutStun();
    }

    protected override void Initailize()
    {
        target = GetComponentInParent<IStun>();
    }
}