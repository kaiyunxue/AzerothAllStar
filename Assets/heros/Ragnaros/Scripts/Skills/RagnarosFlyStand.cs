using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class RagnarosFlyStand : HeroSkill, ISkill
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
        StartCoroutine(SkillUpdate(animator));
    }
    public override void StopSkill(Animator animator, bool isBreak = false)
    {
        StopAllCoroutines();
    }
    protected override IEnumerator SkillUpdate(Animator animator)
    {
        foreach(ISkill skill in nextSkills)
        {
            if (skill.TryStartSkill(animator))
                break;
        }
        yield return new WaitForEndOfFrame();
        StartCoroutine(SkillUpdate(animator));
    }

    public override bool TryStartSkill(Animator animator)
    {
        throw new NotImplementedException();
    }
}
