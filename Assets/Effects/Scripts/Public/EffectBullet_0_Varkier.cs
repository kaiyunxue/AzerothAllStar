//Programer: KevinX
//code date:9/26/2018
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EffectBullet_0_Varkier : SkillItemsBehaviourController
{
    public float speed;
    [SerializeField]
    GameObject trials;
    Coroutine followCoroutine;
    Coroutine shootCoroutine;
    public void Follow(Transform aim)
    {
        followCoroutine = StartCoroutine(follow(aim));
        trials.SetActive(false);
    }
    IEnumerator follow(Transform aim)
    {
        transform.position = aim.position;
        yield return new WaitForEndOfFrame();
        followCoroutine = StartCoroutine(follow(aim));
    }
    public void Shoot(GameObject target)
    {
        this.target = target;
        trials.SetActive(true);
        StopCoroutine(followCoroutine);
        shootCoroutine = StartCoroutine(shoot());
    }
    public IEnumerator shoot()
    {
        Vector3 dir = target.transform.position - transform.position;
        transform.position += dir.normalized * speed * Time.deltaTime;
        yield return new WaitForEndOfFrame();
        shootCoroutine = StartCoroutine(shoot());
    }
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
    }
    public override void SetTarget(GameObject target)
    {
        base.SetTarget(target);
    }
    protected override IEnumerator DestorySelf()
    {
        return base.DestorySelf();
    }
    public override IEnumerator Live()
    {
        return base.Live();
    }

    public override int GetMaxInstance()
    {
        return base.GetMaxInstance();
    }
}
