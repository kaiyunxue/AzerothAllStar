using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagnarosMoltenSeed : HeroSkill, ISkill
{
    public AudioClip word;
    public GameObject[] traps;
    public MoltenSeed moltenSeed;
    MoltenSeed moltenseedInstance;
    public GameObject LeftHand;
    public override void StartSkill(Animator animator)
    {
        hero.audioCtrler.PlaySound(word);
        StartBehave(animator);
    }

    public override void StopSkill(Animator animator)
    {
    }
    void StartBehave(Animator animator)
    {
        traps = GameController.Register.LeftHero.GetComponent<HerosRegistrar>().SearchCompontent("FireTrap");
        if (traps == null)
        {
            Debug.Log("No traps");
            animator.SetBool("MoltenSeed", false);
        }
        else
        {
            List<GameObject> traplist = new List<GameObject>();
            foreach (GameObject go in traps)
            {
                if (go != null && go.GetComponent<FireTrap>().IsWaiting())
                {
                    traplist.Add(go);
                    go.GetComponent<FireTrap>().hold();
                }
            }
            moltenseedInstance = MoltenSeed.InstantiateByPool(moltenSeed, LeftHand.transform, gameObject.layer, traplist.Count, traplist.ToArray());
            StartCoroutine(SkillBehave(animator));
        }
    }
    protected IEnumerator SkillBehave(Animator animator)
    {
        yield return new WaitForSeconds(3f);
        moltenseedInstance.transform.SetParent(GameController.instance.gameObject.transform);
        yield return new WaitForSeconds(0.5f);
        moltenseedInstance.GetComponent<MoltenSeed>().SeedForward();
        animator.SetBool("MoltenSeed",false);
    }

    public override bool IsReady()
    {
        if (hero.GetComponent<HerosRegistrar>().SearchCompontent("FireTrap") == null)
            return false;
        if (hero.state.Stage != 0)
            return false;
        if (!Lock)
            return false;
        if (hero.state.Mana < manaCost)
            return false;
        if (!GameController.LeftInputListener.GetSkill(formula))
            return false;

        hero.state.Mana -= manaCost;
        return true;
    }
    public override bool TryStartSkill(Animator animator)
    {
        if (IsReady())
        {
            StartSkill(animator);
            animator.SetBool("MoltenSeed",true);
            return true;
        }
        else
            return false;
    }
}

