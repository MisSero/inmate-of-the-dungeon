using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEtherealSword : SampleItem
{
    public override string Name { get; } = "Ethereal sword";

    public override string Description { get; } = "Attack * " + attackmultiply;
    protected override int Cost { get; } = (int)ItemRarity.common;

    private static readonly float attackmultiply = 1.5f;


    public override void Effect()
    {
        ReferencesToObjects.Player.Attack = (int)(ReferencesToObjects.Player.Attack * attackmultiply);
    }

    public override void LoadEffect()
    {
    }
}
