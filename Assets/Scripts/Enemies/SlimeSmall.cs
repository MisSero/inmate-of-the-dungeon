using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSmall : Enemy
{
    protected override void Start()
    {
        base.Start();
        HealthPoints = 5;
        Attack = 1;
        mySouls = 2;
        myExp = 1;
    }
}
