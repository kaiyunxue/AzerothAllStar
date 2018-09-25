//Programer: KevinX
//code date:9/24/2018
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AzerothDomain
{
	public class SummonSkeleton : HeroSkill
	{
        [SerializeField]
        SummonField summonField;
        SummonField fieldInstance;
        [SerializeField]
        Skeleton skeleton;
        Skeleton[] skeletonInstances = new Skeleton[6]; 
        public override void StartSkill(Animator animator)
		{
            summonField = KOFItem.InstantiateByPool(summonField, GameController.instance.transform, gameObject.layer);
            summonField.transform.localPosition = hero.transform.localPosition;
            StartCoroutine(startSkill(animator));
		}
        IEnumerator startSkill(Animator animator)
        {
            yield return new WaitForSeconds(1);
            for(int i = 0; i < skeletonInstances.Length; i++)
            {
                Vector3 pos = new Vector3(UnityEngine.Random.Range(-1.5f, 1.5f), 0, UnityEngine.Random.Range(-1.5f, 1.5f));
                skeletonInstances[i] = KOFItem.InstantiateByPool(skeleton, GameController.instance.transform, hero.gameObject.layer);
                skeletonInstances[i].transform.position = hero.transform.position + pos;
                yield return new WaitForSeconds(1);
            }
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
