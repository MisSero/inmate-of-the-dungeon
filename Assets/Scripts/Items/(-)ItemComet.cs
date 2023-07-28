using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemComet : SampleItem
{
    public override string Name { get; } = "Comet";

    public override string Description { get; } = "Launches a comet on a random enemy when using the ability";

    protected override int Cost { get; } = (int)ItemRarity.mythical;

    private Player player;
    private List<GameObject> enemies;
    private System.Random rand;

    public override void Effect()
    {
        player = ReferencesToObjects.Player;
        rand = ReferencesToObjects.Rand;
        //player.Ultimate.EffectNotify += LaunchComet;
    }

    public override void LoadEffect()
    {
        Effect();
    }

    private void LaunchComet()
    {
        enemies = player.Enemies;

    }

    private GameObject FindEnemy(List<GameObject> enemies)
    {
        GameObject enemy = enemies[rand.Next(enemies.Count)];
        if(enemy != null)
        {
            return null;
        }
        return null;
    }
}
