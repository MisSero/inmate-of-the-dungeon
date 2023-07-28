using UnityEngine;

public  class SkillDoubleArmor : SampleSkill
{
    public override string Name { get; } = "Double Armor";
    public override string Description { get; } = "You will receive double armor";

    public override void Effect()
    {
        UltimateWarrior.ArmorMultiply *= 2;
    }
    public override void LoadEffect()
    {
        Effect();
    }
}
