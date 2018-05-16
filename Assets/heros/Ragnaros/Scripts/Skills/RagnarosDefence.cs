
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagnarosDefence : HeroSkill, ISkill
{
    public override void StartSkill(Animator animator)
    {
        animator.SetBool("Run", false);
        animator.SetBool("Back", false);
        StartCoroutine(SkillUpdate(animator));
    }
    protected override IEnumerator SkillUpdate(Animator animator)
    {
        yield return new WaitForEndOfFrame();
        if (!InputListener.GetKey(KeyCode.L))
        {
            animator.SetBool("Defence", false);
        }
        else
            StartCoroutine(SkillUpdate(animator));
    }
    public override void StopSkill(Animator animator)
    {
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
            animator.SetTrigger("Defence");
            return true;
        }
        else
            return false;
    }
}
