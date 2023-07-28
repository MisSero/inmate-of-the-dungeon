using System.Collections.Generic;

public class ItemBlackBox : SampleItem
{
    public override string Name { get; } = "Black Box";

    public override string Description { get; } = "Attack, Max HP or crit chance * 4, other stats / 2";

    protected override int Cost { get; } = (int)ItemRarity.rare;

    public override void Effect()
    {
        Player player = ReferencesToObjects.Player;
        List<int> statsList = new List<int>()
        {
            player.Attack,
            player.MaxHP,
            player.CritChance
        };

        int randNumber = ReferencesToObjects.Rand.Next(statsList.Count);
        statsList[randNumber] *= 4;
        statsList.RemoveAt(randNumber);
        for (int i = 0; i < statsList.Count; i++)
        {
            statsList[i] /= 2;
        }
    }

    public override void LoadEffect()
    {
    }
}
