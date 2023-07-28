using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : ItemsAndObjectsToClick
{
    [SerializeField] private GameObject mimic;
    private GameObject[] itemsToDrop;
    protected override void Start()
    {
        base.Start();
        itemsToDrop = ReferencesToObjects.ItemsToDrop;
    }

    protected override void OnMouseDown()
    {
        if (CheckPosition())
        {
            gameObject.GetComponent<Animator>().SetTrigger("OpenChest");
        }
    }
    // Метод уничтожающи объект(вызывается из анимации)
    protected override void DestroyAnd()
    {
        base.DestroyAnd();
        if (Random.Range(1, 101) <= 5)
            Instantiate(mimic, thisTransform.parent);
        else
            Instantiate(itemsToDrop[Random.Range(0, itemsToDrop.Length)], thisTransform.parent);
    }
}
