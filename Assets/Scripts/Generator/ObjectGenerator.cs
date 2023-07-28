using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGenerator : MonoBehaviour
{
    [SerializeField] private GameObject exit;
    [SerializeField] private GameObject chest;
    [SerializeField] private GameObject trap;
    [SerializeField] private GameObject enemyOften;
    [SerializeField] private GameObject enemyAverage;
    [SerializeField] private GameObject enemyRarely;

    [SerializeField] private int numberChests = 1;
    [SerializeField] private int numberEnemies = 5;
    [SerializeField] private int numberTraps = 3;

    [SerializeField] private GameObject enemyMark;

    private System.Random rand;
    private List<GameObject> availableCells;
    private List<GameObject> enemies;
    //private List<Vector3> cellsPosition;
    private GameObject cellToDelete;// первая ячейка для удаления из availableCells, что бы ненарушать foreach
    private GameObject CellToExit;
    private Vector3 CellToExitPosition;
    private Vector3 initialCell = new Vector3(0, 0, 0);
    private bool oneLastToDelete;//поле для удаления важных занятых ячеек(начальная и последняя) из availableCells



    public void Generate()
    {
        rand = ReferencesToObjects.Rand;
        availableCells = gameObject.GetComponent<LevelGenerator>().createdCells;
        enemies = new List<GameObject>();
        //cellsPosition = gameObject.GetComponent<LevelGenerator>().cellsPosition;
        CellToExit = gameObject.GetComponent<RenderCells>().CellToExit;
        CellToExitPosition = CellToExit.transform.position;
        foreach (var item in availableCells)
        {
            Vector3 position = item.transform.position;
            if (position == initialCell || position == CellToExitPosition)
            {
                if (!oneLastToDelete)
                    cellToDelete = item;

                if (oneLastToDelete)
                {
                    availableCells.Remove(item);
                    availableCells.Remove(cellToDelete);
                    break;
                }
                    oneLastToDelete = true;
            }
        }
        GenerateExit();
        GenerateChest();
        GenerateEnemy();
        GenerateTraps();

        // Методы для предметов
        OpenExit();
        MarkEnemy();

        ReferencesToObjects.Player.Enemies = enemies;
    }

    private void GenerateExit()
    {
        Instantiate(exit, new Vector3(CellToExitPosition.x, CellToExitPosition.y, -1), Quaternion.identity);
    }

    private void GenerateChest()
    {
        while(numberChests > 0)
        {
            Transform parent = RandomCell();
            if (parent.childCount == 1)
            {
                Instantiate(chest, parent);
                numberChests--;
            }
        }
    }

    private void GenerateEnemy()
    {
        while(numberEnemies > 0)
        {
            Transform parent = RandomCell();
            if (parent.childCount == 1)
            {
                int randomChanсe = ReferencesToObjects.Rand.Next(0, 100);
                if (randomChanсe < 47)
                    enemies.Add(Instantiate(enemyOften, parent));
                else if (randomChanсe >= 47 && randomChanсe < 80)
                    enemies.Add(Instantiate(enemyAverage, parent));
                else
                    enemies.Add(Instantiate(enemyRarely, parent));

                numberEnemies--;
            }
        }
    }

    private void GenerateTraps()
    {
        GameObject trapParent = new GameObject("Traps");
        Transform trapParentTransform = trapParent.transform;
        while(numberTraps > 0)
        {
            Transform parent = RandomCell();
            if(parent.childCount == 1)
            {
                Instantiate(trap, parent.position, Quaternion.identity, trapParentTransform);

                numberTraps--;
            }
        }
    }
    private Transform RandomCell()
    {
        //availableCells = ShuffleList(availableCells);
        //Transform currentCell = availableCells[availableCells.Count - 1].transform;
        //availableCells.RemoveAt(availableCells.Count - 1);

        int randomSlot = rand.Next(0, availableCells.Count);
        Transform currentCell = availableCells[randomSlot].transform;
        availableCells.RemoveAt(randomSlot);
        return currentCell;
    }

    // Метод для компаса
    private void OpenExit()
    {
        if (ReferencesToObjects.Player.Compass)
            CellToExit.GetComponent<Room>().OpenShroud();
    }

    // метод для ClearEyes
    private void MarkEnemy()
    {
        if (ReferencesToObjects.Player.ClearEyes)
        {
            foreach (GameObject item in enemies)
            {
                Instantiate(enemyMark, item.transform);
            }
        }
    }

    //private List<GameObject> ShuffleList(List<GameObject> createdCells)
    //{
    //    int n = createdCells.Count - 1;
    //    for (int i = 0; i < n; i++)
    //    {
    //        int j = i + rand.Next(0, n - i);
    //        GameObject temp = createdCells[j];
    //        createdCells[j] = createdCells[i];
    //        createdCells[i] = temp;
    //    }
    //    return createdCells;
    //}
}
