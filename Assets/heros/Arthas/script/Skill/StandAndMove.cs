using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArthasDomain
{
    public class StandAndMove : HeroSkill, ISkill
    {
        public HeroSkill[] nextSkills;
        public override void StartSkill(Animator animator)
        {

        }

        public override void StopSkill(Animator animator)
        {

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
            foreach (ISkill skill in nextSkills)
            {
                if (skill.TryStartSkill(animator))
                    break;
            }
            yield return new WaitForEndOfFrame();
            StartCoroutine(SkillUpdate(animator));
        }
    }
}
