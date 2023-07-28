using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemRender : MonoBehaviour
{
    [SerializeField] private Text itemName;
    [SerializeField] private Text itemDescription;
    [SerializeField] private Image itemSprite;
    [SerializeField] private Text itemCost;
    [SerializeField] private GameObject ItemUnknownObject;

    private Player player;
    private List<GameObject> commonItems;
    private List<GameObject> rareItems;
    private List<GameObject> mythicalItems;
    private GameObject tempItem;
    private SampleItem itemScript;
    private GetItemController itemController;
    private ItemRarity tempRarity;

    private bool started;

    //private System.Random rand;
    private System.Random rand = new System.Random();

    private int itemNumber;
    void Start()
    {
        player = ReferencesToObjects.Player;
        rand = ReferencesToObjects.Rand;
        itemController = ReferencesToObjects.ItemController;
        //GetLinks();
    }

    public void GetItemsList(List<GameObject> commonItems, List<GameObject> rareItems, List<GameObject> mythicalItems)
    {
        this.commonItems = commonItems;
        this.rareItems = rareItems;
        this.mythicalItems = mythicalItems;
    }
    // Вызов с кнопки
    public void OnButtonSelectedItem()
    {
        switch (tempRarity)
        {
            case ItemRarity.common:
                commonItems.RemoveAt(itemNumber);
                GetItemController.receivedCommonItems.Add(itemNumber);
                break;
            case ItemRarity.rare:
                rareItems.RemoveAt(itemNumber);
                GetItemController.receivedRareItems.Add(itemNumber);
                break;
            case ItemRarity.mythical:
                mythicalItems.RemoveAt(itemNumber);
                GetItemController.receivedMythicalItems.Add(itemNumber);
                break;
        }

        itemScript.Effect();
        itemController.CloseItemMenu();
    }
    public void OnButtonRejectItem()
    {
        itemController.CloseItemMenu();
        player.TakeInfectedSouls(itemScript.GetCost());
    }

    public void TakeItem()
    {
        //GetLinks();
        RarityDetermination();

        switch (tempRarity)
        {
            case ItemRarity.common:
                if (commonItems != null)
                {
                    itemNumber = rand.Next(commonItems.Count);
                    tempItem = commonItems[itemNumber];
                }
                else
                    TakeUnknownObject();
                break;
            case ItemRarity.rare:
                if (rareItems != null)
                {
                    itemNumber = rand.Next(rareItems.Count);
                    tempItem = rareItems[itemNumber];
                }
                else
                    TakeUnknownObject();
                break;
            case ItemRarity.mythical:
                if (mythicalItems != null)
                {
                    itemNumber = rand.Next(mythicalItems.Count);
                    tempItem = mythicalItems[itemNumber];
                }
                else
                    TakeUnknownObject();
                break;
        }
        RenderItem();
    }

    private void RarityDetermination()
    {
        int randNumber = rand.Next(1, 101);
        if (randNumber <= 50)
            tempRarity = ItemRarity.common;
        else if (randNumber <= 85)
            tempRarity = ItemRarity.rare;
        else
            tempRarity = ItemRarity.mythical;
    }

    // Доп метод для предупреждения ошибок при загрузке сохранения
    //private void GetLinks()
    //{
    //    if (!started)
    //    {

    //        player = ReferencesToObjects.Player;
    //        rand = ReferencesToObjects.Rand;
    //        itemController = ReferencesToObjects.ItemController;

    //        started = true;
    //    }
    //}
    private void RenderItem()
    {
        itemScript = tempItem.GetComponent<SampleItem>();
        itemName.text = itemScript.Name;
        itemDescription.text = itemScript.Description;
        itemSprite.sprite = itemScript.Icon;
        itemCost.text = itemScript.GetCost().ToString();
    }

    private void TakeUnknownObject()
    {
        tempRarity = ItemRarity.unknownItem;
        tempItem = ItemUnknownObject;
    }
}
