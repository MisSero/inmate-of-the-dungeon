

public class ItemHealth : SampleItem
{
    public override string Name { get; } = "Health+";

    public override string Description { get; } = "Max HP * 1,5. Restores health";

    protected override int Cost { get; } = (int)ItemRarity.common;

    public override void Effect()
    {
        Player player = ReferencesToObjects.Player;
        player.MaxHP = (int)(player.MaxHP * 1.5f);
        player.ChangeHp(player.MaxHP);
    }

    public override void LoadEffect()
    {
    }
}
