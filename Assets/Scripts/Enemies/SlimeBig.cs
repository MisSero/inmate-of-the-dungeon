using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBig : Enemy
{
    protected override void Start()
    {
        base.Start();
        HealthPoints = 13;
        Attack = 3;
        mySouls = 5;
        myExp = 3;
    }
}