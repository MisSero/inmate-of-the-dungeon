using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetSkillController : MonoBehaviour
{
    [SerializeField] private List<GameObject> skillsTotal;
    [SerializeField] private List<GameObject> skillButtons;
    [SerializeField] private GameObject rerollButtonObject;

    public static List<GameObject> skills;
    public static List<int> receivedSkills;


    private List<SkillRender> skillRenders;
    private Button rerollButton;

    void Awake()
    {
        skills = new List<GameObject>(skillsTotal);
        skillRenders = new List<SkillRender>();
        receivedSkills = new List<int>();

        rerollButton = rerollButtonObject.GetComponent<Button>();

        foreach (GameObject item in skillButtons)
        {
            skillRenders.Add(item.GetComponent<SkillRender>());
        }

        gameObject.transform.parent.gameObject.SetActive(false);
    }

    public void TakeSkillsToButtons()
    {
        foreach (SkillRender item in skillRenders)
        {
            item.TakeSkill();
        }

        if (ReferencesToObjects.Player.HandOfFate && !rerollButtonObject.activeInHierarchy)
        {
            rerollButtonObject.SetActive(true);
            rerollButton.interactable = true;
        }
    }

    // По нажатьию кнопки реролла
    public void OnButtonRerollSkills()
    {
        TakeSkillsToButtons();
        rerollButton.interactable = false;
    }

    public static void UpdateSkillsLoad()
    {
        foreach (int index in receivedSkills)
        {
            skills[index].GetComponent<SampleSkill>().LoadEffect();
            skills.RemoveAt(index);
        }
    }

}
