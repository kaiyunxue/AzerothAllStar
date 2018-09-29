//Programer: KevinX
//code date:9/22/2018
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ZombieTank : CreatureBehavuourController
{
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
    }
	private void Start()
    {
        SetTarget(getMaxHatredObject().gameObject);
        StartCoroutine(startBehave());
        StartCoroutine(switchTarget());
    }

    IEnumerator startBehave()
    {
        yield return new WaitForSeconds(5.5f); //the time the mob will wait for the birth animation;
        plane.SetActive(false);
        StartCoroutine(behaveUpdate()); //update
    }
    IEnumerator behaveUpdate()
    {
        yield return new WaitForEndOfFrame();
        //do something
        Ray ray = new Ray(transform.position, transform.forward);
        bool isNearTarget = false;
        foreach (var hit in Physics.RaycastAll(ray, 1.3f))
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
            transform.position += dir.normalized * Time.deltaTime / 5;
        }
        StartCoroutine(behaveUpdate());
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
        return 20;
    }
	protected override IEnumerator switchTarget()
    {
        return base.switchTarget();
    }
}
