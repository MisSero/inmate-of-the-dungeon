
public class ItemMedicalCare : SampleItem
{
    public override string Name { get; } = "Medical care";

    public override string Description { get; } = "Health potion utility * 2";

    protected override int Cost { get; } = (int)ItemRarity.common;

    public override void Effect()
    {
        HpPotion.hpMultiply *= 2;
    }

    public override void LoadEffect()
    {
    }
}
