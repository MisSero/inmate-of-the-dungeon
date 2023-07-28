
public class ItemNeedForEvolution : SampleItem
{
    public override string Name { get; } = "Need for Evolution";

    public override string Description { get; } = "Increase Max HP for kills";

    protected override int Cost { get; } = (int)ItemRarity.rare;

    public override void Effect()
    {
        ReferencesToObjects.Player.NeedForEvolution = true;
    }

    public override void LoadEffect()
    {
        Effect();
    }
}
