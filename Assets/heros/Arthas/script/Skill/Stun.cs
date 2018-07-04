using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArthasDomain
{

    public class Stun : HeroSkill, ISkill
    {
        public override void StartSkill(Animator animator)
        {
            hero.skillManager.GetCurrentSkill().StopSkill(animator);
        }

        public override void StopSkill(Animator animator, bool isBreak = false)
        {
        }

        public override bool TryStartSkill(Animator animator)
        {
            return true;
        }
    }
}
