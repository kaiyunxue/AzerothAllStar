using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagnarosStandAttack : HeroSkill, ISkill
{
    public Sulfuars sulfuars;
    void Start()
    {
        sulfuars = hero.weapon as Sulfuars;
    }
    public override void StartSkill(Animator animator)
    {
        animator.SetBool("Run", false);
        animator.SetBool("Back", false);
        sulfuars.TurnOnPhyAttack();
    }

    public override void StopSkill(Animator animator)
    {
        StartCdColding();
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
            StartSkill(animator);
            animator.SetTrigger("PhyAttack");
            return true;
        }
        else
            return false;
    }
}
