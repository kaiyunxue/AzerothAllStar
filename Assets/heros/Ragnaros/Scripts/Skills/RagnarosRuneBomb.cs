using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagnarosRuneBomb : HeroSkill, ISkill
{
    public int maxSpellTime;
    public int spellTime;

    public Vector3 speed;
    public FireBlower fireBlower;
    public FireRuneBoom runeBoom;
    FireRuneBoom runeBoomInstance;
    public Vector3 initialLocalPosition;
    public float maxTime;
    float time;

    public override void StartSkill(Animator animator)
    {
        time = 0;
        spellTime++;
        hero.state.Mana -= manaCost;
        if(hero.state.Stage == 0)
        {
            runeBoomInstance = KOFItem.InstantiateByPool(runeBoom, initialLocalPosition, Quaternion.Euler(90, 0, 0), GameController.instance.transform, gameObject.layer);
            StartCoroutine(Behave(animator));
        }
        else if(hero.state.Stage == 3)
        {
            var instance = KOFItem.InstantiateByPool(fireBlower, GameController.instance.transform, gameObject.layer);
            instance.damage = new RagnarosDamage(50, DamageType.Fire, gameObject.layer);
            animator.SetBool("RuneBoob", false);
        }
    }

    public override void StopSkill(Animator animator)
    {
        StopAllCoroutines();
        if (spellTime == maxSpellTime)
        {
            spellTime = 0;
            StartCdColding();
            hero.statusBox.cdBar.StartCooling(skillIcon, cd);
        }
    }

    public override bool IsReady()
    {
        if (!Lock)
            return false;
        if (hero.state.Stage == 0 && hero.state.Mana < manaCost)
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
