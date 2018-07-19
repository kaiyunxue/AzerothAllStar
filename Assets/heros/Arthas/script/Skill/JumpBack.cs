using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArthasDomain
{

    public class JumpBack : HeroSkill
    {
        public override void StartSkill(Animator animator)
        {
            hero.GetComponent<Rigidbody>().AddForce(new Vector3(-150, 120, 0));
        }

        public override void StopSkill(Animator animator, bool isBreak = false)
        {
            throw new NotImplementedException();
        }

        public override bool TryStartSkill(Animator animator)
        {
            if (IsReady())
            {
                //animator.SetTrigger("DoubleJump");
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
    }
}
