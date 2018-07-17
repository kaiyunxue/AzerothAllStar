using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagnarosMoltenCore : HeroSkill, ISkill
{
    public AudioClip word;
    public TitanFlame titantFlame;
    TitanFlame flameInstance;
    public RagnarosFlame fire;
    public GameObject pool;
    public ParticleSystem skinFire;
    private float contTime;
    public GameObject materialGO;
    public AnimationCurve materialCurve;
    public AnimationCurve materialCurveBack;
    public override void StartSkill(Animator animator)
    {
        skinFire.gameObject.SetActive(true);
        fire.Play();
        hero.audioCtrler.ForcePlaySound(word);
        pool.SetActive(false);
        contTime = hero.state.Mana * 1f + 2;
        flameInstance = KOFItem.InstantiateByPool(titantFlame,hero.transform.localPosition , GameController.instance.transform, gameObject.layer);
        hero.state.Mana = 0;
        hero.state.Stage = 3;
        hero.transform.localScale *= 3;
        hero.transform.localPosition += new Vector3(0, 3, 0);
        StartCoroutine(SkillBehave());
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
    private void FixedUpdate()
    {
        Debug.Log(contTime);
        if(hero.state.Stage == 3 && contTime > 0)
        {
            contTime += hero.state.Mana * 1f;
            hero.state.Mana = 0;
            contTime -= Time.fixedDeltaTime;
        }
        else if(hero.state.Stage == 3 && contTime <= 0)
        {
            StartCoroutine(WaitForBack());
        }
    }
    IEnumerator WaitForBack()
    {
        hero.state.Stage = 0;
        skinFire.Stop();
        StartCoroutine(MaterialChaneBack());
        StartCdColding();
        hero.statusBox.cdBar.StartCooling(skillIcon, cd);
        fire.Play();
        yield return new WaitForSeconds(1.5f);
        hero.state.Stage = 0;
        hero.transform.localScale /= 3;
        hero.transform.localPosition -= new Vector3(0, 3, 0);
        flameInstance.Stop();
        fire.transform.localScale *= 3;
        pool.SetActive(true);
        yield return new WaitForSeconds(1f);
        fire.Stop();
        yield return new WaitForSeconds(5f);
        fire.transform.localScale /= 3;
        KOFItem.DestoryByPool(flameInstance);
    }
    IEnumerator SkillBehave()
    {
        StartCoroutine(MaterialChane());
        yield return new WaitForSeconds(1f);
        fire.Stop();
        yield return new WaitForSeconds(2f);
    }
    public override void StopSkill(Animator animator, bool isBreak = false)
    {
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
        if (hero.state.Mana < 1)
            return false;
        return true;
    }
}
