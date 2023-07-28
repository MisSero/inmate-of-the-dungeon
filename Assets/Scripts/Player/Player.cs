using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    [SerializeField] private GameObject GameController;
    [SerializeField] private Text textHealthPoints;
    [SerializeField] private Text textAttack;
    [SerializeField] private Text textInfectedSouls;

    [SerializeField] private GameObject UpTrail;
    [SerializeField] private GameObject DownTrail;
    [SerializeField] private GameObject RightTrail;
    [SerializeField] private GameObject LeftTrail;


    [SerializeField] private GameObject SkillButton;// для получения ссылки на ultimate

    public List<GameObject> Enemies { get; set; }
    //public List<GameObject> Enemies;

    public float Vampirism { get; set; } // Показатель вампиризма ( в 0.0f)
    public int EnemiesKilled { get; set; }
    public int MaxHP { get; set; }
    public int Attack { get; set; }
    public int CritChance { get; set; } = 10;
    public int MissChance { get; set; } = 10;
    public int InfectedSouls { get; private set; }
    public int HealthPoints { get; protected set; }
    public int GetSoulsMultiply { get; set; } = 1; // множитель для получения душ(не сохраняется, изменения происходят из предметов\скиллов)
    public int ReduceCooldownMultiply { get; set; } = 1; // множитель для сокращения перезарядки(не сохраняется, изменения происходят из предметов\скиллов)
    public LevelController LevelController { get; private set; }
    public SamleUltimate Ultimate { get; protected set; } // ссылка на скрипт, для изменения перезарядки(ссылка берется из метода ниже), также подписка насобытие для UpdateCanvas()(В Warrior)

    public int MoveCounter { get; set; }

    private System.Random rand;
    private Animator animator;
    private Vector3 PositionToGo;
    private Transform tr;

    private readonly float speed = 12;
    private bool isMoving;
    


    //Items
    // Перо феникса для воскрешение(из метода Rebith)
    public bool PhoenixFeather { get; set; }
    // Компас(вызывается из ObjectGenerator.PointToExit)
    public bool Compass { get; set; }
    // Дьявольский глаз ( метод , вызываемый в начале атаки)
    public bool DevilsEye { get; set; }
    // Предмет песочные часы для возрождения(из метода Rebith)
    public bool Hourglass { get; set; }
    // Педмет треснувшие песочные часы для возрождения(из метода Rebith)
    public bool CrackedHourglass { get; set; }
    // Предмет ясный взор(для помечание врагов из класс ObjectGenerator)
    public bool ClearEyes { get; set; }
    // Предмет HandOfFate для кнопки реролла скиллов (кнопка вызывается из GetSkillConntroller)
    public bool HandOfFate { get; set; }
    // Предмет MagicMap(возможно позже и скиллы) для открытия карты(происходит в LevelGenerato.Awake)
    public bool MagicMap { get; set; }
    // Предмет Blood Magic ( Подписка на событие абилки в SubscribeUltimate(), а сам методо BloodMagicMethod)
    public bool BloodMagic { get; set; }
    // предмет NeedForEvolutuion (проверяется в методе NeedForEqvolutionMode, который вызывается из Enemy.IsAlive() и там же проверяется на колво врагов)
    public bool NeedForEvolution { get; set; }
    private readonly float NeedForEvolutionMultiply = 1.2f;
    // предмет EyeOfGod, работает как ClearEye( помимо доп характеристик)
    public bool EyeOfGod { get; set; }


    protected virtual void Start()
    {
        tr = gameObject.GetComponent<Transform>();
        animator = gameObject.GetComponent<Animator>();
        PositionToGo = tr.position;
        MoveCounter = 1;
        InfectedSouls = PlayerPrefs.GetInt("InfectedSouls");
        LevelController = new LevelController();
        Enemies = new List<GameObject>();

        Ultimate = SkillButton.GetComponent<SamleUltimate>();

        rand = ReferencesToObjects.Rand;

        UpdateCanvas();
        GetSavedData();
    }

    void Update()
    {

        if (tr.position != PositionToGo && !isMoving)
            Move();
        if (isMoving)
        {
            float step = speed * Time.deltaTime;
            tr.position = Vector3.MoveTowards(tr.position, PositionToGo, step);
            if (tr.position == PositionToGo)
            {
                isMoving = false;
                MoveCounter++;
            }
        }
    }

    public void GetSavedData()// должен вызываться отдельно в каждом наследнике(если будет перекрываться в зависимоти от необходимых полей)
    {
        if (ReferencesToObjects.Loaded)
        {
            SaveData saveData = ReferencesToObjects.SaveData;
            HealthPoints = saveData.Hp;
            Attack = saveData.Attack;
            CritChance = saveData.CritChance;
            MissChance = saveData.MissChance;
            LevelController.SetLvlAndEx(saveData.Lvl, saveData.Exp, saveData.SkillPoints);

            Ultimate.TotalCooldown = saveData.CooldownTotal;
            Ultimate.ActivateUltimate(saveData.CooldownNow);
            UpdateCanvas();
        }
    }
    public void SetPositionToGo(Vector3 position)
    {
        //проверка не застрял ли персонаж перед установелнием координат
        Vector3 nowPosition = tr.position;
        if (nowPosition.x % 1 != 0 || nowPosition.y % 1 != 0)
        {
            Debug.Log("Position Corrected. Was:" + nowPosition);
            tr.position = new Vector3(Mathf.Round(nowPosition.x), Mathf.Round(nowPosition.y));
        }
        PositionToGo = position;
    }
    public virtual void AttackEnemy(Enemy enemy, Vector3 positionEnemy)
    {

        // Проверка на наличие предметы DevilsEye и шанс в 40%
        if(DevilsEye && rand.Next(100) <= 40)
        {
            DevilsEyeMode();
            return;
        }
        Vector3 myPosition = tr.position;
        enemy.Attacted = true;
        if ((Mathf.Abs(myPosition.x - positionEnemy.x) == 1)
            && (myPosition.y - positionEnemy.y == 0) ||
                (myPosition.x - positionEnemy.x == 0)
            && (Mathf.Abs(myPosition.y - positionEnemy.y) == 1))
        {
            animator.SetTrigger("Attack");
            MoveCounter++;
            // Проверка на промах
            if(rand.Next(1, 101) <= MissChance)
            {
                enemy.DamagePlayer();
                return;
            }
            CheckPositionForTrail(myPosition, positionEnemy, false);
            if (enemy.ChangeHp(-Attack))
                enemy.DamagePlayer();
        }
        if (enemy is Boss)
        {
            if ((Mathf.Abs(myPosition.y - positionEnemy.y) <= 2)
            && (Mathf.Abs(myPosition.x - positionEnemy.x) <= 2))
            {
                animator.SetTrigger("Attack");
                MoveCounter++;
                CheckPositionForTrail(myPosition, positionEnemy, true);
                if (enemy.ChangeHp(-Attack))
                    enemy.DamagePlayer();
            }
        }
        if (enemy != null)
            enemy.Attacted = false;
    }
    // дополнительный метод атаки для глаза дьявола
    public void AttackEnemy(Enemy enemy)
    {
        enemy.Attacted = true;
        animator.SetTrigger("Attack");
        MoveCounter++;
        CheckPositionForTrail(tr.position, enemy.gameObject.transform.position, false);
        if (enemy.ChangeHp(-Attack))
            enemy.DamagePlayer();
        if (enemy != null)
            enemy.Attacted = false;
    }
    public virtual void ChangeHp(int howMuch)
    {
        HealthPoints += howMuch;
        if (HealthPoints > MaxHP)
            HealthPoints = MaxHP;
        if (HealthPoints <= 0)
            Die();
        UpdateCanvas();
    }
    public void TakeInfectedSoulsAndReduceCooldown(int souls)
    {
        TakeInfectedSouls(souls);
        Ultimate.ReduceCooldown(souls * 2 * ReduceCooldownMultiply);
    }
    public void TakeInfectedSouls(int souls)
    {
        InfectedSouls += souls * GetSoulsMultiply;
        UpdateCanvas();
    }
    public void TakeExp(int exp)
    {
        LevelController.TakeExp(exp);
    }
    // public для класса Exit
    public void Die()
    {
        if (Rebith())
            return;

        GameController.GetComponent<ObjectsKeeper>().DisableObjects();
        PlayerPrefsKeeper.SaveInfectedSouls();
        SceneManager.LoadScene("MenuScene");
        ReferencesToObjects.SaveAndResetScript.ResetGame();
    }
    // изменнёный метод Die() для игрового меню
    public void ExitToMenu()
    {
        GameController.GetComponent<ObjectsKeeper>().DisableObjects();
        SceneManager.LoadScene("MenuScene");
    }

    public virtual void UpdateCanvas()
    {
        textAttack.text = Attack.ToString();
        textHealthPoints.text = HealthPoints.ToString();
        textInfectedSouls.text = InfectedSouls.ToString();
    }

    public void SubscribeUltimate()
    {
        if (BloodMagic)
            Ultimate.EffectNotify += BloodMagicMethod;
    }

    protected void CheckPositionForTrail(Vector3 myposition, Vector3 positionEnemy, bool isBoss)
    {
        int x = 1;
        if (isBoss)
            x = 2;
        if (myposition.y + x == positionEnemy.y)
            StartCoroutine(ShowTrail(UpTrail, positionEnemy));
        else if (myposition.y - x == positionEnemy.y)
            StartCoroutine(ShowTrail(DownTrail, positionEnemy));
        else if (myposition.x + x == positionEnemy.x)
            StartCoroutine(ShowTrail(RightTrail, positionEnemy));
        else if (myposition.x - x == positionEnemy.x)
            StartCoroutine(ShowTrail(LeftTrail, positionEnemy));
    }

    private void Move()
    {
        if ((Mathf.Abs(tr.position.x - PositionToGo.x) == 1)
            && (tr.position.y - PositionToGo.y == 0) ||
                (tr.position.x - PositionToGo.x == 0)
            && (Mathf.Abs(tr.position.y - PositionToGo.y) == 1))
        {
            isMoving = true;
        }
    }
    // Метод для выбора врага при наличии глаза дьявола 
    private void DevilsEyeMode()
    {
        Enemy enemy = Enemies[rand.Next(Enemies.Count)].GetComponent<Enemy>();
        if (enemy == null)
            DevilsEyeMode();
        else
        {
            AttackEnemy(enemy);
        }
    }

    // Метод для возрождения с помощью многих предметов(может скиллов)
    private bool Rebith()
    {
        //Проверка на наличие пера феникса
        if (PhoenixFeather)
        {
            HealthPoints = MaxHP / 2;
            PhoenixFeather = false;
            return true;
        }
        //Проверка на наличие песочных часов
        if (Hourglass)
        {
            tr.position = new Vector3(0, 0);
            HealthPoints = 1;
            MaxHP = (int)(MaxHP * 1.5f);
            Attack = (int)(Attack * 1.5f);
            Hourglass = false;
            return true;
        }
        //Проверка на наличие треснувших песочных часов
        if (CrackedHourglass)
        {
            tr.position = new Vector3(0, 0);
            MaxHP = 1;
            HealthPoints = 1;
            Attack *= 5;
            CrackedHourglass = false;
            return true;
        }
        return false;
    }

    private void BloodMagicMethod()
    {
        ChangeHp(1);
    }

    public void NeedForEvolutionMode()
    {
        if (NeedForEvolution)
        {
            Debug.Log("NeddForEvolutionMode");
            MaxHP = Mathf.RoundToInt(MaxHP * NeedForEvolutionMultiply);
            UpdateCanvas();
        }
    }
    private IEnumerator ShowTrail(GameObject trail, Vector3 positionEnemy)
    {
        trail.SetActive(true);
        trail.transform.position = positionEnemy;
        yield return new WaitForSeconds(0.15f);
        trail.SetActive(false);
        yield break;
    }

}
