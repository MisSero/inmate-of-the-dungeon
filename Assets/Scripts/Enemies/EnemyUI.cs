using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    [SerializeField] private Text hp;
    [SerializeField] private Text attack;

    //[SerializeField] private int textSize = 23;
    //[SerializeField] private Font textFont;
    //[SerializeField] private Color textColor = Color.white;

    //[SerializeField] private Texture2D spriteHp;
    //[SerializeField] private Texture2D spriteAttack;

    //[SerializeField] private float yForHp = 8;
    //[SerializeField] private float yForAttack = 4;
    //[SerializeField] private float xForHp = -33;
    //[SerializeField] private float xForAttack = 5;


    //[SerializeField] private float xForAttackText = 48;
    //[SerializeField] private float xForHpText = -47;
    //[SerializeField] private float yForText = 48;

    private bool started;
    private Enemy enemy;
    //private Vector2 nativeSize = new Vector2(640, 480);

    private void Start()
    {
        enemy = gameObject.GetComponentInParent<Enemy>();
        hp.text = enemy.HealthPoints.ToString();
        attack.text = enemy.Attack.ToString();
        started = true;
    }
    private void Update()
    {
        if (hp.text != enemy.HealthPoints.ToString() || attack.text != enemy.Attack.ToString())
        {
            hp.text = enemy.HealthPoints.ToString();
            attack.text = enemy.Attack.ToString();
        }
    }

    //private void OnGUI()
    //{
    //    GUI.depth = 1;
    //    GUIStyle style = new GUIStyle();
    //    style.fontSize = textSize;
    //    style.richText = true;
    //    if (textFont)
    //        style.font = textFont;
    //    style.normal.textColor = textColor;
    //    style.alignment = TextAnchor.MiddleCenter;

    //    Vector3 scale = new Vector3(Screen.width / nativeSize.x, Screen.height / nativeSize.y, 1.0f);
    //    GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, scale);

    //    Vector3 worldPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    //    Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);
    //    //screenPosition.y = Screen.height - screenPosition.y;

    //    //GUI.Label(new Rect(screenPosition.x + xForHp, screenPosition.y + yForHp, spriteHp.width, spriteHp.height), spriteHp, style);
    //    //GUI.Label(new Rect(screenPosition.x + xForAttack, screenPosition.y + yForAttack, spriteAttack.width, spriteAttack.height), spriteAttack, style);

    //    GUI.Label(new Rect(screenPosition.x + xForHpText, screenPosition.y + yForText, 0, 0), hp, style);
    //    GUI.Label(new Rect(screenPosition.x + xForAttackText, screenPosition.y + yForText, 0, 0), attack, style);
    //}
    private void OnBecameInvisible()
    {
        enabled = false;
    }
    private void OnBecameVisible()
    {
        if(started)
            enabled = true;
    }
}
