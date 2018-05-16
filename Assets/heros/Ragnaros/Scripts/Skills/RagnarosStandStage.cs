using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RagnarosStandStage : HeroSkill,ISkill
{
    public HeroSkill[] nextSkills;
    protected override void Awake()
    {
        base.Awake();
        List<HeroSkill> skills = nextSkills.ToList();
        nextSkills = skills.OrderByDescending(skill => skill.Priority).ToArray();
    }
    public override void StartSkill(Animator animator)
    {
        animator.SetBool("Run", false);
        animator.SetBool("Back", false);
        StartCoroutine(SkillUpdate(animator));
    }
    protected override IEnumerator SkillUpdate(Animator animator)
    {
        yield return new WaitForEndOfFrame();
        foreach (ISkill skill in nextSkills)
        {
            if (skill.TryStartSkill(animator))
                break;
        }
        StartCoroutine(SkillUpdate(animator));
    }

    public override void StopSkill(Animator animator)
    {
        StopAllCoroutines();
    }

    public override bool TryStartSkill(Animator animator)
    {
        throw new NotImplementedException();
    }
}
