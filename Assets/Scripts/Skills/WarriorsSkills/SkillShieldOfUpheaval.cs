using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillShieldOfUpheaval : SampleSkill
{
    public override string Name { get; } = "Shield of upheaval";

    public override string Description { get; } = "+" + block + "% block";

    private readonly static int block = 20;
    public override void Effect()
    {
        if (ReferencesToObjects.Player is Warrior warrior)
            warrior.Block += block;
    }

    public override void LoadEffect()
    {
    }
}
