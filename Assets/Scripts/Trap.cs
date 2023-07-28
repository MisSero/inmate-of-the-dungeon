using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private Sprite activeSprite;
    [SerializeField] private Sprite inactiveSprite;

    private Player player;
    private SpriteRenderer spriteRenderer;
    private bool playerOnTrap;
    private bool isActive;
    private int damage = 2;
    void Start()
    {
        player = ReferencesToObjects.Player;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        StartCoroutine(TrapActivity());
    }
    private IEnumerator TrapActivity()
    {
        while (true)
        {
            spriteRenderer.sprite = inactiveSprite;
            isActive = false;
            yield return new WaitForSeconds(1);

            spriteRenderer.sprite = activeSprite;
            isActive = true;
            StartCoroutine(DamageStanding());
            yield return new WaitForSeconds(1);
            StopCoroutine(DamageStanding());

        }
    }
    private IEnumerator DamageStanding()
    {
        while (isActive)
        {
            if(playerOnTrap)
            {
                player.ChangeHp(-damage);
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(0.1f);
        }
        yield break;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerOnTrap = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        playerOnTrap = false;
    }
}
