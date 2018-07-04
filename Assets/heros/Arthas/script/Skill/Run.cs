using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArthasDomain
{
    public class Run : HeroSkill, ISkill
    {
        public Vector3 speed;
        public override void StartSkill(Animator animator)
        {
            animator.SetBool("Run", true);
            StartCoroutine(skillBehave(animator));
        }
        IEnumerator skillBehave(Animator animator)
        {
            if(Input.GetKey(hero.inputListener.dirKeys[3]))
            {
                hero.transform.position += speed * Time.deltaTime;
                yield return new WaitForEndOfFrame();
                StartCoroutine(skillBehave(animator));
            }
            else
            {
                animator.SetBool("Run", false);
                yield return null;
            }
        }

        public override void StopSkill(Animator animator, bool isBreak = false)
        {
        }

        public override bool TryStartSkill(Animator animator)
        {
            if (!Lock)
                return false;
            if (!hero.inputListener.GetSkill(formula))
                return false;
            StartSkill(animator);
            return true;
        }
    }
}
