using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagnarosPhyAttack : HeroSkill, ISkill
{
    public AudioClip die;
    bool isDoubleAttack;
    public Sulfuars sulfuars;
    public override void StartSkill(Animator animator)
    {
        hero.audioCtrler.PlaySound(die, 0.5f);
        isDoubleAttack = false;
        sulfuars.TurnOnPhyAttack();
        StartCoroutine(WatchDog(animator));
    }
    IEnumerator WatchDog(Animator animator)
    {
        if (GameController.LeftInputListener.GetSkill(formula))
        {
            isDoubleAttack = true;
            animator.SetTrigger("PhyAttack");
        }
        else
        {
            yield return new WaitForEndOfFrame();
            StartCoroutine(WatchDog(animator));
        }
    }
    public override void StopSkill(Animator animator)
    {
        if(isDoubleAttack)
        {
            sulfuars.TurnOnPhyAttack();
        }
        StopAllCoroutines();
    }
    public override bool IsReady()
    {
        if (!Lock)
            return false;
        if (!GameController.LeftInputListener.GetSkill(formula))
            return false;
        return true;
    }
    public override bool TryStartSkill(Animator animator)
    {
        if (IsReady())
        {
            animator.SetTrigger("PhyAttack");
            return true;
        }
        else
            return false;
    }
}
