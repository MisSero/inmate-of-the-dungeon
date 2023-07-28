using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillExcellentProtection : SampleSkill
{
    public override string Name { get; } = "Excellent protection";

    public override string Description { get; } = "Ignoring " + ignorDamage + " damage";

    private readonly static int ignorDamage = 1;

    public override void Effect()
    {
        if (ReferencesToObjects.Player is Warrior warrior)
            warrior.IgnorDamage += ignorDamage;
    }

    public override void LoadEffect()
    {
        Effect();
    }
}
