using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHourglass : SampleItem
{
    public override string Name { get; } = "Hourglass";

    public override string Description { get; } = "When HP = 0 you go back to the beginning of the level, Max HP and attack * 1,5";

    protected override int Cost { get; } = (int)ItemRarity.mythical;


    public override void Effect()
    {
        ReferencesToObjects.Player.Hourglass = true;
    }

    public override void LoadEffect()
    {
    }
}
