using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlUpVisualizer : MonoBehaviour
{
    [SerializeField] private GameObject selectSkillsMenu;
    [SerializeField] private GameObject boxCollider;
    [SerializeField] private GameObject buttonGameMenu;
    [SerializeField] private GameObject buttonGMLvlUp;
    [SerializeField] private GameObject TextLvlUp;

    public bool NewSkills { get; set; }

    private GetSkillController getSkillController;
    private LevelController levelController;

    private bool opened;
    private bool started;
    public void Start()
    {
        if (!started)
        {
            levelController = ReferencesToObjects.Player.LevelController;
            levelController.LvlUpdateEvent += EnableLvlUpButton;

            getSkillController = selectSkillsMenu.transform.GetChild(0).gameObject.GetComponent<GetSkillController>();
            started = true;
        }
    }

    public void OpenCloseSkillsMenu()
    {
        if (!opened)
        {
            opened = true;
            boxCollider.SetActive(true);
            selectSkillsMenu.SetActive(true);
            if (NewSkills)
            {
                getSkillController.TakeSkillsToButtons();
                NewSkills = false;
            }
        }
        else
        {
            opened = false;
            boxCollider.SetActive(false);
            selectSkillsMenu.SetActive(false);
        }
    }

    public void DisableLvlUpButton()
    {
        levelController.ReduceSkillPoint();
        if (levelController.SkillPoints <= 0)
        {
            buttonGMLvlUp.SetActive(false);
            buttonGameMenu.SetActive(true);
        }
        NewSkills = true;
        OpenCloseSkillsMenu();
    }

    private void EnableLvlUpButton(bool newLvl)
    {
        buttonGameMenu.SetActive(false);
        buttonGMLvlUp.SetActive(true);
        if (newLvl)
        {
            StartCoroutine(ShowText());
            if (levelController.SkillPoints == 1)
                NewSkills = true;
        }
    }

    //вызов надписи лвлап(возможно добавление анимации)
    IEnumerator ShowText()
    {
        TextLvlUp.SetActive(true);
        yield return new WaitForSeconds(1f);
        TextLvlUp.SetActive(false);
        yield break;
    }
}
