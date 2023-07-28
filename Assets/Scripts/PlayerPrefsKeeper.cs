using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsKeeper : MonoBehaviour
{
    void Start()
    {
        // поставнока значений при 1 запуске игры
        if (!PlayerPrefs.HasKey("InfectedSouls"))
        {
            PlayerPrefs.SetInt("InfectedSouls", 0);// Валюта
            PlayerPrefs.SetInt("WarriorMaxHP", 100);// Максхп для война (10)
            PlayerPrefs.SetInt("WarriorAttack", 10);// Урон для война (3)
        }
    }
    public static void SaveInfectedSouls()
    {
        PlayerPrefs.SetInt("InfectedSouls", ReferencesToObjects.Player.InfectedSouls);
        PlayerPrefs.Save();
    }
}
