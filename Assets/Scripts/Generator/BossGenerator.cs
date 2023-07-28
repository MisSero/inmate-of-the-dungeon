using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] bosses;
    [SerializeField] private Transform[] cellsPlatformBoss;
    [SerializeField] private Transform[] transformAttackCells;
    [SerializeField] private Transform parent;


    void Start()
    {
        GameObject boss = Instantiate(bosses[Random.Range(0, bosses.Length)], parent);
        Boss bossScript = boss.GetComponent<Boss>();
        bossScript.SetCellsToAttack(transformAttackCells);
        bossScript.SetBossPlatformCells(cellsPlatformBoss);
    }
}
