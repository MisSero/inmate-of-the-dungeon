using UnityEngine;

public class ItemCrackedHourglass : SampleItem
{
    public override string Name { get; } = "Cracked hourglass";

    public override string Description { get; } = "When HP = 0 you go back to the beginning of the level, Max HP = 1, attack * 5";

    protected override int Cost { get; } = (int)ItemRarity.mythical;

    public override void Effect()
    {
        ReferencesToObjects.Player.CrackedHourglass = true;
    }

    public override void LoadEffect()
    {
    }
}
