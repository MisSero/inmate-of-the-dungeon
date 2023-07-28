using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{

    [SerializeField] protected GameObject attackCell;
    [SerializeField] protected GameObject Exit;

    public List<GameObject> AttackedCells1 { get; protected set; } // ячейки, показывающие, где будет атака на некст ход(для 1 атаки)
    public List<GameObject> AttackedCells2 { get; protected set; } // ячейки, показывающие, где будет атака на некст ход(для 2 атаки)

    protected List<GameObject> PoolCellsToAttack = new List<GameObject>(); // пул ячеек для подсвечивания
    protected List<Transform> BossPlatformCells; // ячейки платформы босс(для спавна дропа)
    protected List<Vector2> PositionToAttackInitial = new List<Vector2>();// все ячейки, где может быть атака
    protected List<Vector2> PositionToAttackChanged1; // изменненый лист ячеек атаки, для избежания повтора(для 1 атаки)
    protected List<Vector2> PositionToAttackChanged2; // изменненый лист ячеек атаки, для избежания повтора (для 2 атаки)
    protected List<Vector2> NowAttack1 = new List<Vector2>(); 
    protected List<Vector2> NowAttack2 = new List<Vector2>();
    protected Vector2[] aroundCells = new Vector2[]
    {
        new Vector2(0, 1),
        new Vector2(-1, 1),
        new Vector2(-2, 1),
        new Vector2(1, 1),
        new Vector2(2, 1),
        new Vector2(2, 2),
        new Vector2(2, 3),
        new Vector2(2, 4),
        new Vector2(2, 5),
        new Vector2(1, 5),
        new Vector2(0, 5),
        new Vector2(-1, 5),
        new Vector2(-2, 5),
        new Vector2(-2, 4),
        new Vector2(-2, 3),
        new Vector2(-2, 2),
    }; // ячейки вокруг босса для атак
    protected int checkMove; // отслеживание ходов игрока для атаки

    private Vector2 myPosition;
    protected override void Start()
    {
        base.Start();
        myPosition = transform.position;
        AttackedCells1 = new List<GameObject>();
        AttackedCells2 = new List<GameObject>();
        GeneratePoolCells();
    }

    //передача доступнызх для атаки ячеек
    public virtual void SetCellsToAttack(Transform[] cells)
    {
        foreach (Transform cellTransform in cells)
        {
            PositionToAttackInitial.Add(cellTransform.position);
        }
        PositionToAttackChanged1 = new List<Vector2>(PositionToAttackInitial);
        PositionToAttackChanged2 = new List<Vector2>(PositionToAttackInitial);
    }
    public void SetBossPlatformCells(Transform[] cells)
    {
        BossPlatformCells = new List<Transform>(cells);
    }
    public bool CheckMyPosition(Vector2 objectPosition)
    {
        if (myPosition == objectPosition
            || new Vector2(myPosition.x + 1, myPosition.y) == objectPosition || new Vector2(myPosition.x - 1, myPosition.y) == objectPosition
            || new Vector2(myPosition.x, myPosition.y + 1) == objectPosition || new Vector2(myPosition.x, myPosition.y - 1) == objectPosition
            || new Vector2(myPosition.x + 1, myPosition.y + 1) == objectPosition || new Vector2(myPosition.x + 1, myPosition.y - 1) == objectPosition
            || new Vector2(myPosition.x - 1, myPosition.y - 1) == objectPosition || new Vector2(myPosition.x - 1, myPosition.y + 1) == objectPosition)
            return true;
        else
            return false;
    }
    public void ResetPool(int numberAttack)
    {
        if(numberAttack == 1)
        {
            while (AttackedCells1.Count > 0)
            {
                GameObject target = AttackedCells1[0];
                AttackedCells1.RemoveAt(0);
                target.SetActive(false);
                PoolCellsToAttack.Add(target);
            }
        NowAttack1 = new List<Vector2>();
        PositionToAttackChanged1 = new List<Vector2>(PositionToAttackInitial);
        }
        if(numberAttack == 2)
        {
            while (AttackedCells2.Count > 0)
            {
                GameObject target = AttackedCells2[0];
                AttackedCells2.RemoveAt(0);
                target.SetActive(false);
                PoolCellsToAttack.Add(target);
            }
            NowAttack2 = new List<Vector2>();
            PositionToAttackChanged2 = new List<Vector2>(PositionToAttackInitial);
        }
    }
    public void GeneratePoolCells()
    {
        GameObject poolParent = new GameObject("PoolAttackCells");
        Transform poolParentTransform = poolParent.transform;
        // Создание ячеек атаки 
        for (int i = 0; i < PositionToAttackInitial.Count; i++)
        {
            GameObject cell = Instantiate(attackCell, poolParentTransform);
            cell.GetComponent<AttackCell>().boss = this;
            PoolCellsToAttack.Add(cell);
        }
    }
    // Добавить спавн сундука, сокровищныцы или...
    protected override void Drop()
    {
        Instantiate(Exit, BossPlatformCells[ReferencesToObjects.Rand.Next(BossPlatformCells.Count)].position, Quaternion.identity);
        player.TakeInfectedSoulsAndReduceCooldown(mySouls);
        player.TakeExp(myExp);
    }
    protected override bool IsAlive()
    {
        if (HealthPoints <= 0)
        {
            Destroy(gameObject);
            ResetPool(1);
            ResetPool(2);
            Drop();
            return false;
        }
        else
            return true;
    }
    // Взятие ячейки и поставление её в случайно место
    protected void SelectCellToAttack(int numberAttack)
    {
        GameObject attackCell = TakeFromPool();
        if(numberAttack == 1)
        {
            int randNumber = ReferencesToObjects.Rand.Next(PositionToAttackChanged1.Count);
            attackCell.transform.position = PositionToAttackChanged1[randNumber];
            NowAttack1.Add(PositionToAttackChanged1[randNumber]);
            PositionToAttackChanged1.RemoveAt(randNumber);
            AttackedCells1.Add(attackCell);
        }
        if(numberAttack == 2)
        {
            int randNumber = ReferencesToObjects.Rand.Next(PositionToAttackChanged2.Count);
            attackCell.transform.position = PositionToAttackChanged2[randNumber];
            NowAttack2.Add(PositionToAttackChanged2[randNumber]);
            PositionToAttackChanged2.RemoveAt(randNumber);
            AttackedCells2.Add(attackCell);
        }
        
    }
    //Взятие ячейки с постановкой на определенное место
    protected void SelectCellToAttack(int numverAttack, Vector2 attackPosition)
    {
        GameObject attackCell = TakeFromPool();
        attackCell.transform.position = attackPosition;
        if(numverAttack == 1)
        {
            NowAttack1.Add(attackPosition);
            AttackedCells1.Add(attackCell);
        }
        if(numverAttack == 2)
        {
            NowAttack2.Add(attackPosition);
            AttackedCells2.Add(attackCell);
        }
    }
    // Атака по герою, если он стоит на месте атаки
    protected bool AttackOnCell()
    {
        Vector2 playerPosiotion = player.transform.position;
        foreach (Vector2 item in NowAttack1)
        {
            if (playerPosiotion == item)
                return true;
        }
        foreach (Vector2 item in NowAttack2)
        {
            if (playerPosiotion == item)
                return true;
        }
        return false;
    }
    // Взятие первой ячейки из пула
    protected GameObject TakeFromPool()
    {
        if(PoolCellsToAttack.Count > 0)
        {
            GameObject result = PoolCellsToAttack[0];
            PoolCellsToAttack.RemoveAt(0);
            result.SetActive(true);
            return result;
        }
        return null;
    }

    // Дополнительная проверка на количество объектов в пуле, если анимация не успевает обновить пул
    protected void CheckWithResetPool()
    {
        if (PoolCellsToAttack.Count < PositionToAttackInitial.Count / 2)
        {
            ResetPool(1);
            ResetPool(2);
        }
    }
    // запуск анимации на ячейках атаки 
    protected void StartCellAnimations(int numberAttack, CellAnimations nameAnimation)
    {
        if(numberAttack == 1)
        {
            foreach (GameObject item in AttackedCells1)
            {
                item.GetComponent<AttackCell>().StartAnimation(nameAnimation);
            }
        }
        if(numberAttack == 2)
        {
            foreach (GameObject item in AttackedCells2)
            {
                item.GetComponent<AttackCell>().StartAnimation(nameAnimation);
            }
        }
    }
}
