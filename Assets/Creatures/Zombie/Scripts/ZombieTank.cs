using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieTank : CreatureBehavuourController {
    protected override void Awake()
    {
        base.Awake();
    }
    public override IEnumerator Die()
    {
        return base.Die();
    }
    protected override KOFItem getMaxHatredObject()
    {
        return base.getMaxHatredObject();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
    }
    protected override IEnumerator Live()
    {
        return base.Live();
    }
    public override void SetTarget(GameObject target)
    {
        base.SetTarget(target);
    }
}
