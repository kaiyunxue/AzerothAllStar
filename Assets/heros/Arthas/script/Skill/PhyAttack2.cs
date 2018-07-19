using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArthasDomain
{

    public class PhyAttack2 : HeroSkill

    {

        public Frostmourne weapon;

        public override void StartSkill(Animator animator)
        {
            weapon.TurnOnPhyAttack();

        }



        public override void StopSkill(Animator animator, bool isBreak = false)
        {
            weapon.TurnOffPhyAttack();
        }



        public override bool TryStartSkill(Animator animator)

        {

            throw new NotImplementedException();

        }

    }
}
