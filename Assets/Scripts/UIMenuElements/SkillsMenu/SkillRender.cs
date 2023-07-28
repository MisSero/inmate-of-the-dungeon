using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillRender : MonoBehaviour
{
    [SerializeField] private Text skillName;
    [SerializeField] private Text skillDescription;
    [SerializeField] private Image SkillSprite;

    private List<GameObject> skills;
    private GameObject tempSkill;
    private SampleSkill skillScript;
    private LvlUpVisualizer lvlUpVisualizer;

    private bool started;

    //private System.Random rand;
    private System.Random rand = new System.Random();

    private int skillNumber;
    void Start()
    {
        GetLinks();
    }
    // дописать, что бы меню сворачивалось, происходила смена кнопки
    public void OnButtonSelectedSkills()
    {
        skills.RemoveAt(skillNumber);
        skillScript.Effect();
        lvlUpVisualizer.DisableLvlUpButton();
        GetSkillController.receivedSkills.Add(skillNumber);
    }
    public void TakeSkill()
    {
        GetLinks();
        if (skills.Count != 0)
        {
            skillNumber = rand.Next(skills.Count);
            tempSkill = skills[skillNumber];
            RenderSkill();
        }
        else
            Debug.Log("Скиллы в списке закончились");
    }

    private void GetLinks()
    {
        if (!started)
        {
            skills = GetSkillController.skills;
            rand = ReferencesToObjects.Rand;
            lvlUpVisualizer = ReferencesToObjects.Canvas.GetComponent<LvlUpVisualizer>();
            started = true;
        }
    }
    private void RenderSkill()
    {
        skillScript = tempSkill.GetComponent<SampleSkill>();
        skillName.text = skillScript.Name;
        skillDescription.text = skillScript.Description;
        SkillSprite.sprite = skillScript.Icon;
    }
}
