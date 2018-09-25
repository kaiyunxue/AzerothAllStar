//Programer: KevinX
//code date:9/24/2018
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Skeleton : CreatureBehavuourController
{
    protected override void Awake()
    {
        base.Awake();
        hatredCurve = ConstHatredCurve.instance.GetMobCurve();
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
	IEnumerator startBehave()
    {
        yield return new WaitForSeconds(0.2f); //the time the mob will wait for the birth animation;
        StartCoroutine(behaveUpdate()); //update
    }
    IEnumerator behaveUpdate()
    {
        yield return new WaitForEndOfFrame();
        //do something
        Ray ray = new Ray(transform.position, transform.forward);
        bool isNearTarget = false; ;
        foreach (var hit in Physics.RaycastAll(ray, 1))
        {
            if (hit.collider.gameObject == target)
            {
                GetComponent<Animator>().SetBool("Attack", true);
                isNearTarget = true;
                break;
            }
        }
        if (!isNearTarget)
        {
            GetComponent<Animator>().SetBool("Attack", false);
            Vector3 dir = target.transform.position - transform.position;
            dir.y = 0;
            transform.position += dir.normalized * Time.deltaTime * 0.4f;
        }
        StartCoroutine(behaveUpdate());
    }




	//OVERRIDE FUNCTIONS
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
		Vector3 targetPos = target.transform.position;
        targetPos.y = transform.position.y;
        transform.LookAt(targetPos);
    }
    protected override KOFItem getMaxHatredObject()
    {
        return base.getMaxHatredObject();
    }
    public override int GetMaxInstance()
    {
        return 100;
    }
	protected override IEnumerator switchTarget()
    {
        return base.switchTarget();
    }
}
