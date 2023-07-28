using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mimic : Enemy
{
    protected override void Start()
    {
        base.Start();
        HealthPoints = 6;
        Attack = 3;
        chance = 95;
        mySouls = 6;
        myExp = 5;
        DamagePlayer();
        gameObject.GetComponentInChildren<EnemyUI>().enabled = true;
    }
}
