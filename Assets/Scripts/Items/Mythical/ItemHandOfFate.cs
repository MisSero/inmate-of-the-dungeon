
public class ItemHandOfFate : SampleItem
{
    public override string Name { get; } = "Hand of Fate";

    public override string Description { get; } = "Allows you to rerole abilities";

    protected override int Cost { get; } = (int)ItemRarity.mythical;

    public override void Effect()
    {
        ReferencesToObjects.Player.HandOfFate = true;
    }

    public override void LoadEffect()
    {
        Effect();
    }
}
