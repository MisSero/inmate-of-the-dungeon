using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool Attacted { get; set; }
    public int HealthPoints { get; protected set; }
    public int Attack { get; protected set; }

    protected int mySouls = 2;
    protected int myExp = 1;
    protected int chance = 10;
    protected GameObject[] itemsToDrop;

    protected Player player;
    protected Animator animator;

    protected virtual void Start()
    {
        player = ReferencesToObjects.Player;
        itemsToDrop = ReferencesToObjects.ItemsToDrop;
        animator = gameObject.GetComponent<Animator>();
    }
    public void OnMouseDown()
    {
        player.AttackEnemy(this, gameObject.transform.position);
    }

    public void DamagePlayer()
    {
        player.ChangeHp(-Attack);
    }
    public void DamagePlayer(int damage)
    {
        player.ChangeHp(-damage);
    }
    public bool ChangeHp(int howMuch)
    {
        HealthPoints += howMuch;
        foreach (var item in animator.parameters)
        {
            if(item.name == "Attacked")
            {
                animator.SetTrigger("Attacked");
                break;
            }
        }
        return IsAlive();
    }
    protected virtual void Drop()
    {
        int checkChance = Random.Range(1, 101);
        if (checkChance <= chance)
            Instantiate(itemsToDrop[Random.Range(0, itemsToDrop.Length)], transform.parent);
        player.TakeInfectedSoulsAndReduceCooldown(mySouls);
        player.TakeExp(myExp);
    }
    protected virtual bool IsAlive()
    {
        if (HealthPoints <= 0)
        {
            Destroy(gameObject);
            Drop();
            if (Attacted)
                player.ChangeHp((int)(HealthPoints * player.Vampirism));

            player.EnemiesKilled++;
            if(player.EnemiesKilled % 5 == 1)
            {
                // Проверка на предмет NeedforEbolution и скилл  Bloodlust
                player.NeedForEvolutionMode();

                if (player is Warrior warrior)
                    warrior.BloodlustMood();
            }
            return false;
        }
        else
            return true;
    }
   
    
}
