using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Warrior : Player
{
    [SerializeField] private Text textArmor;
    public int Armor { get; set; }
    public int Block { get; set; }
    //Скиллы:
    // Включение шипов
    public bool Thorns { get; set; }
    // Игнорирование урона
    public int IgnorDamage { get; set; }
    // Включение вихря гнева
    public bool WhirlwindOfAnger { get; set; }
    // Включение bosskiller
    public bool BossKiller { get; set; }
    // Включение berserk
    public bool Berserk { get; set; }
    public int FixedAttack { get; set; }
    // Включение bloodlust(вызывается из enemy и там же проверяется на колво врагов)
    public bool Bloodlust { get; set; }
    public float BloodlustMultiply { get; } = 1.3f;
    

    protected override void Start()
    {
        MaxHP = PlayerPrefs.GetInt("WarriorMaxHP");
        HealthPoints = MaxHP;
        Attack = PlayerPrefs.GetInt("WarriorAttack");
        
        UpdateCanvas();
        base.Start();

        //GetSavedData();
    }

    public override void AttackEnemy(Enemy enemy, Vector3 positionEnemy)
    {
        bool isBoss = false;
        if(BossKiller && enemy is Boss)
        {
            isBoss = true;
            Attack *= 4;
        }
        base.AttackEnemy(enemy, positionEnemy);
        if (Thorns)
            enemy.ChangeHp(-enemy.Attack);
        if (WhirlwindOfAnger)
        {
            Vector2 myPosition = gameObject.transform.position;
            List<Vector2> positionAround = new List<Vector2>()
            {
                new Vector2(myPosition.x + 1,myPosition.y),
                new Vector2(myPosition.x - 1,myPosition.y),
                new Vector2(myPosition.x,myPosition.y + 1),
                new Vector2(myPosition.x,myPosition.y - 1)
            };
            foreach (Vector2 expectedPosition in positionAround)
            {
                if (expectedPosition != (Vector2)positionEnemy)
                {
                    foreach (GameObject item in Enemies)
                    {
                        if(item != null)
                        {
                            Vector2 enemyPosition = item.transform.position;
                            if (enemyPosition == expectedPosition)
                            {
                                Enemy itemEnemy = item.GetComponent<Enemy>();
                                itemEnemy.Attacted = true;
                                itemEnemy.ChangeHp(-Attack);
                                CheckPositionForTrail(myPosition, enemyPosition, false);
                                if (item != null)
                                    itemEnemy.Attacted = false;
                            }
                        }
                    }
                }
            }
            if (isBoss)
                Attack /= 4;
        }
    }

    public override void ChangeHp(int howMuch)
    {
        // Проверка и выполнение игнорирования урона
        if (howMuch < 0)
            howMuch += IgnorDamage;

        // проверка на наличие брони  и  (else) удар по броне
        if(Armor <= 0 || howMuch >= 0)
            base.ChangeHp(howMuch);
        else
        {
            int tempHowMuch = howMuch + Armor;
            Armor += howMuch;
            if (Armor < 0)
                Armor = 0;
            if (tempHowMuch > 0)
                tempHowMuch = 0;
            base.ChangeHp(tempHowMuch);
        }
            BerserkMode();
    }
    public override void UpdateCanvas()
    {
        base.UpdateCanvas();
        textArmor.text = Armor.ToString();
    }

    public void BerserkMode()
    {
        if (Berserk)
        {
            float persentHp = (float)HealthPoints / (float)MaxHP;
            if (persentHp >= 0.5)
                Attack = (int)((2 - persentHp) * FixedAttack);
            else if(persentHp >= 0.2)
                Attack = (int)((3 - persentHp) * FixedAttack);
            else
                Attack = (int)((4 - persentHp) * FixedAttack);
            UpdateCanvas();
        }
    }
    // вызывается из Enemy.IsAlive()
    public void BloodlustMood()
    {
        if(Bloodlust)
        {
            Debug.Log("BloodlustMode");
            Attack = Mathf.RoundToInt(Attack * BloodlustMultiply);
            UpdateCanvas();
        }
    }

}
