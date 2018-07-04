using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RagnarosWalkBack : HeroSkill, ISkill
{
    public float speed;
    public Ragnaros heroModel;
    public HeroSkill[] nextSkills;
    protected override void Awake()
    {
        base.Awake();
        heroModel = GetComponentInParent<Ragnaros>();
        List<HeroSkill> skills = nextSkills.ToList();
        nextSkills = skills.OrderByDescending(skill => skill.Priority).ToArray();
    }
    public override void StartSkill(Animator animator)
    {
        animator.SetBool("Run", false);
        StartCoroutine(SkillUpdate(animator));
    }
    protected override IEnumerator SkillUpdate(Animator animator)
    {
        yield return new WaitForEndOfFrame();
        foreach (ISkill skill in nextSkills)
        {
            if (skill.TryStartSkill(animator))
                break;
            StopCoroutine(SkillUpdate(animator));
        }
        if (GameController.LeftInputListener.isLongPress(KeyCode.A))
        {
            animator.SetBool("Back", true);
            heroModel.transform.position += new Vector3(speed, 0, 0);
            StartCoroutine(SkillUpdate(animator));
        }
        else
        {
            animator.SetBool("Back", false);
        }
    }
    public override void StopSkill(Animator animator, bool isBreak = false)
    {
        StopAllCoroutines();
        animator.SetBool("Back", false);
    }
    public override bool IsReady()
    {
        if (!Lock)
            return false;
        if (!GameController.LeftInputListener.isLongPress(KeyCode.A))
            return false;
        return true;
    }
    public override bool TryStartSkill(Animator animator)
    {
        if (IsReady())
        {
            animator.SetBool("Back", true);
            return true;
        }
        else
            return false;
    }
}
