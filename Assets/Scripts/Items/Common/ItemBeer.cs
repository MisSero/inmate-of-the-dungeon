
public class ItemBeer : SampleItem
{
    public override string Name { get; } = "Beer";

    public override string Description { get; } = "Attack * " + attackMultiply + ". Miss chance 40%";

    protected override int Cost { get; } = (int)ItemRarity.common;

    private readonly static int attackMultiply = 2;

    public override void Effect()
    {
        ReferencesToObjects.Player.Attack *= attackMultiply;
        ReferencesToObjects.Player.MissChance += 40;
    }

    public override void LoadEffect()
    {
    }
}
