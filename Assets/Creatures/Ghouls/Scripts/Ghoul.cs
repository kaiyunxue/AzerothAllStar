using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghoul : CreatureBehavuourController {
    public HatredCurveTemplate template;
    public GameObject plate;
    Coroutine updateBehave;
    Coroutine attackBehave;
    Coroutine runBehave;
    Coroutine watchBehave;
    bool isNearTarget = false;
    protected override void Awake()
    {
        base.Awake();
        hatredCurve = template.mobsCurve;
    }
    public override IEnumerator Die()
    {
        GetComponent<Animator>().SetTrigger("Dead");
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
        plate.SetActive(true);
        SetTarget(getMaxHatredObject().gameObject);
        StartCoroutine(watchDis());
        updateBehave = StartCoroutine(startBehave());
        StartCoroutine(switchTarget());
    }
    void plateDsapr()
    {
        plate.SetActive(false);
    }
    IEnumerator startBehave()
    {
        yield return new WaitForSeconds(4.04f);
        StartCoroutine(isOnSky());
        plateDsapr();
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
        GetComponent<Animator>().CrossFade("AttackUnarmed [7]", 0.1f);
        StartCoroutine(Attack());
        yield return new WaitForSecondsRealtime(0.75f);

        GetComponent<Animator>().CrossFade("Stand [1]", 0.1f);
        yield return new WaitForSeconds(0.3f);
        attackBehave = StartCoroutine(attack());
        yield break;
    }
    IEnumerator run()
    {
        GetComponent<Animator>().CrossFade("Run [6]", 0f);
        Vector3 dir = target.transform.position - transform.position;
        dir.y = 0;
        transform.position += dir.normalized * Time.deltaTime;
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
        if(isNearTarget)
        {
            if(attackBehave == null)
            {
                if(runBehave != null)
                {
                    StopCoroutine(runBehave);
                    runBehave = null;
                }
                if(attackBehave == null)
                    attackBehave = StartCoroutine(attack());
            }
        }
        else
        {
            if(attackBehave != null)
            {
                StopCoroutine(attackBehave);
                attackBehave = null;
            }
            if(runBehave == null)
                runBehave =  StartCoroutine(run());
        }
        yield return new WaitForEndOfFrame();
        updateBehave = StartCoroutine(behaveUpdate());
    }
    IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.4f);
        if (isNearTarget)
        {
            target.GetComponent<State>().TakeSkillContent(new Damage(30));
        }
    }
    public override void SetTarget(GameObject target)
    {
        base.SetTarget(target);
        Vector3 targetPos = target.transform.position;
        targetPos.y = transform.position.y;
        transform.LookAt(targetPos);
    }
}
