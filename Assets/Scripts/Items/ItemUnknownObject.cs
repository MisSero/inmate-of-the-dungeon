
public class ItemUnknownObject : SampleItem
{
    public override string Name { get; } = "Unknown Object";

    public override string Description { get; } = "??? ?";

    protected override int Cost { get; } = (int)ItemRarity.unknownItem;

    public override void Effect()
    {
        Player player = ReferencesToObjects.Player;
        player.Attack += 1;
        player.MaxHP += 1;
        player.TakeExp(1);
    }

    public override void LoadEffect()
    {
    }
}
