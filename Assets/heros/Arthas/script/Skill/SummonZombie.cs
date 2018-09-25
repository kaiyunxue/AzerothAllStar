using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArthasDomain
{
    public class SummonZombie : HeroSkill
    {
        public Weapon weapon;
        public Vector3 pos;
        [SerializeField]
        ZombieTank tank;
        ZombieTank tankInstance;
        public SpriteLine line;
        SpriteLine lineInstance;
        public override void StartSkill(Animator animator)
        {
            weapon = hero.weapon;
            StartCoroutine(startSkill(animator));
        }

        IEnumerator startSkill(Animator animator)
        {
            yield return new WaitForSeconds(0.5f);
            lineInstance = KOFItem.InstantiateByPool(line, GameController.instance.transform ,gameObject.layer);
            tankInstance = KOFItem.InstantiateByPool(tank, GameController.instance.transform, gameObject.layer);
            tankInstance.transform.localPosition = hero.transform.localPosition + pos;
            var t = tankInstance.transform.position;
            t.y = 0;
            lineInstance.SetLine(weapon.spellPoint.transform, t,0.7f,1);
            yield return new WaitForSeconds(1.4f);
            StopSkill(animator);
        }

        public override void StopSkill(Animator animator, bool isBreak = false)
        {
            if (isBreak)
            {

            }
            else
            {
                KOFItem.DestoryByPool(lineInstance);
            }
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
