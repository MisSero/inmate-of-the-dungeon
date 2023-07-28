using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillThorns : SampleSkill
{
    public override string Name { get; } = "Thorns";

    public override string Description { get; } = "When enemies attack you they take damage";

    public override void Effect()
    {
        if (ReferencesToObjects.Player is Warrior warrior)
            warrior.Thorns = true;
    }

    public override void LoadEffect()
    {
        Effect();
    }
}
