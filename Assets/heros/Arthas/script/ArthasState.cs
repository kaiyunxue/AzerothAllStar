using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArthasState : HeroState
{
    protected override void Awake()
    {
        base.Awake();
        Health = MaxHealth;
    }
    protected override float SetHealth(float value)
    {
        float val = base.SetHealth(value);
        statusBox.ShowHealth(val / MaxHealth);
        return val;
    }
    public override void Stun(float time)
    {
        hero.animator.SetBool("Stun", true);
        StartCoroutine(Stuning(time));
    }
    IEnumerator Stuning(float time)
    {
        yield return new WaitForSeconds(time);
        hero.animator.SetBool("Stun", false);
    }
    //protected override float SetMana(float value)
    //{
    //    var val = base.SetMana(value);
    //    statusBox.ShowMana(val);
    //    return val;
    //}

    //public override void TakeSkillContent(SkillContent content)
    //{
    //    base.TakeSkillContent(content);
    //}
}
