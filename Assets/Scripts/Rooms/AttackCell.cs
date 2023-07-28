using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCell : MonoBehaviour
{

    public Boss boss { get; set; }

    private Animator animator;

    static private int finishedAnimations1;
    static private int finishedAnimations2;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }
    public void FinishedAnimationFirstAttack()
    {
        finishedAnimations1++;
        if(finishedAnimations1 == boss.AttackedCells1.Count)
        {
            boss.ResetPool(1);
            finishedAnimations1 = 0;
        }
    }public void FinishedAnimationSecondAttack()
    {
        finishedAnimations2++;
        if(finishedAnimations2 == boss.AttackedCells2.Count)
        {
            boss.ResetPool(2);
            finishedAnimations2 = 0;
        }
    }
    public void StartAnimation(CellAnimations nameAnimation)
    {
        switch (nameAnimation)
        {
            case CellAnimations.Default:
                animator.SetTrigger("Default");
                break;
            case CellAnimations.Bite:
                animator.SetTrigger("Bite");
                break;
            case CellAnimations.JumpJolt:
                animator.SetTrigger("JumpJolt");
                break;
            case CellAnimations.Spawn:
                animator.SetTrigger("Spawn");
                break;
            case CellAnimations.Crossfire:
                animator.SetTrigger("Crossfire");
                break;
        }
    }
}
    public enum CellAnimations
{
    Default,
    Bite,
    JumpJolt,
    Spawn,
    Crossfire
}