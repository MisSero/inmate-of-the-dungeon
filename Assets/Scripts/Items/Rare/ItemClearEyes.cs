
public class ItemClearEyes : SampleItem
{
    public override string Name { get; } = "Clear eyes";

    public override string Description { get; } = "Increased visibility. When entering a level, enemies are marked.";

    protected override int Cost { get; } = (int)ItemRarity.rare;

    public override void Effect()
    {
        ReferencesToObjects.Player.ClearEyes = true;
        LoadEffect();
    }

    public override void LoadEffect()
    {
        ReferencesToObjects.MainCamera.orthographicSize += 1.5f;
    }
}
