
public class HpPotion : ItemsAndObjectsToClick
{
    public static float hpMultiply = 1; // при отключении всех обхектов ObjectKeeper.DisableObjects происходит преобразование к начальному значпению
    private int hpToHeal = 5;
    protected override void DestroyAnd()
    {
        base.DestroyAnd();
        player.ChangeHp((int)(hpToHeal * hpMultiply));
    }
}
