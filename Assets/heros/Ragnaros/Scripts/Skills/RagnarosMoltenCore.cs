using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagnarosMoltenCore : HeroSkill, ISkill
{
    public GameObject fire;
    public GameObject pool;
    private float contTime;
    public GameObject materialGO;
    public AnimationCurve materialCurve;
    public AnimationCurve materialCurveBack;
    public override void StartSkill(Animator animator)
    {
        foreach (ParticleSystem p in fire.GetComponentsInChildren<ParticleSystem>())
        {
            p.Play();
        }
        pool.SetActive(false);
        contTime = hero.state.Mana * 3;
        hero.state.Mana = 0;
        hero.state.Stage = 3;
        hero.transform.localScale *= 3;
        hero.transform.localPosition += new Vector3(0, 3, 0);
        StartCoroutine(SkillBehave());
        StartCoroutine(WaitForBack(contTime));
    }
    IEnumerator MaterialChane(float time = 0)
    {
        foreach (var m in materialGO.GetComponent<Renderer>().materials)
        {
            m.SetFloat("_CutOff", materialCurve.Evaluate(time));
        }
        hero.weapon.GetComponent<Renderer>().material.SetFloat("_CutOff", materialCurve.Evaluate(time));
        yield return new WaitForEndOfFrame();
        time +=Time.deltaTime;
        if (time < 3)
            StartCoroutine(MaterialChane(time));
    }
    IEnumerator MaterialChaneBack(float time = 0)
    {
        foreach (var m in materialGO.GetComponent<Renderer>().materials)
        {
            m.SetFloat("_CutOff", materialCurveBack.Evaluate(time));
        }
        hero.weapon.GetComponent<Renderer>().material.SetFloat("_CutOff", materialCurveBack.Evaluate(time));
        yield return new WaitForEndOfFrame();
        time += Time.deltaTime;
        if (time <= 3f)
            StartCoroutine(MaterialChaneBack(time));
    }
    IEnumerator WaitForBack(float time)
    {
        yield return new WaitForSecondsRealtime(contTime);
        StartCoroutine(MaterialChaneBack());
        foreach (var effect in fire.GetComponentsInChildren<ParticleSystem>())
        {
            effect.Play();
        }
        yield return new WaitForSeconds(1.5f);
        hero.state.Stage = 0;
        hero.transform.localScale /= 3;
        hero.transform.localPosition -= new Vector3(0, 3, 0);
        fire.transform.localScale *= 3;
        pool.SetActive(true);
        yield return new WaitForSeconds(1f);
        foreach (var effect in fire.GetComponentsInChildren<ParticleSystem>())
        {
            effect.Stop();
        }
        yield return new WaitForSeconds(5f);
        fire.transform.localScale /= 3;
        StartCdColding();
    }
    IEnumerator SkillBehave()
    {
        StartCoroutine(MaterialChane());
        yield return new WaitForSeconds(1f);
        foreach(var effect in fire.GetComponentsInChildren<ParticleSystem>())
        {
            effect.Stop();
        }
        yield return new WaitForSeconds(2f);
    }
    public override void StopSkill(Animator animator)
    {
        StartCdColding();
    }

    public override bool TryStartSkill(Animator animator)
    {
        if (IsReady())
        {
            animator.SetBool("MoltenCoreState", true);
            StartSkill(animator);
            return true;
        }
        else
            return false;
    }
    public override bool IsReady()
    {
        //Debug.Log("bp2");
        if (!Lock)
            return false;
        if (!GameController.LeftInputListener.GetSkill(formula))
            return false;
        if (hero.state.Mana == 0)
            return false;
        return true;
    }
}
