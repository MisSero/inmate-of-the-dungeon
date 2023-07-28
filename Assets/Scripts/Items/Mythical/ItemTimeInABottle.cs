
public class ItemTimeInABottle : SampleItem
{
    public override string Name { get; } = "Time In A Bottle";

    public override string Description { get; } = "There never seems to be enough time. Reduced ability cooldown for killing enemies * " +
        redeceCooldownMultiply;

    protected override int Cost { get; } = (int)ItemRarity.mythical;

    private readonly static int redeceCooldownMultiply = 3;

    public override void Effect()
    {
        ReferencesToObjects.Player.ReduceCooldownMultiply *= redeceCooldownMultiply;
    }

    public override void LoadEffect()
    {
        Effect();
    }
}
