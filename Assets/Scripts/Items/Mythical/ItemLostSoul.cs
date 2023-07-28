
public class ItemLostSoul : SampleItem
{
    public override string Name { get; } = "Lost Soul";

    public override string Description { get; } = "Gives " + numberOfSouls + " souls";

    protected override int Cost { get; } = (int)ItemRarity.mythical;

    private readonly static int numberOfSouls = 100 * ReferencesToObjects.LvlNumber;

    public override void Effect()
    {
        ReferencesToObjects.Player.TakeInfectedSouls(numberOfSouls);
    }

    public override void LoadEffect()
    {
    }
}
