using UnityEngine;
using UnityEngine.UI;

public class GMTextController : OpenEvent
{
    [SerializeField] private Text levelText;
    [SerializeField] private Text maxHpText;
    [SerializeField] private Text attackText;

    private Player player;
    private LevelController levelController;



    protected override void Start()
    {
        base.Start();
        player = ReferencesToObjects.Player;
        levelController = player.LevelController;

        UpdateUI();
    }

    protected override void UpdateUI()
    {
        levelText.text = levelController.CurrentLvl.ToString();
        maxHpText.text = player.MaxHP.ToString();
        attackText.text = player.Attack.ToString();
    }
}
