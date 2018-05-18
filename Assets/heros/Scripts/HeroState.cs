using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HeroState : State
{
    protected StatusBox statusBox;
    [SerializeField]
    protected Hero hero;
    protected virtual void Awake()
    {
        hero = gameObject.GetComponent<Hero>();
        statusBox = hero.statusBox;
    }
    public override void TakeSkillContent(Damage damage)
    {
        if (damage.doSkillEffect != null)
            damage.doSkillEffect();
        base.TakeSkillContent(damage);
        Health -= damage.skillDamage;
    }
    public override void TakeSkillContent(Dot dot)
    {
        base.TakeSkillContent(dot);
        if(dot.doSkillEffect != null)
            dot.doSkillEffect();
        StartCoroutine(TakeDot(dot, 0));
    }
    public override void TakeSkillContent(Debuff dot)
    {
        base.TakeSkillContent(dot);
        dot.buffEffect(this);
    }
    IEnumerator TakeDot(Dot dot, float time)
    {
        TakeSkillContent(dot.PerDamage);
        time += dot.Frequency;
        yield return new WaitForSeconds(dot.Frequency);
        if (time > dot.TotalTime && dot.TotalTime > 0)
            yield return null;
        else
            StartCoroutine(TakeDot(dot, time));
    }
}
