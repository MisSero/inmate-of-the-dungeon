
public class ItemKnife : SampleItem
{
    public override string Name { get; } = "Knife";

    public override string Description { get; } = "It will kill. Attack + 50%";

    protected override int Cost { get; } = (int)ItemRarity.common;

    public override void Effect()
    {
        ReferencesToObjects.Player.Attack = (int)(ReferencesToObjects.Player.Attack * 1.5f);
    }

    public override void LoadEffect()
    {
    }
}
