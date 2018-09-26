//Programer: KevinX
//code date:9/25/2018
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ForstWurm : SkillItemsBehaviourController
{
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
    }
    public override void SetTarget(GameObject target)
    {
        base.SetTarget(target);
    }
    protected override IEnumerator DestorySelf()
    {
        return base.DestorySelf();
    }
    public override IEnumerator Live()
    {
        return base.Live();
    }

    public override int GetMaxInstance()
    {
        return base.GetMaxInstance();
    }
}
