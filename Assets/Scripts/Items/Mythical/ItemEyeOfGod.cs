using UnityEngine;

public class ItemEyeOfGod : SampleItem
{
    public override string Name { get; } = "Eye of God";

    public override string Description { get; } = "When entering a level, enemies are marked. Attach * 3, impairs visibility";

    protected override int Cost { get; } = (int)ItemRarity.mythical;

    public override void Effect()
    {
        ReferencesToObjects.Player.Attack *= 3;
    }

    public override void LoadEffect()
    {
        ReferencesToObjects.Player.EyeOfGod = true;
        ReferencesToObjects.MainCamera.orthographicSize -= 1.5f;
        ReferencesToObjects.MainCamera.transform.rotation = Quaternion.Euler(0, 0, 15);
    }
}
