using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArthasDomain
{
    public class PhyAttack : HeroSkill
    {
        public Frostmourne weapon;
        Coroutine skillCoroutine;
        public override void StartSkill(Animator animator)
        {
            weapon.TurnOnPhyAttack();
            StartCoroutine(waitSeconds(animator));
        }
        IEnumerator waitSeconds(Animator animator)
        {
            yield return new WaitForSeconds(0.1f);
            skillCoroutine = StartCoroutine(waitForNextSkill(animator));
        }
        IEnumerator waitForNextSkill(Animator animator, float time = 0)
        {
            if(Input.GetKeyUp(KeyCode.Keypad1))
            {
                animator.SetTrigger("PhyAttack");
            }
            else
            {
                yield return null;
                time += Time.deltaTime;
                skillCoroutine = StartCoroutine(waitForNextSkill(animator, time));
            }
        }
        public override void StopSkill(Animator animator, bool isBreak = false)
        {
            weapon.TurnOffPhyAttack();
            if (skillCoroutine != null)
                StopCoroutine(skillCoroutine);
        }

        public override bool TryStartSkill(Animator animator)
        {
            if (IsReady())
            {
                animator.SetTrigger("PhyAttack");
                return true;
            }
            else
                return false;
        }
        public override bool IsReady()
        {
            if (!Lock)
                return false;
            if (!GameController.RightInputListener.GetSkill(formula))
                return false;
            return true;
        }
    }
}
