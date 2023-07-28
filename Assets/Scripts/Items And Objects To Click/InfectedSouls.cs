using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectedSouls : ItemsAndObjectsToClick
{
    protected override void DestroyAnd()
    {
        base.DestroyAnd();
        player.TakeInfectedSouls(3 * ReferencesToObjects.LvlNumber);
    }
}
