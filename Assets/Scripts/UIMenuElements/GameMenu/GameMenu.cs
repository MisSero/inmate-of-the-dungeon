using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenu : MonoBehaviour
{
    public delegate void OpenHandler();
    public event OpenHandler OpenEvent;

    [SerializeField] private GameObject boxCollider;
    [SerializeField] private GameObject canvasGameMenu;

    private bool opened;

    //Вызваются кнопками
    public void OpenCloseMenu()
    {
        if (!opened)
        {
            canvasGameMenu.SetActive(true);
            boxCollider.SetActive(true);
            OpenEvent?.Invoke();
            opened = true;
        }
        else
        {
            canvasGameMenu.SetActive(false);
            boxCollider.SetActive(false);
            opened = false;
        }
    }
    
    public void ExitToMainMenu()
    {
        ReferencesToObjects.Player.ExitToMenu();
    }
}
