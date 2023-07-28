using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferencesToObjects : MonoBehaviour
{
    [SerializeField] private GameObject[] arrayItems;
    [SerializeField] private GameObject GameController;
    [SerializeField] private GameObject canvas; // 1) для вкл. стрта LvlUpVisualizer при загрузке сейва
    [SerializeField] private GameObject itemMenu; // для получения ссылки на GetItemConroller
    [SerializeField] private GameObject mainCamera; // для получения ссылки на камеру(что бы воздейстовать на неё из предметов)

    public static System.Random Rand { get; set; } // создаётся в awake, меняется при каждой смене уровня
    public static GameObject[] ItemsToDrop { get; set; }
    public static GameObject Canvas;
    public static SaveData SaveData { get; set; } // ниже создаётся экземпляр для сейва
    public static int RandSeed { get; set; }
    public static GameObject PlayerObject { get; private set; } // геймобжект непосредственно героя
    public static Player Player { get; private set; } //  скрипт героя
    public static Camera MainCamera { get; private set; } //  Камера
    public static ObjectsKeeper ObjectsKeeper { get; private set; } 
    public static SaveAndResetGame SaveAndResetScript { get; private set; } 
    public static GetItemController ItemController { get; private set; } 
    public static int LvlNumber { get; set; }
    public static bool Loaded { get; set; } // отвечает за прибаление lvlnumber в levelgenerator и за смену показаетлей у player; Смена на false происходит у Exit
    private GameObject PlayerKeeper { get; set; }

    private static readonly int maxLvl = 4;
    private void Awake()
    {
        RandSeed = (int)DateTime.Now.Ticks;
        Rand = new System.Random(RandSeed);
        DontDestroyOnLoad(gameObject);
        PlayerKeeper = gameObject;
        if(PlayerKeeper.transform.childCount > 0)
            PlayerObject = PlayerKeeper.transform.GetChild(0).gameObject;
        Player = PlayerObject.GetComponent<Player>();
        ItemsToDrop = arrayItems;
        ObjectsKeeper = GameController.GetComponent<ObjectsKeeper>();
        SaveAndResetScript = GameController.GetComponent<SaveAndResetGame>();
        SaveData = new SaveData();
        Canvas = canvas;
        ItemController = itemMenu.GetComponent<GetItemController>();
        MainCamera = mainCamera.GetComponent<Camera>();

    }
    static public int GetMaxLvl()
    {
        return maxLvl;
    }
}

