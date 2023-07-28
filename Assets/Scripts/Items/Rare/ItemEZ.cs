using UnityEngine;

public class ItemEZ : SampleItem
{
    public override string Name { get; } = "EZ";

    public override string Description { get; } = "EZ. Miss chance = 0, crit chance + 20%, attack * 1,5";

    protected override int Cost { get; } = (int)ItemRarity.rare;

    public override void Effect()
    {
        Player player = ReferencesToObjects.Player;
        player.MissChance = 0;
        player.CritChance += 20;
        player.Attack = Mathf.RoundToInt(player.Attack * 1.5f);
    }

    public override void LoadEffect()
    {
    }
}
