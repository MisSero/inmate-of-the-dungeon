using UnityEngine;

//[CreateAssetMenu(menuName = "Skill")]
public class SkillAsset : ScriptableObject
{
    [SerializeField] private string skillName;
    [SerializeField] private string description;
    [SerializeField] private Sprite icon;
}
