using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagnarosFireBlower : HeroSkill {

    #region Only for test
    [Header("Damage")]
    public float damageVal;
    public DamageType damageType;
    #endregion 

    public FireBlower fireBlower;
    public override void StartSkill(Animator animator)
    {
        hero.state.Mana -= manaCost;
        var instance = KOFItem.InstantiateByPool(fireBlower,GameController.instance.transform, gameObject.layer);
        instance.damage = new RagnarosDamage(damageVal, damageType, gameObject.layer);
        StartCdColding();
        hero.statusBox.cdBar.StartCooling(skillIcon, cd);
    }
    public override void StopSkill(Animator animator)
    {
    }
    public override bool IsReady()
    {
        if (!GameController.LeftInputListener.GetSkill(formula))
            return false;
        if (!Lock)
            return false;
        if (hero.state.Stage != 0)
            return false;
        if (hero.state.Mana < manaCost)
            return false;
        return true;
    }
    public override bool TryStartSkill(Animator animator)
    {
        if (IsReady())
        {
            animator.SetBool("FireBlower", true);
            return true;
        }
        else
            return false;
    }
}
