using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagiciansBlue : Boss
{
    [SerializeField] private GameObject[] slimeToSpawn;

    private List<Transform> aroundCellsTransform = new List<Transform>(); // лист для атаки Spawn
    private int spawnDamage = 2;
    private int crossfireDamage = 7;
    private int numberCellsToCrossfire = 20;
    private bool checkSpawn;// 2 атака
    private bool checkCrossfire;// 1 атака


    protected override void Start()
    {
        base.Start();
        HealthPoints = 30;
        Attack = 2;
        mySouls = 20;
        myExp = 15;
    }
    private void Update()
    {
        // Атака на каждый 4 ход и спав на 11
        //выполнятеся проверка на то производится ли эта атака уже и производится ли другая
        if (!checkSpawn && player.MoveCounter % 11 == 0 && !checkCrossfire)
        {
            checkMove = player.MoveCounter;
            SetSpawn();
        }
        if (checkSpawn && checkMove + 1 == player.MoveCounter)
        {
            Spawn();
        }
        if (!checkCrossfire && player.MoveCounter % 4 == 0 && !checkSpawn)
        {
            checkMove = player.MoveCounter;
            SetCrossfire();
        }
        if (checkCrossfire && checkMove + 1 == player.MoveCounter)
        {
            Crossfire();
        }

    }

    public override void SetCellsToAttack(Transform[] cells)
    {
        foreach (Transform cellTransform in cells)
        {
            Vector2 temp = cellTransform.position;
            PositionToAttackInitial.Add(temp);
            foreach (Vector2 item in aroundCells)
            {
                if (item == temp)
                {
                    aroundCellsTransform.Add(cellTransform);
                    break;
                }
            }
        }
        PositionToAttackChanged1 = new List<Vector2>(PositionToAttackInitial);
        PositionToAttackChanged2 = new List<Vector2>(PositionToAttackInitial);
    }

    private void SetSpawn()
    {
        CheckWithResetPool();
        checkSpawn = true;
        foreach (var item in aroundCellsTransform)
        {
            if (item.childCount == 0)
            {
                SelectCellToAttack(2, item.transform.position);
            }
        }
    }
    private void Spawn()
    {
        animator.SetTrigger("Spawn");
        StartCellAnimations(2, CellAnimations.Spawn);
        if (AttackOnCell())
        {
            DamagePlayer(spawnDamage);
            player.GetComponent<Transform>().position = new Vector2(0, -1);
        }
        foreach (Vector2 cell in NowAttack2)// обход с помеченными ячейками атаки для спавна врага
        {
            foreach (Transform item in aroundCellsTransform)//обход ячеек вокруг босса, что бы правильно присвоить родителя врагу
            {
                if(cell == (Vector2)item.position)
                {
                    GameObject tempSlime = Instantiate(slimeToSpawn[ReferencesToObjects.Rand.Next(slimeToSpawn.Length)], item);
                    tempSlime.transform.GetChild(0).GetComponent<EnemyUI>().enabled = true;
                }
            }
        }
        checkSpawn = false;
    }
    private void SetCrossfire()
    {
        CheckWithResetPool();
        while (AttackedCells1.Count <= numberCellsToCrossfire)
        {
            SelectCellToAttack(1);
        }
        checkCrossfire = true;
    }

    private void Crossfire()
    {
        animator.SetTrigger("Crossfire");
        StartCellAnimations(1, CellAnimations.Crossfire);
        if (AttackOnCell())
        {
            DamagePlayer(crossfireDamage);
        }
        // атака по слаймам
        foreach (Transform cellTransform in aroundCellsTransform)
        {
            if (cellTransform.childCount > 0 && cellTransform.GetChild(0).CompareTag("Enemy"))
            {
                foreach (Vector2 item in NowAttack1)
                {
                    if(item == (Vector2)cellTransform.position)
                    {
                        cellTransform.GetChild(0).GetComponent<Enemy>().ChangeHp(-crossfireDamage/2);
                        break;
                    }
                }
            }
        }
        checkCrossfire = false;
    }
}