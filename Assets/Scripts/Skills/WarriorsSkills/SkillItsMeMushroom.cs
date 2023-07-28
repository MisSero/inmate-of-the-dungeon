using UnityEngine;

public class SkillItsMeMushroom : SampleSkill
{
    public override string Name { get; } = "It's a-me, Mushroom";

    public override string Description { get; } = "MaxHp *" + hpMultiply + ". Be careful, it increases the size and flips";

    private readonly static int hpMultiply = 3;

    public override void Effect()
    {
        ReferencesToObjects.Player.MaxHP *= hpMultiply;
        IncreasesAndFlips();
    }

    public override void LoadEffect()
    {
        IncreasesAndFlips();
    }

    private void IncreasesAndFlips()
    {
        SpriteRenderer spriteRenderer = ReferencesToObjects.Player.gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.drawMode = SpriteDrawMode.Sliced;
        spriteRenderer.size = new Vector2(1.5f, 1.5f);
        spriteRenderer.flipX = true;
        spriteRenderer.flipY = true;
    }
}
