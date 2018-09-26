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
    GameObject explosion;
    [SerializeField]
    ParticleSystem trail;
    Coroutine followCoroutine;
    Coroutine shootCoroutine;
    public void Follow(Transform aim)
    {
        followCoroutine = StartCoroutine(follow(aim));
    }
    IEnumerator follow(Transform aim)
    {
        transform.position = aim.position;
        yield return new WaitForEndOfFrame();
        followCoroutine = StartCoroutine(follow(aim));
    }
    public void Shoot(GameObject target)
    {
        if (target.GetComponent<Hero>() != null)
            target = target.GetComponent<Hero>().head.gameObject;
        this.target = target;

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
    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Weapon>() != null)
            return;
        if(shootCoroutine != null && other.gameObject.layer != gameObject.layer)
        {
            StopCoroutine(shootCoroutine);
            StartCoroutine(DestorySelf());
        }
    }
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        explosion.SetActive(false);
    }
    public override void SetTarget(GameObject target)
    {
        base.SetTarget(target);
    }
    protected override IEnumerator DestorySelf()
    {
        trail.Stop();
        explosion.SetActive(true);
        yield return new WaitForSeconds(5);
        yield return base.DestorySelf();
    }
    public override IEnumerator Live()
    {
        return base.Live();
    }

    public override int GetMaxInstance()
    {
        return 30;
    }
}
