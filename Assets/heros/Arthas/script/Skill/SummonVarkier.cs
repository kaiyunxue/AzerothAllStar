//Programer: KevinX
//code date:9/26/2018
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArthasDomain
{
	public class SummonVarkier : HeroSkill
	{
        [SerializeField]
        Varkier varkier;
        Varkier varkierInstance;
        public Vector3 relativePos;
		public override void StartSkill(Animator animator)
		{
            StartCoroutine(startSkill(animator));
		}
        IEnumerator startSkill(Animator animator)
        {
            yield return new WaitForSeconds(3);
            varkierInstance = KOFItem.InstantiateByPool(varkier, GameController.instance.transform, gameObject.layer);
            varkierInstance.transform.localPosition = hero.transform.localPosition + relativePos;
            animator.SetBool("SummonVar", false);
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
                animator.SetTrigger("SummonGhoul");
                animator.SetBool("SummonVar", true);
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
