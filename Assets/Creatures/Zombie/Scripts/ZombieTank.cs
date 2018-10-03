//Programer: KevinX
//code date:9/22/2018
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ZombieTank : CreatureBehavuourController
{
    Coroutine updateBehave;
    Coroutine attackBehave;
    Coroutine runBehave;
    Coroutine watchBehave;
    bool isNearTarget = false;
    public GameObject plane;
    protected override void Awake()
    {
        base.Awake();
        hatredCurve = ConstHatredCurve.instance.GetTankCurve();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        plane.SetActive(true);
        SetTarget(getMaxHatredObject().gameObject);
        StartCoroutine(watchDis());
        updateBehave = StartCoroutine(startBehave());
        StartCoroutine(switchTarget());
    }

    IEnumerator startBehave()
    {
        yield return new WaitForSeconds(5.5f); //the time the mob will wait for the birth animation;
        plane.SetActive(false);
        StartCoroutine(isOnSky(0.8f));
        updateBehave = StartCoroutine(behaveUpdate());
    }
    protected override IEnumerator isOnSky(float height = 0.2F)
    {
        return base.isOnSky(height);
    }
    public override IEnumerator watchForDeath()
    {
        return base.watchForDeath();
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
        GetComponent<Animator>().CrossFade("Attack1H [2]", 0.1f);
        StartCoroutine(Attack());
        yield return new WaitForSecondsRealtime(1.5f);

        GetComponent<Animator>().CrossFade("Stand [90]", 0.1f);
        yield return new WaitForSeconds(1.1f);
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
    IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.8f);
        if (isNearTarget)
        {
            target.GetComponent<State>().TakeSkillContent(new Damage(30));
        }
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
    protected override IEnumerator Live()
    {
        return base.Live();
    }
    public override void SetTarget(GameObject target)
    {
        base.SetTarget(target);
        Vector3 targetPos = target.transform.position;
        targetPos.y = transform.position.y;
        transform.LookAt(targetPos);
    }

    public override int GetMaxInstance()
    {
        return 20;
    }
	protected override IEnumerator switchTarget()
    {
        return base.switchTarget();
    }
}
