using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghoul : CreatureBehavuourController {
    public HatredCurveTemplate template;
    protected override void Awake()
    {
        base.Awake();
        hatredCurve = template.mobsCurve;
    }
    public override IEnumerator Die()
    {
        return base.Die();
    }
    public override int GetMaxInstance()
    {
        return 10;
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        SetTarget(getMaxHatredObject().gameObject);
    }
    public override void SetTarget(GameObject target)
    {
        base.SetTarget(target);
        transform.LookAt(target.transform);
    }
}
