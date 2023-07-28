using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSwordOfUpheaval : SampleItem
{
    public override string Name { get; } = "Sword of Upheaval";

    public override string Description { get; } = "Attack * " + attackMultiply;

    protected override int Cost { get; } = (int)ItemRarity.mythical;

    private readonly static int attackMultiply = 3;

    public override void Effect()
    {
        ReferencesToObjects.Player.Attack *= attackMultiply;
    }

    public override void LoadEffect()
    {
    }
}
