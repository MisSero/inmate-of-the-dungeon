using UnityEngine;

public abstract class SampleSkill : MonoBehaviour
{
    public Sprite Icon;

    public abstract string Name { get;}

    public abstract string Description { get; }

    public abstract void Effect();

    public abstract void LoadEffect();
}
