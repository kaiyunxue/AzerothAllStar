using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagnarosSubmergeAttack : HeroSkill, ISkill
{
    public float waitingTime;
    public float waitingTime2;
    public Vector3 aeroliteinterval;
    public FireAerolite aerolite;
    public GameObject sf;
    public GameObject Hand_sf;
    public GameObject sf_copy;
    public GameObject fire;

    public AnimationCurve curve;
    public AnimationCurve curve2;
    public Material pool;
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


    List<ISkill> nextSkills = new List<ISkill>();

    public override void StartSkill(Animator animator)
    {
        hero.state.Mana -= manaCost;
        foreach (ParticleSystem p in fire.GetComponentsInChildren<ParticleSystem>())
        {
            p.Play();
        }
        StartCoroutine(SkillBehave(animator));

    }

    public override bool IsReady()
    {
        //Debug.Log("bp2");
        if (!Lock)
            return false;
        if (!GameController.LeftInputListener.GetSkill(formula))
            return false;
        if (hero.state.Mana < manaCost)
            return false;
        return true;
    }
    public override bool TryStartSkill(Animator animator)
    {
        //Debug.Log("bp");
        if (IsReady())
        {
            animator.SetBool("SubmergeAttack", true);
            return true;
        }
        else
            return false;
    }

    public override void StopSkill(Animator animator)
    {
        StartCdColding();
    }
    protected IEnumerator SkillBehave(Animator animator)
    {
        yield return new WaitForSeconds(1f);
        sf_copy = Instantiate(sf, Hand_sf.transform, true);
        sf_copy.transform.localScale *= 1.5f;
        sf_copy.transform.SetParent(Hand_sf.transform);
        sf.SetActive(false);
        sf_copy.transform.localRotation = Quaternion.Euler(0, 70, 0);
        yield return new WaitForSeconds(0.25f);
        sf_copy.transform.SetParent(GameController.instance.transform);
        sf_copy.transform.position += new Vector3(0, 0.7f, 0);
        Vector3 pos = transform.position;
        pos.y = 10;
        for (int i = 0; i < 7; i++)
        {
            pos += aeroliteinterval;
            FireAerolite go = KOFItem.InstantiateByPool(aerolite,pos,Quaternion.Euler(0,0,0), GameController.instance.transform,gameObject.layer, true);
            go.GetComponent<SkillItemsBehaviourController>().SetTarget(sf_copy);
            if(i == 3)
            {
                go.GetComponent<FireAerolite>().isSeed = true;
            }
            yield return new WaitForSeconds(UnityEngine.Random.Range(0.1f, 0.3f));
        }
                StartCoroutine(Dilling(0));
        StartCoroutine(Step2(animator));
    }
    IEnumerator Step2(Animator animator)
    {
        yield return new WaitForSeconds(waitingTime);
        animator.SetBool("SubmergeAttack", false);
        StartCoroutine(FloatUp(0));
        yield return new WaitForSeconds(waitingTime2);
        Destroy(sf_copy);
        sf.SetActive(true);
        StartCoroutine(WaitAndDisable());
    }
    IEnumerator WaitAndDisable()
    {
        foreach (ParticleSystem p in fire.GetComponentsInChildren<ParticleSystem>())
        {
            p.Stop();
        }
        yield return new WaitForSeconds(1.5f);
    }
    IEnumerator FloatDirectly(Animator animator)
    {
        animator.SetBool("SubmergeAttack", false);
        StartCoroutine(FloatUp(0));
        yield return new WaitForSeconds(waitingTime2);
        Destroy(sf_copy);
        sf.SetActive(true);
    }
    public void Float(Animator animator)
    {
        StartCoroutine(FloatDirectly(animator));
    }
}

