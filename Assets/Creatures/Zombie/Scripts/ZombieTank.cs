//Programer: KevinX
//code date:9/22/2018
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ZombieTank : CreatureBehavuourController
{
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
    }
    public override IEnumerator Die()
    {
        return base.Die();
    }
    protected override IEnumerator Live()
    {
        return base.Live();
    }
    public override void SetTarget(GameObject target)
    {
        base.SetTarget(target);
    }
    protected override KOFItem getMaxHatredObject()
    {
        return base.getMaxHatredObject();
    }
    public override int GetMaxInstance()
    {
        return base.GetMaxInstance();
    }
}
