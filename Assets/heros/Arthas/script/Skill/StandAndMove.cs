using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArthasDomain
{
    public class StandAndMove : HeroSkill, ISkill
    {
        public Vector3 forwardSpeed;
        public HeroSkill[] nextSkills;
        public Coroutine currentCoroutine;
        public override void StartSkill(Animator animator)
        {
            hero.skillManager.SetCurrentSkill(this);
            if(currentCoroutine != null)
                StopCoroutine(currentCoroutine);
            currentCoroutine = StartCoroutine(SkillUpdate(hero.animator));
        }

        public override void StopSkill(Animator animator, bool isBreak = false)
        {
            if(isBreak && currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
            }
        }

        public override bool TryStartSkill(Animator animator)
        {
            return true;
        }
        public override bool IsReady()
        {
            return true;
        }


        protected override IEnumerator SkillUpdate(Animator animator)
        {
            bool isHasNextSkill = false;
            foreach (ISkill skill in nextSkills)
            {
                if (skill.TryStartSkill(animator))
                {
                    isHasNextSkill = true;
                    break;
                }
            }
            if(isHasNextSkill)
            {
                StopAllCoroutines();
            }
            else
            {
                if(GameController.RightInputListener.isLongPress(KeyCode.LeftArrow))
                {
                    hero.transform.position += forwardSpeed * Time.deltaTime;
                    animator.SetBool("Forward", true);
                    animator.SetBool("Backward", false);
                }
                else if (GameController.RightInputListener.isLongPress(KeyCode.RightArrow))
                {
                    hero.transform.position -= forwardSpeed * Time.deltaTime * 2 / 3;
                    animator.SetBool("Forward", false);
                    animator.SetBool("Backward", true);
                }
                else
                {
                    animator.SetBool("Backward", false);
                    animator.SetBool("Forward", false);
                }
            }
            yield return new WaitForEndOfFrame();
            currentCoroutine = StartCoroutine(SkillUpdate(animator));
        }
    }
}
