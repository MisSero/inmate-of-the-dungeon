using UnityEngine;

public abstract class SampleItem : MonoBehaviour
{
    public Sprite Icon;

    public abstract string Name { get; }

    public abstract string Description { get; }
    protected abstract int Cost { get; }

    public abstract void Effect();

    public abstract void LoadEffect();

    public int GetCost()
    {
        return Cost * ReferencesToObjects.LvlNumber;
    }
}

enum ItemRarity
{
    unknownItem = 1,
    common = 5,
    rare = 10,
    mythical = 15,
}
