using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    private Player player;
    private Vector2 myPosition;
    private Vector2 playerPosition;

    private void Start()
    {
        myPosition = gameObject.transform.position;
        player = ReferencesToObjects.Player;
    }

    //Старая версия*
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    DontDestroyOnLoad(gameObject);
    //    //Высвечивать вопрос хочет ли он перейти на некс лвл и тогда корутина с задержкой будет не нужна
    //    if (ReferencesToObjects.LvlNumber < ReferencesToObjects.GetMaxLvl())
    //    {
    //        StartCoroutine(NextLevel());
    //    }
    //    else
    //    {
    //        Debug.Log("End Game");
    //        ReferencesToObjects.Player.Die();
    //    }
    //}
    //private IEnumerator NextLevel()
    //{
    //    yield return new WaitForSeconds(0.1f);
    //    ReferencesToObjects.Player.transform.position = new Vector3(0, 0);
    //    SceneManager.LoadScene(ReferencesToObjects.LvlNumber + 1);
    //    ReferencesToObjects.Player.transform.GetComponent<Player>().MoveCounter = 1;
    //    Destroy(gameObject);
    //    yield break;
    //}

    private void OnMouseDown()
    {
        playerPosition = player.transform.position;

        if((Mathf.Abs(playerPosition.x - myPosition.x) == 1)
            && (playerPosition.y - myPosition.y == 0) ||
                (playerPosition.x - myPosition.x == 0)
            && (Mathf.Abs(playerPosition.y - myPosition.y) == 1))
        {
            PlayerPrefsKeeper.SaveInfectedSouls();
            if (ReferencesToObjects.LvlNumber < ReferencesToObjects.GetMaxLvl())
            {
                player.transform.GetComponent<Player>().MoveCounter = 1;
                player.transform.position = new Vector3(0, 0);
                SceneManager.LoadScene(ReferencesToObjects.LvlNumber + 1);
            }
            else
            {
                Debug.Log("End Game");
                ReferencesToObjects.Player.Die();
            }
        }
    }
}
