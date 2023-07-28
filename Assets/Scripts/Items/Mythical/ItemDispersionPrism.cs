
public class ItemDispersionPrism : SampleItem
{
    public override string Name { get; } = "Dispersion Prism";

    public override string Description { get; } = "Improves random individual character stats";

    protected override int Cost { get; } = (int)ItemRarity.mythical;

    public override void Effect()
    {
        Player player = ReferencesToObjects.Player;
        int randomNumber = ReferencesToObjects.Rand.Next(1, 101);
        if (player is Warrior warrior)
        {
            if (randomNumber <= 45)
                warrior.Block += 20;
            else if (randomNumber <= 90)
                warrior.Armor += 25;
            else
                ImproveStandartStats();
        }
        else
            ImproveStandartStats();
    }

    public override void LoadEffect()
    {
    }
    private void ImproveStandartStats()
    {
        Player player = ReferencesToObjects.Player;
        player.Attack = (int)(player.Attack * 1.5f);
        player.MaxHP = (int)(player.MaxHP * 1.5f);
        player.Vampirism += 0.1f;
    }
}
