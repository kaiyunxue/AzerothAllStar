//Programer: KevinX
//code date:9/25/2018
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArthasDomain
{
	public class SummonForstWurm : HeroSkill
	{
        [SerializeField]
        SummonField summonField;
        SummonField fieldInstance;
        [SerializeField]
        ForstWurm wurm;
        ForstWurm wurmInstance;
		public override void StartSkill(Animator animator)
		{
            summonField = KOFItem.InstantiateByPool(summonField, GameController.instance.transform, gameObject.layer);
            summonField.transform.localPosition = hero.transform.localPosition;
            wurmInstance = KOFItem.InstantiateByPool(wurm, new Vector3(-15, 4, 0), GameController.instance.transform, gameObject.layer);
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
                animator.SetTrigger("Summon1");
                StartSkill(animator);
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
            if (!GameController.RightInputListener.GetSkill(formula))
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
