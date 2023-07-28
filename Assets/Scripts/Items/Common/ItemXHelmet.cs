using UnityEngine;
using UnityEngine.UI;

public class ItemXHelmet : SampleItem
{
    [SerializeField] Image xShapedImage;
    public override string Name { get; } = "X-Helmet";

    public override string Description { get; } = "Max HP * 3, restores HP. X-shaped visibility limiter";

    protected override int Cost { get; } = (int)ItemRarity.common;

    public override void Effect()
    {
        Player player = ReferencesToObjects.Player;
        player.MaxHP *= 3;
        player.ChangeHp(player.MaxHP);
    }

    public override void LoadEffect()
    {
        Instantiate(xShapedImage, ReferencesToObjects.Canvas.transform);
    }
}
