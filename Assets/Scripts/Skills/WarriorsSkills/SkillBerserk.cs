using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBerserk : SampleSkill
{
    public override string Name { get; } = "Berserk";

    public override string Description { get; } = "Attack * " + attackMultiply + ". Less HP = more attack. Attack modifiers stop working correctly*";

    private readonly static float attackMultiply = 1.5f;

    public override void Effect()
    {
        if (ReferencesToObjects.Player is Warrior warrior)
        {
            warrior.FixedAttack = (int)(warrior.Attack * attackMultiply);
            warrior.Berserk = true;
            warrior.BerserkMode();
        }
    }

    public override void LoadEffect()
    {
        if (ReferencesToObjects.Player is Warrior warrior)
            warrior.Berserk = true;
    }
}
