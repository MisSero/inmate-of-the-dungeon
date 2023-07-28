using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillVampire : SampleSkill
{
    public override string Name { get; } = "Vampire";

    public override string Description { get; } = "+ " + vampirism * 100 + "% vampirism, attack * " + attackMultiply;

    private readonly static float vampirism = 0.3f;
    private readonly static float attackMultiply = 1.2f;

    public override void Effect()
    {
        Player player = ReferencesToObjects.Player;
        player.Vampirism += vampirism;
        player.Attack = (int)(player.Attack * attackMultiply);
    }

    public override void LoadEffect()
    {
    }
}
