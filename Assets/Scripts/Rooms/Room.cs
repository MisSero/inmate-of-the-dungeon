using UnityEngine;

public class Room : MonoBehaviour
{

    protected Player player;
    protected Transform thisTransform;

    private bool opened;
    private Transform playerTransform;
    private Vector3 playerPosition;

    void Start()
    {
        thisTransform = gameObject.transform;
        player = ReferencesToObjects.Player;
        playerTransform = ReferencesToObjects.PlayerObject.transform;
        playerPosition = playerTransform.position;
        if (playerTransform.position == thisTransform.position)
        {
            OpenShroud();
        }
            OpenSideShround();
    }
    private void Update()
    {
        if(playerTransform.position != playerPosition )
        {
            OpenSideShround();
            playerPosition = playerTransform.position;
        }
    }
    private void OnMouseDown()
    {
        if (thisTransform.childCount < 1)
            player.SetPositionToGo(thisTransform.position);
    }

    public void OpenShroud()
    {
        if (!opened)
        {
            if(thisTransform.childCount >= 1 && thisTransform.GetChild(0).CompareTag("Shroud"))
            {
                Destroy(thisTransform.GetChild(0).gameObject);
                opened = true;
                if (thisTransform.childCount >= 2 && thisTransform.GetChild(1).CompareTag("Enemy"))
                    thisTransform.GetChild(1).gameObject.GetComponentInChildren<EnemyUI>().enabled = true;
            }
        }
    }
    private void OpenSideShround()
    {
        //Проверка вызывается у ячеек по кресту
        if ((Mathf.Abs(playerTransform.position.x - thisTransform.position.x) == 1)
            && (playerTransform.position.y - thisTransform.position.y == 0) ||
                (playerTransform.position.x - thisTransform.position.x == 0)
            && (Mathf.Abs(playerTransform.position.y - thisTransform.position.y) == 1))
            OpenShroud();
    }
}
