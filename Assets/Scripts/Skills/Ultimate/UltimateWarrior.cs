using UnityEngine;

public class UltimateWarrior : SamleUltimate
{
    public override int TotalCooldown { get; set; } = 120;

    public int Armor { get; set; } = 1;
    public static int ArmorMultiply { get; set; } = 1;

    private Warrior player;
    private GameObject ultimateAnimation;

    protected override void Start()
    {
        base.Start();
        player = ReferencesToObjects.Player as Warrior;
        ultimateAnimation = player.gameObject.transform.GetChild(0).gameObject;
    }
    protected override void Effect()
    {
        player.Armor += Armor * ArmorMultiply;
        ultimateAnimation.SetActive(true);
        player.UpdateCanvas();
    }

    private void OnDestroy()
    {
        ArmorMultiply = 1;
    }
}
