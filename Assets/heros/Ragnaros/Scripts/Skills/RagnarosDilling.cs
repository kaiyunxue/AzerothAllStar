using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagnarosDilling : HeroSkill, ISkill
{
    private float t;
    public AudioClip clip;
    public AnimationCurve curve;
    public AnimationCurve curve2;
    public Material pool;
    public RagnarosFlame fire;
    public override void StartSkill(Animator animator)
    {

        t = 0;
        hero.audioCtrler.PlaySound(clip);
        StartCdColding();
        hero.statusBox.cdBar.StartCooling(skillIcon, cd);
        StartCoroutine(Dilling(0));
        StartCoroutine(Dilling(animator));
        fire.Play();
    }
    public override void StopSkill(Animator animator)
    {
    }
    IEnumerator Dilling(float time)
    {
        pool.SetFloat("_CutOff", curve.Evaluate(time));
        yield return new WaitForEndOfFrame();
        StartCoroutine(Dilling(time + Time.deltaTime));
    }
    IEnumerator FloatUp(float time)
    {
        pool.SetFloat("_CutOff", curve2.Evaluate(time));
        yield return new WaitForEndOfFrame();
        StartCoroutine(FloatUp(time + Time.deltaTime));
    }
    public override bool IsReady()
    {
        if (!Lock)
            return false;
        if (hero.state.Stage != 0)
            return false;
        if (!GameController.LeftInputListener.GetSkill(formula))
            return false;
        return true;
    }
    public override bool TryStartSkill(Animator animator)
    {
        if (IsReady())
        {
            StartSkill(animator);
            animator.SetBool("Dilling", true);
            return true;
        }
        else
            return false;
    }

    IEnumerator Dilling(Animator animator)
    {
        t += Time.deltaTime;
        if (t >= 5 || Input.GetKeyUp(KeyCode.L))
        {
            StartCoroutine(FloatUp(0));
            animator.SetBool("Dilling", false);
            //hero.state.Health += (int)(t * 6);
            yield return null;
            StartCoroutine(WaitAndDisable());
        }
        else
        {
            yield return new WaitForEndOfFrame();
            hero.state.Health += Time.deltaTime * 60;
            StartCoroutine(Dilling(animator));
        }
    }
    IEnumerator WaitAndDisable()
    {
        yield return new WaitForSeconds(2f);
        fire.Stop();
    }
}

