using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonZombie : HeroSkill
{
    public Weapon weapon;
    protected override void Awake()
    {
        base.Awake();
    }
    public override void StartSkill(Animator animator)
    {
        throw new NotImplementedException();
    }

    public override void StopSkill(Animator animator, bool isBreak = false)
    {
        throw new NotImplementedException();
    }

    public override bool TryStartSkill(Animator animator)
    {
        throw new NotImplementedException();
    }
    public override bool IsReady()
    {
        return base.IsReady();
    }
    public override string GetFormula()
    {
        return base.GetFormula();
    }
}
