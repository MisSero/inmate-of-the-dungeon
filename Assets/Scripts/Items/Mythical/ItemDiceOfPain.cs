
public class ItemDiceOfPain : SampleItem
{
    public override string Name { get; } = "Dice of Pain";

    public override string Description { get; } = "Ability to reroll item";

    protected override int Cost { get; } = (int)ItemRarity.mythical;

    public override void Effect()
    {
        ReferencesToObjects.ItemController.DiceOfPain = true;
    }

    public override void LoadEffect()
    {
        Effect();
    }
}
