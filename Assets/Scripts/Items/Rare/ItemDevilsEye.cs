using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDevilsEye : SampleItem
{
    public override string Name { get; } = "Devil's eye";

    public override string Description { get; } = "Attack * " + attackMultiply + ". Chance to attack a random enemy instead of the target";

    protected override int Cost { get; } = (int)ItemRarity.rare;

    private readonly static float attackMultiply = 2.5f;

    public override void Effect()
    {
        ReferencesToObjects.Player.Attack = (int)(ReferencesToObjects.Player.Attack * attackMultiply);
        LoadEffect();
    }

    public override void LoadEffect()
    {
        ReferencesToObjects.Player.DevilsEye = true;
    }
}
