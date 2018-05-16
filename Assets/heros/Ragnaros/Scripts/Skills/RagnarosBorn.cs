using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagnarosBorn : HeroSkill
{
    public AnimationCurve curve1;
    public AnimationCurve curve2;
    public Projector rune;
    public Transform firePool;
    public Material poolM;
    public GameObject fireCylinder;
    Vector3 aimPosition;
    protected override void Awake()
    {
        aimPosition = firePool.position;
        firePool.localPosition = new Vector3(0, -2, 0);
        poolM.SetFloat("_CutOff", 0);
    }
    public override void StartSkill(Animator animator)
    {
        foreach (ParticleSystem p in fireCylinder.GetComponentsInChildren<ParticleSystem>())
        {
            p.Play();
        }
        StartCoroutine(RuneEnlarge(0));
        StartCoroutine(PoolApear(0));
    }
    IEnumerator PoolApear(float time)
    {
        firePool.localPosition = new Vector3(0,curve2.Evaluate(time),0);
        yield return new WaitForEndOfFrame();
        StartCoroutine(PoolApear(time += Time.deltaTime));
    }
    IEnumerator RuneEnlarge(float time)
    {
        rune.orthographicSize = curve1.Evaluate(time);
        yield return new WaitForEndOfFrame();
        if (rune.orthographicSize >= 2)
            rune.orthographicSize = 2;
        else
            StartCoroutine(RuneEnlarge(time += Time.deltaTime));
    }
    public override void StopSkill(Animator animator)
    {
        StopAllCoroutines();
        foreach(ParticleSystem p in fireCylinder.GetComponentsInChildren<ParticleSystem>())
        {
            p.Stop();
        }
    }
    public override bool TryStartSkill(Animator animator)
    {
        throw new NotImplementedException();
    }
}
