using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSimpleBook : SampleItem
{
    public override string Name { get; } = "A simple book?";

    public override string Description { get; } = "EXP multipllier * " + expMultiply;

    protected override int Cost { get; } = (int)ItemRarity.common;

    private readonly static float expMultiply = 1.5f;

    public override void Effect()
    {
        ReferencesToObjects.Player.LevelController.ExpMultimply *= expMultiply;
    }

    public override void LoadEffect()
    {
        Effect();
    }
}
