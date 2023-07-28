
public class ItemScrollOfKnowledge : SampleItem
{
    public override string Name { get; } = "Scroll of Knowledge";

    public override string Description { get; } = "+ 1 level";

    protected override int Cost { get; } = (int)ItemRarity.rare;

    public override void Effect()
    {
        ReferencesToObjects.Player.LevelController.TakeExp(ReferencesToObjects.Player.LevelController.GetMaxExp());
    }

    public override void LoadEffect()
    {
    }
}
