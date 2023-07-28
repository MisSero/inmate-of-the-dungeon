using UnityEngine;

public class ItemRainbowInAJar : SampleItem
{
    public override string Name { get; } = "Rainbow in a jar";

    public override string Description { get; } = "A little bit of everything";

    protected override int Cost { get; } = (int)ItemRarity.mythical;

    public override void Effect()
    {
        Player player = ReferencesToObjects.Player;
        player.MaxHP = Mathf.RoundToInt(player.MaxHP * 1.2f);
        player.Attack = Mathf.RoundToInt(player.Attack * 1.2f);
        player.Vampirism += 0.1f;
        player.CritChance += 10;
        player.TakeInfectedSouls(10 * ReferencesToObjects.LvlNumber);
        player.TakeExp(5 * ReferencesToObjects.LvlNumber);
        if (player.MissChance >= 3)
            player.MissChance -= 3;
    }

    public override void LoadEffect()
    {
    }
}
