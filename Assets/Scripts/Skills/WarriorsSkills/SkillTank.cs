using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTank : SampleSkill
{
    public override string Name { get; } = "Tank";

    public override string Description { get; } = "MaxHp*2. More MaxHp = more + to your attack";

    public override void Effect()
    {
        Player player = ReferencesToObjects.Player;
        player.MaxHP *= 2;
        player.Attack += player.MaxHP / 4;
    }

    public override void LoadEffect()
    {
    }
}
