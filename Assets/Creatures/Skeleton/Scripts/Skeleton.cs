//Programer: KevinX
//code date:9/24/2018
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Skeleton : CreatureBehavuourController
{
    Coroutine updateBehave;
    Coroutine attackBehave;
    Coroutine runBehave;
    Coroutine watchBehave;
    bool isNearTarget = false;
    protected override void Awake()
    {
        base.Awake();
        hatredCurve = ConstHatredCurve.instance.GetTankCurve();
    }
    public override IEnumerator Die()
    {
        GetComponent<Animator>().SetTrigger("Die");
        StopCoroutine(updateBehave);
        if (runBehave != null)
        {
            StopCoroutine(runBehave);
            runBehave = null;
        }
        if (attackBehave != null)
        {
            StopCoroutine(attackBehave);
            attackBehave = null;
        }

        yield return new WaitForSecondsRealtime(10);
        KOFItem.DestoryByPool(this);

    }
    public override int GetMaxInstance()
    {
        return 10;
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        SetTarget(getMaxHatredObject().gameObject);
        StartCoroutine(watchDis());
        updateBehave = StartCoroutine(startBehave());
        StartCoroutine(switchTarget());
    }
    IEnumerator startBehave()
    {
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(isOnSky());
        updateBehave = StartCoroutine(behaveUpdate());
    }
    IEnumerator watchDis()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        foreach (var hit in Physics.RaycastAll(ray, attackDis))
        {
            if (hit.collider.gameObject == target)
            {
                isNearTarget = true;
                yield return new WaitForEndOfFrame();
                watchBehave = StartCoroutine(watchDis());
                yield break;
            }
        }
        isNearTarget = false;
        yield return new WaitForEndOfFrame();
        watchBehave = StartCoroutine(watchDis());
    }
    IEnumerator attack()
    {
        GetComponent<Animator>().CrossFade("Attack1H [1]", 0.1f);
        StartCoroutine(Attack());
        yield return new WaitForSecondsRealtime(1f);

        GetComponent<Animator>().CrossFade("Stand [88] 0", 0.1f);
        yield return new WaitForSeconds(1f);
        attackBehave = StartCoroutine(attack());
        yield break;
    }
    IEnumerator run()
    {
        GetComponent<Animator>().CrossFade("Walk [24]", 0f);
        Vector3 dir = target.transform.position - transform.position;
        dir.y = 0;
        transform.position += dir.normalized * Time.deltaTime * state.speed;
        yield return new WaitForEndOfFrame();
        runBehave = StartCoroutine(run());
    }
    IEnumerator behaveUpdate()
    {
        GetComponent<Animator>().SetBool("OnSky", !isOnGround);
        if (!isOnGround)
        {
            if (runBehave != null)
            {
                StopCoroutine(runBehave);
                runBehave = null;
            }
            if (attackBehave != null)
            {
                StopCoroutine(attackBehave);
                attackBehave = null;
            }
            yield return new WaitForEndOfFrame();
            StartCoroutine(behaveUpdate());
            yield break;
        }
        if (isNearTarget)
        {
            if (attackBehave == null)
            {
                if (runBehave != null)
                {
                    StopCoroutine(runBehave);
                    runBehave = null;
                }
                if (attackBehave == null)
                    attackBehave = StartCoroutine(attack());
            }
        }
        else
        {
            if (attackBehave != null)
            {
                StopCoroutine(attackBehave);
                attackBehave = null;
            }
            if (runBehave == null)
                runBehave = StartCoroutine(run());
        }
        yield return new WaitForEndOfFrame();
        updateBehave = StartCoroutine(behaveUpdate());
    }
    IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.8f);
        if (isNearTarget)
        {
            target.GetComponent<State>().TakeSkillContent(new Damage(10));
        }
    }
    public override void SetTarget(GameObject target)
    {
        base.SetTarget(target);
        Vector3 targetPos = target.transform.position;
        targetPos.y = transform.position.y;
        transform.LookAt(targetPos);
    }
    protected override IEnumerator Live()
    {
        return base.Live();
    }
    protected override KOFItem getMaxHatredObject()
    {
        return base.getMaxHatredObject();
    }
    protected override IEnumerator isOnSky(float height = 0.2F)
    {
        return base.isOnSky(height);
    }
    protected override IEnumerator switchTarget()
    {
        return base.switchTarget();
    }
    public override IEnumerator watchForDeath()
    {
        return base.watchForDeath();
    }
}
