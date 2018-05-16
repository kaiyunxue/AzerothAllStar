using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagnarosRuneBomb : HeroSkill, ISkill
{
    public int maxSpellTime;
    public int spellTime;

    public Vector3 speed;
    public FireRuneBoom runeBoom;
    public FireRuneBoom runeBoomInstance;
    public Vector3 initialLocalPosition;
    public float maxTime;
    float time;

    public override void StartSkill(Animator animator)
    {
        time = 0;
        spellTime++;
        hero.state.Mana -= manaCost;
        runeBoomInstance = KOFItem.InstantiateByPool(runeBoom, initialLocalPosition ,Quaternion.Euler(90,0,0), GameController.instance.transform, gameObject.layer);
        StartCoroutine(Behave(animator));
    }

    public override void StopSkill(Animator animator)
    {
        StopAllCoroutines();
        if (spellTime == maxSpellTime)
        {
            spellTime = 0;
            StartCdColding();
        }
    }

    public override bool IsReady()
    {
        if (!Lock)
            return false;
        if (!GameController.LeftInputListener.GetSkill(formula))
            return false;
        return true;
    }
    public override bool TryStartSkill(Animator animator)
    {
        if (IsReady())
        {
            animator.SetBool("RuneBoob", true);
            return true;
        }
        else
            return false;
    }

    IEnumerator Behave(Animator animator)
    {
        if(InputListener.GetKeyUp(KeyCode.K) || time >= maxTime)
        {
            yield return null;
            animator.SetBool("RuneBoob", false);
            runeBoomInstance.StartSkill();
        }
        else
        {
            yield return null;
            time += Time.deltaTime;
            runeBoomInstance.transform.position += speed;
            StartCoroutine(Behave(animator));
        }
    }
    // Use this for initialization
}
