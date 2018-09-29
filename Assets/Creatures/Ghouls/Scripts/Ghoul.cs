using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghoul : CreatureBehavuourController {
    public HatredCurveTemplate template;
    public GameObject plate;
    Coroutine currentBehave;
    bool isNearTarget = false;
    protected override void Awake()
    {
        base.Awake();
        hatredCurve = template.mobsCurve;
    }
    public override IEnumerator Die()
    {
        GetComponent<Animator>().SetTrigger("Dead");
        StopCoroutine(currentBehave);
        yield return new WaitForSecondsRealtime(5);
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
        currentBehave = StartCoroutine(startBehave());
        StartCoroutine(switchTarget());
    }
    void plateDsapr()
    {
        plate.SetActive(false);
    }
    IEnumerator startBehave()
    {
        yield return new WaitForSeconds(4.04f);
        plateDsapr();
        currentBehave = StartCoroutine(behaveUpdate());
    }
    IEnumerator behaveUpdate()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        foreach(var hit in Physics.RaycastAll(ray, attackDis))
        {
            if(hit.collider.gameObject == target)
            {
                isNearTarget = true;
                GetComponent<Animator>().CrossFade("AttackUnarmed [7]", 0.1f);
                StartCoroutine(Attack());
                yield return new WaitForSecondsRealtime(1.5f);
                break;
            }
        }
        if(!isNearTarget)
        {
            GetComponent<Animator>().CrossFade("Run [6]", 0);
            Vector3 dir = target.transform.position - transform.position;
            dir.y = 0;
            transform.position += dir.normalized * Time.deltaTime;
        }
        yield return new WaitForEndOfFrame();
        currentBehave = StartCoroutine(behaveUpdate());
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
