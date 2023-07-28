using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMidl : Enemy
{
    protected override void Start()
    {
        base.Start();
        HealthPoints = 7;
        Attack = 2;
        mySouls = 3;
        myExp = 2;
    }
}
