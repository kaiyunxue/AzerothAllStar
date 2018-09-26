//Programer: KevinX
//code date:9/26/2018
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Varkier : CreatureBehavuourController
{
    public float spellDis;
    public float reviveDis;
    public float speed;

    [SerializeField]
    Transform spellHand;

    [SerializeField]
    EffectBullet_0_Varkier bullet;
    EffectBullet_0_Varkier bulletInstance;

    bool hasBullet = false;

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
        yield return new WaitForSeconds(0.1f); //the time the mob will wait for the birth animation;
		//do something

        StartCoroutine(behaveUpdate()); //update
    }
    IEnumerator behaveUpdate()
    {
        //do something


        //Only a reference
        if (Vector3.Distance(transform.position, target.transform.position) <= spellDis)
        {
            GetComponent<Animator>().CrossFade("FlyCustomSpell06 [106]", 0.1f);
            if(!hasBullet)
            {
                hasBullet = true;
                bulletInstance = KOFItem.InstantiateByPool(bullet, GameController.instance.transform, gameObject.layer);
                bulletInstance.Follow(this.spellHand);
                yield return new WaitForSeconds(1f);
                GetComponent<Animator>().CrossFade("FlyCustomSpell07 [107]", 0.1f);
                yield return new WaitForSeconds(0.5f);
                bulletInstance.Shoot(this.target);
                yield return new WaitForSeconds(0.4f);
                GetComponent<Animator>().CrossFade("Hover [101] 0", 0.1f);
                yield return new WaitForSeconds(1);
                hasBullet = false;
            }
        }
        else
        {
            GetComponent<Animator>().CrossFade("Hover [101]", 0.1f);
            Vector3 dir = target.transform.position - transform.position;
            transform.position += dir.normalized * Time.deltaTime * speed;
        }
        yield return new WaitForEndOfFrame();
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
        transform.LookAt(target.transform.position);
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
