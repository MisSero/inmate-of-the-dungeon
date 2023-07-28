using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCompass : SampleItem
{
    public override string Name { get; } = "Compass";

    public override string Description { get; } = "Shows the exit when entering the level";

    protected override int Cost { get; } = (int)ItemRarity.common;

    public override void Effect()
    {
        ReferencesToObjects.Player.Compass = true;
    }

    public override void LoadEffect()
    {
        Effect();
    }
}
