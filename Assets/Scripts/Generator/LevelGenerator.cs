using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] bossRooms;
    [SerializeField] private GameObject cell;
    [SerializeField] private int maxNumberRooms;
    [SerializeField] private bool bossLevel;

    public List<GameObject> createdCells { get; private set; }
    public List<Vector3> cellsPosition { get; private set; }

    private int currentNumberRooms;
    private int randSeed;
    private System.Random rand;
    private GameObject currentCell;

    private void Awake()
    {

        if (!ReferencesToObjects.Loaded)
        {
            // Создание отдельного рандом на уровень, для его воспроизводства(сид сохраняется)
            randSeed = (int)DateTime.Now.Ticks;
            rand = new System.Random(randSeed);
            ReferencesToObjects.Rand = rand;
            ReferencesToObjects.RandSeed = randSeed;

            ReferencesToObjects.LvlNumber++;
            ReferencesToObjects.SaveAndResetScript.SaveGame();
        }
        else
            rand = ReferencesToObjects.Rand;

        ReferencesToObjects.Loaded = false;

        if (bossLevel)
        {
            Instantiate(bossRooms[rand.Next(0, bossRooms.Length)]);
        }
        else
        {
            GenerateCells();
            // Проверка на открытие карты
            if (ReferencesToObjects.Player.MagicMap)
                OpenMap();
            gameObject.GetComponent<RenderCells>().Render();
            gameObject.GetComponent<ObjectGenerator>().Generate();
        }

    }

    private void GenerateCells()
    {
        createdCells = new List<GameObject>();
        cellsPosition = new List<Vector3>();
        Transform parent = new GameObject("Level").transform;
        GameObject zeroCell = Instantiate(cell, new Vector3(0, 0, 0), Quaternion.identity, parent);
        createdCells.Add(zeroCell);
        cellsPosition.Add(zeroCell.transform.position);
        currentNumberRooms++;

        while(currentNumberRooms < maxNumberRooms)
        {
            //if (Random.Range(0, 10) <= 6)
            //    currentCell = Instantiate(cell, GetPosition(createdCells[createdCells.Count - 1]), Quaternion.identity, parent);
            //else
            //    currentCell = Instantiate(cell, GetPosition(createdCells[Random.Range(0, createdCells.Count - 1)]), Quaternion.identity, parent);

            createdCells = ShuffleList(createdCells);
            currentCell = Instantiate(cell, GetPosition(createdCells[createdCells.Count - 1]), Quaternion.identity, parent);

            createdCells.Add(currentCell);
            cellsPosition.Add(currentCell.transform.position);
            currentNumberRooms++;
        }
    }
    //Выдача соседней позиции. Если на неё что-то стоит, то поиск соседгней позиции у случайной уже созданной ячейки
    private Vector3 GetPosition(GameObject cell)
    {
        Vector3 currentPostiont = cell.transform.position;
        Vector3[] positionToCreate = new Vector3[]
        {
            new Vector3(currentPostiont.x + 1, currentPostiont.y),
            new Vector3(currentPostiont.x - 1, currentPostiont.y),
            new Vector3(currentPostiont.x, currentPostiont.y + 1),
            new Vector3(currentPostiont.x, currentPostiont.y - 1)
        };
        Vector3 positiontToGo = positionToCreate[rand.Next(0, positionToCreate.Length)];
        foreach (Vector3 cellPosition in cellsPosition)
        {
            if(cellPosition == positiontToGo)
            {
                positiontToGo = GetPosition(createdCells[rand.Next(0, createdCells.Count - 1)]);
                return positiontToGo;
            }
        }
        return positiontToGo;
    }
    private List<GameObject> ShuffleList(List<GameObject> createdCells)
    {
        int n = createdCells.Count - 1;
        for (int i = 0; i < n; i++)
        {
            int j = i + rand.Next(0, n - i);
            GameObject temp = createdCells[j];
            createdCells[j] = createdCells[i];
            createdCells[i] = temp;
        }
        return createdCells;
    }

    // Метод для открытия карты(удаление шрауда со всех ячеек)(активируется предмето\скиллом)
    private void OpenMap()
    {
        foreach (GameObject cell in createdCells)
        {
            cell.GetComponent<Room>().OpenShroud();
        }
    }
}
