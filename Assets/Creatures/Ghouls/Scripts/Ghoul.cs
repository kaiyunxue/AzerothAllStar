using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghoul : CreatureBehavuourController {
    public HatredCurveTemplate template;
    public GameObject plate;
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
        plate.SetActive(true);
    }
    private void Start()
    {
        SetTarget(getMaxHatredObject().gameObject);
        StartCoroutine(startBehave());
        StartCoroutine(switchTarget());
    }
    void plateDsapr()
    {
        plate.SetActive(false);
    }
    IEnumerator startBehave()
    {
        yield return new WaitForSeconds(4f);
        plateDsapr();
        StartCoroutine(behaveUpdate());
    }
    IEnumerator behaveUpdate()
    {
        yield return new WaitForEndOfFrame();
        Ray ray = new Ray(transform.position, transform.forward);
        bool isNearTarget = false; ;
        foreach(var hit in Physics.RaycastAll(ray, 1))
        {
            if(hit.collider.gameObject == target)
            {
                GetComponent<Animator>().SetBool("Attack", true);
                isNearTarget = true;
                break;
            }
        }
        if(!isNearTarget)
        {
            GetComponent<Animator>().SetBool("Attack", false);
            Vector3 dir = target.transform.position - transform.position;
            dir.y = 0;
            transform.position += dir.normalized * Time.deltaTime;
        }
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
