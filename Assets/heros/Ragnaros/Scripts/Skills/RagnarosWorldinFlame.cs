using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagnarosWorldinFlame : HeroSkill, ISkill
{
    public AudioClip clip;
    public FlameOfFlameWorld flame;
    public float RadiusInterval;
    List<ISkill> nextSkills = new List<ISkill>();

    public override void StartSkill(Animator animator)
    {
        hero.audioCtrler.PlaySound(clip);
        hero.state.Mana -= manaCost;
        StartCoroutine(SkillBehave(1));
    }

    public override void StopSkill(Animator animator)
    {
        StartCdColding();
        hero.statusBox.cdBar.StartCooling(skillIcon, cd);
    }

    public override bool IsReady()
    {
        if (!Lock)
            return false;
        if (hero.state.Stage != 0)
            return false;
        if (!GameController.LeftInputListener.GetSkill(formula))
            return false;
        if (hero.state.Mana < manaCost)
            return false;
        return true;
    }
    public override bool TryStartSkill(Animator animator)
    {
        if (IsReady())
        {
            animator.SetBool("WorldInFlame", true);
            return true;
        }
        else
            return false;
    }

    protected IEnumerator SkillBehave(int n)
    {
        FlameCircle(n);
        yield return new WaitForSeconds(1.2f);
        if (n != 6)
            StartCoroutine(SkillBehave(n + 1));
    }
    void FlameCircle(int n)
    {
        Vector3[] flamePos = new Vector3[8];
        for (int i = 0; i < 8; i++)
        {
            flamePos[i].y = 2.3f;
            flamePos[i].x = gameObject.transform.position.x + n * RadiusInterval * Mathf.Sin(Mathf.PI / 4 * i);
            flamePos[i].z = gameObject.transform.position.z + n * RadiusInterval * Mathf.Cos(Mathf.PI / 4 * i);
            KOFItem.InstantiateByPool(flame, flamePos[i], Quaternion.Euler(Vector3.zero), GameController.instance.transform, gameObject.layer, true);
        }
    }
}