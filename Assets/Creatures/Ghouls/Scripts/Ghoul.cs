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
    }
    private void Start()
    {
        SetTarget(getMaxHatredObject().gameObject);
        StartCoroutine(startBehave());
        StartCoroutine(switchTarget());
    }
    IEnumerator switchTarget()
    {
        SetTarget(getMaxHatredObject().gameObject);
        yield return new WaitForSeconds(1);
        StartCoroutine(switchTarget());
    }
    IEnumerator startBehave()
    {
        yield return new WaitForSeconds(4f);
        StartCoroutine(behaveUpdate());
    }
    IEnumerator behaveUpdate()
    {
        yield return new WaitForEndOfFrame();
        StartCoroutine(behaveUpdate());
    }
    public override void SetTarget(GameObject target)
    {
        base.SetTarget(target);
        Vector3 targetPos = target.transform.position;
        targetPos.y = transform.position.y;
        transform.LookAt(targetPos);
    }
}
