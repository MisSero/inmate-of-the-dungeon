using System.Collections.Generic;
using UnityEngine;
using System;

public class GetItemController : MonoBehaviour
{
    [SerializeField] private List<GameObject> commonItems;
    [SerializeField] private List<GameObject> rareItems;
    [SerializeField] private List<GameObject> mythicalItems;
    [SerializeField] private GameObject itemButton;
    [SerializeField] private GameObject buttonReroll;
    [SerializeField] private GameObject canvasItemMenu;
    [SerializeField] private GameObject boxCollider;// обхект с boxCollider (находится в PlayerKeepre) для предотвращения движения при откртом меню

    // Предмет DiceOfPain для реролла предметов
    public bool DiceOfPain { get; set; }

    public static List<int> receivedCommonItems;
    public static List<int> receivedRareItems;
    public static List<int> receivedMythicalItems;

    private ItemRender itemRender;
    private Player player;


    void Awake()
    {
        receivedCommonItems = new List<int>();
        receivedRareItems = new List<int>();
        receivedMythicalItems = new List<int>();

        itemRender = itemButton.GetComponent<ItemRender>();
        itemRender.GetItemsList(commonItems, rareItems, mythicalItems);
        player = ReferencesToObjects.Player;

        //gameObject.transform.parent.gameObject.SetActive(false);
    }
    public void OpenItemMenu()
    {
        canvasItemMenu.SetActive(true);
        itemRender.TakeItem();
        boxCollider.SetActive(true);

        if (DiceOfPain)
            buttonReroll.SetActive(true);
    }

    public void CloseItemMenu()
    {
        canvasItemMenu.SetActive(false);
        boxCollider.SetActive(false);
    }

    public void UpdateItemsLoad()
    {
        foreach (int index in receivedCommonItems)
        {
            commonItems[index].GetComponent<SampleItem>().LoadEffect();
            commonItems.RemoveAt(index);
        }
        foreach (int index in receivedRareItems)
        {
            rareItems[index].GetComponent<SampleItem>().LoadEffect();
            rareItems.RemoveAt(index);
        }
        foreach (int index in receivedMythicalItems)
        {
            mythicalItems[index].GetComponent<SampleItem>().LoadEffect();
            mythicalItems.RemoveAt(index);
        }
    }

    // Реролл предмета по нажатию кнопки
    public void OnButtonReroll()
    {
        buttonReroll.SetActive(false);

        itemRender.TakeItem();

        Debug.Log("(Нормально ли окргляет до целого)Округление здоровья для урона после использования DiceOFPAIN" + Math.Round(player.MaxHP / 10f));

        player.ChangeHp(-(int)Math.Round(player.MaxHP / 10f));
    }
}
