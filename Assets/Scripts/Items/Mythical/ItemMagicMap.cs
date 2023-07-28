
public class ItemMagicMap : SampleItem
{
    public override string Name { get; } = "Magic Map";

    public override string Description { get; } = "When entering a level, the map is open";

    protected override int Cost { get; } = (int)ItemRarity.mythical;

    public override void Effect()
    {
        ReferencesToObjects.Player.MagicMap = true;
    }

    public override void LoadEffect()
    {
        Effect();
    }
}
