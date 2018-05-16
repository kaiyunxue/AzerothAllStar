using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagnarosFireDebuff : HeroSkill, ISkill
{
    public GameObject LeftHand;
    public GameObject target;
    public FireDebuff effect;
    public FireDebuff effectInstance;

    public override void StartSkill(Animator animator)
    {
        hero.state.Mana -= manaCost;
        effectInstance = KOFItem.InstantiateByPool(effect, LeftHand.transform, gameObject.layer);
        effectInstance.GetComponent<FireDebuff>().target = target;
        StartCoroutine(WaitTime());
    }
    public override void StopSkill(Animator animator)
    {
    }

    public override bool IsReady()
    {
        if (!Lock)
            return false;
        if (!GameController.LeftInputListener.GetSkill(formula))
            return false;
        if (hero.state.Mana < manaCost)
            return false;
        return true;
    }
    public override bool TryStartSkill(Animator animator)
    {
        if (IsReady())
        {
            animator.SetTrigger("FireDebuff");
            return true;
        }
        else
            return false;
    }

    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(effectInstance.GetComponent<FireDebuff>().GoFireDebuff(target)); 
    }
}

