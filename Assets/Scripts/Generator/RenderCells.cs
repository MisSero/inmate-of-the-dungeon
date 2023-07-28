using System.Collections.Generic;
using UnityEngine;

public class RenderCells : MonoBehaviour
{
    [SerializeField] Sprite center;
    [SerializeField] Sprite leftRightPath;
    [SerializeField] Sprite upDownPath;
    [SerializeField] Sprite upRightCorner;
    [SerializeField] Sprite upLeftCorner;
    [SerializeField] Sprite downRightCorner;
    [SerializeField] Sprite downLeftCorner;
    [SerializeField] Sprite up;
    [SerializeField] Sprite down;
    [SerializeField] Sprite right;
    [SerializeField] Sprite left;
    [SerializeField] Sprite upWall;
    [SerializeField] Sprite downWall;
    [SerializeField] Sprite rightWall;
    [SerializeField] Sprite leftWall;

    public GameObject CellToExit { get; private set; }

    private List<GameObject> singleCells = new List<GameObject>();

    public void Render()
    {
        List<GameObject> createdCells = gameObject.GetComponent<LevelGenerator>().createdCells;
        List<Vector3> cellsPosition = gameObject.GetComponent<LevelGenerator>().cellsPosition;
        foreach(GameObject cell in createdCells)
        {
            bool down = false;
            bool up = false;
            bool right = false;
            bool left = false;

            SpriteRenderer spriteRenderer = cell.GetComponent<SpriteRenderer>();
            Vector3 position = cell.transform.position;
            Vector3 positionUp = new Vector3(position.x, position.y + 1);
            Vector3 positionDown = new Vector3(position.x, position.y - 1);
            Vector3 positionRight = new Vector3(position.x + 1, position.y);
            Vector3 positionLeft = new Vector3(position.x - 1, position.y);

            int directionCount = 0;
            foreach (Vector3 item in cellsPosition)
            {
                //Нахождение в какие стороны от ячейки есть другие ячейки
                if (item == positionUp)
                {
                    up = true;
                    directionCount++;
                }
                if (item == positionDown)
                {
                    down = true;
                    directionCount++;
                }
                if (item == positionRight)
                {
                    right = true;
                    directionCount++;
                }
                if (item == positionLeft)
                {
                    left = true;
                    directionCount++;
                }
            }
                //Определение сколько есть путей у ячейки и на основании этого подбор спрайта
                switch (directionCount)
                {
                    case 4:
                        spriteRenderer.sprite = center;
                        break;
                    case 3:
                        if (up && right && down)
                            spriteRenderer.sprite = leftWall;
                        else if (up && left && down)
                            spriteRenderer.sprite = rightWall;
                        else if (right && left && up)
                            spriteRenderer.sprite = downWall;
                        else
                            spriteRenderer.sprite = upWall;
                        break;
                    case 2:
                        if (right && left)
                        {
                            spriteRenderer.sprite = leftRightPath;
                            break;
                        }
                        else if (up && down)
                        {
                            spriteRenderer.sprite = upDownPath;
                            break;
                        }
                        else if (up && left)
                            spriteRenderer.sprite = downRightCorner;
                        else if (up && right)
                            spriteRenderer.sprite = downLeftCorner;
                        else if (down && left)
                            spriteRenderer.sprite = upRightCorner;
                        else
                            spriteRenderer.sprite = upLeftCorner;
                        if(cell.transform.position != new Vector3(0,0))
                            singleCells.Add(cell);
                        break;
                    case 1:
                        if (up)
                            spriteRenderer.sprite = this.down;
                        else if (down)
                            spriteRenderer.sprite = this.up;
                        else if (left)
                            spriteRenderer.sprite = this.right;
                        else
                            spriteRenderer.sprite = this.left;
                        if (cell.transform.position != new Vector3(0, 0))
                            singleCells.Add(cell);
                        break;
                    case 0:
                        break;
            }
        }
        CellToExit = singleCells[ReferencesToObjects.Rand.Next(0, singleCells.Count)];
    }
}
