using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPhoenixFeather : SampleItem
{
    public override string Name { get; } = "Phoenix feather";

    public override string Description { get; } = "Rebirth with 50% hp";

    protected override int Cost { get; } = (int)ItemRarity.mythical;

    public override void Effect()
    {
        ReferencesToObjects.Player.PhoenixFeather = true;
    }

    public override void LoadEffect()
    {
    }
}
