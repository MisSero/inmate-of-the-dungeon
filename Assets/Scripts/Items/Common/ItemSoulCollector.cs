
public class ItemSoulCollector : SampleItem
{
    public override string Name { get; } = "Soul Collector";

    public override string Description { get; } = "Number of souls received * 2";

    protected override int Cost { get; } = (int)ItemRarity.common;

    public override void Effect()
    {
        ReferencesToObjects.Player.GetSoulsMultiply *= 2;
    }

    public override void LoadEffect()
    {
        Effect();
    }
}
