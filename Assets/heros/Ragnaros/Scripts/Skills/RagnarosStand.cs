using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagnarosStand : HeroSkill, ISkill
{
    public AudioClip standWord;
    public AnimationCurve valCurve;
    public GameObject fireStand;
    public GameObject rune;
    public GameObject firePool;
    public RagnarosFlame fire;
 
    public new bool IsReady()
    {
        return true;
    }
    private void Start()
    {
        
    }


    public override void StartSkill(Animator animator)
    {
        fire.Play();
        hero.audioCtrler.ForcePlaySound(standWord);
        StartCoroutine(FirePoolSink());
        StartCoroutine(RuneDispare(0));
    }
    IEnumerator FirePoolSink()
    {
        yield return null;
        firePool.transform.localPosition -= new Vector3(0, 0.05f, 0);
        if (firePool.transform.position.y > -20)
            StartCoroutine(FirePoolSink());
    }
    IEnumerator RuneDispare(float time)
    {
        yield return null;
        time += Time.deltaTime;
        var val = valCurve.Evaluate(time/5);
        rune.GetComponent<Projector>().material.SetFloat("_Cutoff", val);
        StartCoroutine(RuneDispare(time));
    }
    public override void StopSkill(Animator animator)
    {
        StopAllCoroutines();
        Destroy(fireStand);
        StartCoroutine(WaitAndDisable());
    }

    public override bool TryStartSkill(Animator animator)
    {
        throw new NotImplementedException();
    }
    IEnumerator WaitAndDisable()
    {
        fire.Stop();
        yield return new WaitForSeconds(1.5f);
        Destroy(fire);
    }
}
