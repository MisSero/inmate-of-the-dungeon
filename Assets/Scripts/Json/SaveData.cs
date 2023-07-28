using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    [SerializeField] private int lvlNumber;
    [SerializeField] private int hp;
    [SerializeField] private int attack;
    [SerializeField] private int critChance;
    [SerializeField] private int missChance;
    [SerializeField] private float vampirism;
    [SerializeField] private int exp;
    [SerializeField] private int lvl;
    [SerializeField] private int randSeed;
    [SerializeField] private int cooldownNow;
    [SerializeField] private int cooldownTotal;
    [SerializeField] private int skillPoints;
    [SerializeField] private List<int> receivedSkills;
    [SerializeField] private List<int> receivedCommonItems;
    [SerializeField] private List<int> receivedRareItems;
    [SerializeField] private List<int> receivedMythicalItems;

    //Поля для предметов и их влияния
    [SerializeField] private float hpPotionMultiply;
    [SerializeField] private bool phoenixFeather;
    [SerializeField] private bool hourglass;
    [SerializeField] private bool crackedHourglass;


    // Поля для Warrior
    [SerializeField] private int armor;
    [SerializeField] private int fixedAttack;
    [SerializeField] private int block;
    public int LvlNumber
    {
        get { return lvlNumber; }
        private set { lvlNumber = value; }
    }
    public int Hp
    {
        get { return hp; }
        private set { hp = value; }
    }
    public int Attack
    {
        get { return attack; }
        private set { attack = value; }
    }
    public int CritChance
    {
        get { return critChance; }
        private set { critChance = value; }
    }
    public int MissChance
    {
        get { return missChance; }
        private set { missChance = value; }
    }
    public float Vampirism
    {
        get { return vampirism; }
        private set { vampirism = value; }
    }
    public int Exp
    {
        get { return exp; }
        private set { exp = value; }
    }
    public int Lvl
    {
        get { return lvl; }
        private set { lvl = value; }
    }
    public int RandSeed
    {
        get { return randSeed; }
        private set { randSeed = value; }
    }
    public int CooldownNow
    {
        get { return cooldownNow; }
        private set { cooldownNow = value; }
    }
    public int CooldownTotal
    {
        get { return cooldownTotal; }
        private set { cooldownTotal = value; }
    }
    public int SkillPoints
    {
        get { return skillPoints; }
        private set { skillPoints = value; }
    }
    public List<int> ReceivedSkills
    {
        get { return receivedSkills; }
        private set { receivedSkills = value; }
    }
    public List<int> ReceivedCommonItems
    {
        get { return receivedCommonItems; }
        private set { receivedCommonItems = value; }
    }
    public List<int> ReceivedRareItems
    {
        get { return receivedRareItems; }
        private set { receivedRareItems = value; }
    }
    public List<int> ReceivedMythicalItems
    {
        get { return receivedMythicalItems; }
        private set { receivedMythicalItems = value; }
    }

    // Сохранение предметов и их влияния
    public float HpPotionMultiply
    {
        get { return hpPotionMultiply; }
        private set { hpPotionMultiply = value; }
    }
    public bool PhoenixFeather
    {
        get { return phoenixFeather; }
    }
    public bool Hourglass
    {
        get { return hourglass; }
    }
    public bool CrackedHourglass
    {
        get { return crackedHourglass; }
    }


    // Войн
    public int Armor
    {
        get { return armor; }
        private set { armor = value; }
    }
    public int FixedAttack
    {
        get { return fixedAttack; }
        private set { fixedAttack = value; }
    }
    public int Block
    {
        get { return block; }
        private set { block = value; }
    }

    private Player player;
    private LevelController levelController;

    public void SetData()
    {
        player = ReferencesToObjects.Player;
        levelController = player.LevelController;
        lvlNumber = ReferencesToObjects.LvlNumber;
        randSeed = ReferencesToObjects.RandSeed;
        hp = player.HealthPoints;
        attack = player.Attack;
        critChance = player.CritChance;
        missChance = player.MissChance;
        vampirism = player.Vampirism;
        exp = levelController.CurrentExp;
        lvl = levelController.CurrentLvl;
        skillPoints = levelController.SkillPoints;
        cooldownNow = player.Ultimate.NowCooldown;
        cooldownTotal = player.Ultimate.TotalCooldown;
        receivedSkills = GetSkillController.receivedSkills;
        receivedCommonItems = GetItemController.receivedCommonItems;
        receivedRareItems = GetItemController.receivedRareItems;
        receivedMythicalItems = GetItemController.receivedMythicalItems;

        hpPotionMultiply = HpPotion.hpMultiply;
        phoenixFeather = player.PhoenixFeather;
        hourglass = player.Hourglass;
        crackedHourglass = player.CrackedHourglass;

        if(player is Warrior warrior)
        {
            armor = warrior.Armor;
            fixedAttack = warrior.FixedAttack;
            block = warrior.Block;
        }
    }
}
