using UnityEngine;

public class OpenEvent : MonoBehaviour
{
    [SerializeField] private GameObject gameMenuObject;

    protected virtual void Start()
    {
        gameMenuObject.GetComponent<GameMenu>().OpenEvent += UpdateUI;
    }

    protected virtual void UpdateUI()
    {
    }
}
