
public class ItemParasite : SampleItem
{
    public override string Name { get; } = "Parasite";

    public override string Description { get; } = "Little parasite for you. Max HP * " + maxHpMultiply + ", Health potion utility / 2";

    protected override int Cost { get; } = (int)ItemRarity.rare;

    private readonly static float maxHpMultiply = 2.5f;

    public override void Effect()
    {
        ReferencesToObjects.Player.MaxHP = (int)(ReferencesToObjects.Player.MaxHP * maxHpMultiply);
        HpPotion.hpMultiply /= 2;
    }

    public override void LoadEffect()
    {
    }
}
