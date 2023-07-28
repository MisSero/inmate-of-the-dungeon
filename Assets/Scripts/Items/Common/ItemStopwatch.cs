using UnityEngine;

public class ItemStopwatch : SampleItem
{
    public override string Name { get; } = "Stopwatch";

    public override string Description { get; } = "Cooldown/2";

    protected override int Cost { get; } = (int)ItemRarity.common;

    public override void Effect()
    {
        ReferencesToObjects.Player.Ultimate.TotalCooldown /= 2;
    }

    public override void LoadEffect()
    {
    }
}
