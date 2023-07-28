using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsKeeper : MonoBehaviour
{
    void Start()
    {
        // ���������� �������� ��� 1 ������� ����
        if (!PlayerPrefs.HasKey("InfectedSouls"))
        {
            PlayerPrefs.SetInt("InfectedSouls", 0);// ������
            PlayerPrefs.SetInt("WarriorMaxHP", 100);// ������ ��� ����� (10)
            PlayerPrefs.SetInt("WarriorAttack", 10);// ���� ��� ����� (3)
        }
    }
    public static void SaveInfectedSouls()
    {
        PlayerPrefs.SetInt("InfectedSouls", ReferencesToObjects.Player.InfectedSouls);
        PlayerPrefs.Save();
    }
}
