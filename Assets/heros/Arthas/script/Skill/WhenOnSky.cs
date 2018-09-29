using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArthasDomain
{
    public class WhenOnSky : HeroSkill, ISkill
    {

        public override void StartSkill(Animator animator)
        {
            hero.skillManager.GetCurrentSkill().StopSkill(animator, true);
            hero.skillManager.SetCurrentSkill(this);
        }

        public override void StopSkill(Animator animator, bool isBreak = false)
        {
        }

        public override bool TryStartSkill(Animator animator)
        {
            throw new NotImplementedException();
        }
        private void FixedUpdate()
        {
            Ray ray = new Ray(hero.transform.position, -hero.transform.up);
            if (!Physics.Raycast(ray, 0.2f))
            {
                if(!hero.animator.GetBool("OnSky"))
                    hero.animator.SetTrigger("StartOnSky");
                hero.animator.SetBool("OnSky", true);
            }
            else
            {
                hero.animator.SetBool("OnSky", false);
            }
        }
    }
}
