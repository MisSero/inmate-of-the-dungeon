
public class ItemBloodMagic : SampleItem
{
    public override string Name { get; } = "Blood Magic";

    public override string Description { get; } = "Using the ability restores health points";

    protected override int Cost { get; } = (int)ItemRarity.rare;

    public override void Effect()
    {
        ReferencesToObjects.Player.BloodMagic = true;
        ReferencesToObjects.Player.SubscribeUltimate();
    }

    public override void LoadEffect()
    {
        Effect();
    }
}
