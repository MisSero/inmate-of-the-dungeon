using UnityEngine;

public class SkillWhirlwindOfAnger : SampleSkill
{
    public override string Name { get; } = "Whirlwind of anger";

    public override string Description { get; } = "Damage to everyone around";

    public override void Effect()
    {
        if (ReferencesToObjects.Player is Warrior warrior)
            warrior.WhirlwindOfAnger = true;
    }

    public override void LoadEffect()
    {
        Effect();
    }
}
