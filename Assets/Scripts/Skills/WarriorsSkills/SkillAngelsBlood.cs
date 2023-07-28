using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAngelsBlood : SampleSkill
{
    public override string Name { get; } = "Angel's blood";

    public override string Description { get; } = "Max HP * 2";

    public override void Effect()
    {
        ReferencesToObjects.Player.MaxHP *= 2;
    }

    public override void LoadEffect()
    {
        Effect();
    }
}
