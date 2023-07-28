using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BIGManiac : Boss
{
    private readonly int jumpJoltAttack = 6;
    private readonly int biteAttack = 4;
    private readonly int numberCellsToJumpJolt = 20;
    private bool checkJumpJolt;//1 атака босса
    private bool checkBite;// 2 атака босса
    private readonly Vector2[] biteCellsOptional = new Vector2[]
    {
        new Vector2(0, 0),
        new Vector2(1, 0),
        new Vector2(-1, 0),
        new Vector2(-3, 4),
        new Vector2(-3, 3),
        new Vector2(-3, 2),
        new Vector2(-1, 6),
        new Vector2(0, 6),
        new Vector2(1, 6),
        new Vector2(3, 4),
        new Vector2(3, 3),
        new Vector2(3, 2),
    };
    protected override void Start()
    {
        base.Start();
        HealthPoints = 40;
        Attack = 2;
        mySouls = 20;
        myExp = 15;
    }
    // метод запуска анимации JumpJolt ячейки через аниматор евентс
    public void StartCellJumpJoltAnimation()
    {
        StartCellAnimations(1, CellAnimations.JumpJolt);
        if (AttackOnCell())
        {
            DamagePlayer(jumpJoltAttack);
        }
        checkJumpJolt = false;
    }
    private void Update()
    {
        // Атака на каждый 5 и 7 ходы, с проверкой, что другая атака уже не идёт
        //выполнятеся проверка на то производится ли эта атака уже и производится ли другая
        if (!checkJumpJolt && player.MoveCounter % 5 == 0 && !checkBite)
        {
            checkMove = player.MoveCounter;
            SetJumpJolt();
        }
        if(checkJumpJolt && checkMove + 1 == player.MoveCounter)
        {
            JumpJolt();
        }
        if (!checkBite && player.MoveCounter % 7 == 0 && !checkJumpJolt)
        {
            checkMove = player.MoveCounter;
            SetBite();
        }
        if (checkBite && checkMove + 1 == player.MoveCounter)
        {
            Bite();
        }

    }
    private void SetJumpJolt()
    {
        CheckWithResetPool();
        while(AttackedCells1.Count <= numberCellsToJumpJolt)
        {
            SelectCellToAttack(1);
        }
        checkJumpJolt = true;
    }
    // Атака - прыжок
    private void JumpJolt()
    {
        animator.SetTrigger("JumpJolt");
    }
    // установление атаки - укус на все ячейки biteCells и на половину biteCellsOptional с занесением уже использованных рандомных чисел
    private void SetBite()
    {
        CheckWithResetPool();
        animator.SetBool("Bite", true);
        checkBite = true;
        foreach (var item in aroundCells)
        {
            SelectCellToAttack(2, item);
        }
        int[] usedCell = new int[biteCellsOptional.Length / 2];
        for (int i = 0; i < biteCellsOptional.Length / 2; i++)
        {
            bool closedCell = false;//для проверки опциональных ячеек на занятость
            int randNumber = ReferencesToObjects.Rand.Next(biteCellsOptional.Length);
            foreach (var item in usedCell)
            {
                if (randNumber == item)
                {
                    closedCell = true;
                    break;
                }
            }
            if (closedCell)
            {
                continue;
            }
            usedCell[i] = randNumber;
            SelectCellToAttack(2, biteCellsOptional[randNumber]);
        }
    }
    private void Bite()
    {
        animator.SetBool("Bite", false);
        StartCellAnimations(2, CellAnimations.Bite);
        if (AttackOnCell())
        {
            DamagePlayer(biteAttack);
            HealthPoints += biteAttack * 2;
        }
        checkBite = false;
    }

    
}
