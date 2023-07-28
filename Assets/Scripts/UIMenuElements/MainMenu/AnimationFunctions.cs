using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationFunctions : MonoBehaviour
{
    [SerializeField] private GameObject buttonText;

    public void HideText()
    {
        buttonText.SetActive(false);
    }
    public void StartGame()
    {
        ReferencesToObjects.ObjectsKeeper.EnableObjects();
        SceneManager.LoadScene("Level 1");
    }
    public void ExitApp()
    {
        Application.Quit();
    }
    public void PressButton()
    {
        gameObject.GetComponent<Animator>().SetTrigger("Press");
    }
}
