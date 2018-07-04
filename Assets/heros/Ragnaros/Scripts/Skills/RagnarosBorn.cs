using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagnarosBorn : HeroSkill
{
    public AudioClip word;
    public AnimationCurve curve1;
    public AnimationCurve curve2;
    public Projector rune;
    public Transform firePool;
    public Material poolM;
    public RagnarosFlame fireCylinder;
    Vector3 aimPosition;
    protected override void Awake()
    {
        base.Awake();
        aimPosition = firePool.position;
        firePool.localPosition = new Vector3(0, -2, 0);
        poolM.SetFloat("_CutOff", 0);
    }
    public override void StartSkill(Animator animator)
    {
        fireCylinder.Play();
        hero.audioCtrler.PlaySound(word);
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
    public override void StopSkill(Animator animator, bool isBreak = false)
    {
        StopAllCoroutines();
        fireCylinder.Stop();
    }
    public override bool TryStartSkill(Animator animator)
    {
        throw new NotImplementedException();
    }
}
