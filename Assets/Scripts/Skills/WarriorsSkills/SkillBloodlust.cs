using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBloodlust : SampleSkill
{
    public override string Name { get; } = "Bloodlust";

    public override string Description { get; } = "Increase attack for kills";

    public override void Effect()
    {
        if (ReferencesToObjects.Player is Warrior warrior)
            warrior.Bloodlust = true;
    }

    public override void LoadEffect()
    {
        Effect();
    }
}
