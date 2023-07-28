using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;

public class ObjectsKeeper : MonoBehaviour
{
    [SerializeField] private GameObject playerKeeper;
    [SerializeField] private List<GameObject> objectsToOpen;
    [SerializeField] private List<GameObject> objects;


    private void Start()
    {
        foreach (var item in objectsToOpen)// добавление к objects остальных объектов, которые находятся в objectsToOpen
            objects.Add(item);

        foreach(GameObject item in objects)
            DontDestroyOnLoad(item);

        objectsToOpen.Insert(0, ReferencesToObjects.PlayerObject);
    }

    // Вкл по кнопке.
    public void EnableObjects()
    {
        foreach (GameObject item in objectsToOpen)
        {
            item.SetActive(true);
        }

        objectsToOpen = null;
    }
    public void DisableObjects()
    {
        ReferencesToObjects.LvlNumber = 0;
        Destroy(playerKeeper);
        Destroy(gameObject);
        foreach (GameObject item in objects)
        {
            Destroy(item);
        }

        HpPotion.hpMultiply = 1;// Преобразование к начальному значению,что бы не стакалась через главное меню
    }
}
