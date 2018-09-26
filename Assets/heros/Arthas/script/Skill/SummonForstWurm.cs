//Programer: KevinX
//code date:9/25/2018
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace #NAMESPACE#
{
	public class SummonForstWurm : HeroSkill
	{
		public override void StartSkill(Animator animator)
		{
			throw new NotImplementedException();
		}

		public override void StopSkill(Animator animator, bool isBreak = false)
		{
			if(isBreak)
            {

            }
            else
            {

            }
		}

		public override bool TryStartSkill(Animator animator)
		{
			if (IsReady())
            {
                //animator.SetTrigger("ANIMATIONAME");
                //StartSkill(animator);
                return true;
            }
            else
                return false;
		}
		public override bool IsReady()
		{
			//return base.IsReady();
			if (!Lock)
                return false;
            if (!GameController.//RightInputListener.GetSkill(formula))
                return false;
            return true;
		}
		public override string GetFormula()
		{
			return base.GetFormula();
		}		
		protected override void Awake()
		{
			base.Awake();
		}
	}
}
