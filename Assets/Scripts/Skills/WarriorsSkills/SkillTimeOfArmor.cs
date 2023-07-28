using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTimeOfArmor : SampleSkill
{
    public override string Name { get; } = "Time of armor";

    public override string Description { get; } = "Gives " + armor + " armor";

    private readonly static int armor = 30;

    public override void Effect()
    {
        if (ReferencesToObjects.Player is Warrior warrior)
            warrior.Armor += armor;
    }

    public override void LoadEffect()
    {
    }
}
