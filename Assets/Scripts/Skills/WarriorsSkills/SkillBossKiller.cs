using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBossKiller : SampleSkill
{
    public override string Name { get; } = "Boss Killer";

    public override string Description { get; } = "Boss damage * 4";

    public override void Effect()
    {
        if (ReferencesToObjects.Player is Warrior warrior)
            warrior.BossKiller = true;
    }

    public override void LoadEffect()
    {
        Effect();
    }
}
