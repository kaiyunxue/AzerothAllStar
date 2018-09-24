﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArthasDomain
{
    public class SummonZombie : HeroSkill
    {
        public Weapon weapon;
        protected override void Awake()
        {
            base.Awake();
        }
        public override void StartSkill(Animator animator)
        {
            throw new NotImplementedException();
        }

        public override void StopSkill(Animator animator, bool isBreak = false)
        {
            throw new NotImplementedException();
        }

        public override bool TryStartSkill(Animator animator)
        {
            if (IsReady())
            {
                animator.SetTrigger("SummonGhoul");
                StartSkill(animator);
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
        public override string GetFormula()
        {
            return base.GetFormula();
        }
    }
}
