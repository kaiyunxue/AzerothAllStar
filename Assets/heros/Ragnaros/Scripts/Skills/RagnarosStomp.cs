using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagnarosStomp : HeroSkill, ISkill
{
    [SerializeField]
    FieryHell wave;
    [SerializeField]
    Transform foot;
    public override void StartSkill(Animator animator)
    {
        hero.state.Mana--;
        animator.SetBool("Run", false);
        animator.SetBool("Back", false);
        StartCoroutine(Stomp());
    }

    public override void StopSkill(Animator animator)
    {
    }

    public override bool IsReady()
    {
        if (!Lock)
            return false;
        if (hero.state.Mana < manaCost)
            return false;
        if (!GameController.LeftInputListener.GetSkill(formula))
            return false;
        return true;
    }
    IEnumerator Stomp()
    {
        yield return new WaitForSeconds(0.3f);
        KOFItem.InstantiateByPool(wave,foot.position , Quaternion.Euler(Vector3.zero),GameController.instance.transform, gameObject.layer, true);

    }
    public override bool TryStartSkill(Animator animator)
    {
        if (IsReady())
        {
            StartSkill(animator);
            animator.SetTrigger("Stomp");
            return true;
        }
        else
            return false;
    }
}
