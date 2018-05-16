using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagnarosFireTrap : HeroSkill, ISkill
{
    //The damage of firtrap is in the Effect item script: firtrap.
    //#region Only for test
    //[Header("Damage")]
    //public float damageVal;
    //public DamageType damageType;
    //#endregion 

    public FireTrap trap;
    FireTrap instance;
    public void StartBehave()
    {
        float x = UnityEngine.Random.Range(-6f, 2f);
        Vector3 RandomPosition = new Vector3(x, 0.2f, 0);
        instance = KOFItem.InstantiateByPool(trap,RandomPosition, GameController.instance.transform, gameObject.layer);
    }

    public override void StartSkill(Animator animator)
    {
        hero.state.Mana -= manaCost;
        StartBehave();
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
        if (hero.state.Mana < manaCost)
            return false;
        return true;
    }
    public override bool TryStartSkill(Animator animator)
    {
        if (IsReady())
        {
            animator.SetTrigger("FireTrap");
            return true;
        }
        else
            return false;
    }
}

