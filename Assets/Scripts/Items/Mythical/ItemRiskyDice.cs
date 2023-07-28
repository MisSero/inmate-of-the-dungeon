using UnityEngine;

public class ItemRiskyDice : SampleItem
{
    public override string Name { get; } = "Risky Dice";

    public override string Description { get; } = "¹25. Attact * 0.5, 1, 2, 2.5, 3, 3.5 or 4. 10% chance to die";

    protected override int Cost { get; } = (int)ItemRarity.mythical;

    private float[] attackMultiply = new float[7] { 0.5f, 1f, 2f, 2.5f, 3f, 3.5f, 4f };

    public override void Effect()
    {
        Player player = ReferencesToObjects.Player;
        System.Random rand = ReferencesToObjects.Rand;
        player.Attack = Mathf.RoundToInt(player.Attack * attackMultiply[rand.Next(attackMultiply.Length)]);

        if (rand.Next(100) <= 9)
            player.Die();
    }

    public override void LoadEffect()
    {
    }
}
