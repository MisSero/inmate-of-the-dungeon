using UnityEngine;

public class SkillTheWall : SampleSkill
{
    public override string Name { get; } = "The Wall";

    public override string Description { get; } = "Hey you! +" + block + "% block";

    private readonly static int block = 50;

    public override void Effect()
    {
        if (ReferencesToObjects.Player is Warrior warrior)
            warrior.Block += block;
    }

    public override void LoadEffect()
    {
    }
}
