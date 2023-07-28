using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ExpBar : OpenEvent
{

    private Slider slider;
    private LevelController levelController;

    protected override void  Start()
    {
        base.Start();
        slider = gameObject.GetComponent<Slider>();
        levelController = ReferencesToObjects.Player.LevelController;

        UpdateUI();
    }
    protected override void UpdateUI()
    {
        slider.value = levelController.CurrentExp * 100 / levelController.GetMaxExp();
    }
}
