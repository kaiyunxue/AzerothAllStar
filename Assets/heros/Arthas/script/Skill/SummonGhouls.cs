using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArthasDomain
{


    [ExecuteInEditMode]
    public class SummonGhouls : HeroSkill

    {
        public SpriteLine spriteLine;
        public SpriteLine[] spriteLineInstances;
        public float r;


        public override void StartSkill(Animator animator)

        {
            StartSummonGhouls(GetSummonGhoulsPosition(3));

        }



        public override void StopSkill(Animator animator, bool isBreak = false)

        {

            throw new NotImplementedException();

        }


        public override bool IsReady()
        {
            if (!Lock)
                return false;
            if (!GameController.RightInputListener.GetSkill(formula))
                return false;
            return true;
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
        Vector3[] GetSummonGhoulsPosition(int n)

        {
            Vector3[] positions = new Vector3[n];
            spriteLineInstances = new SpriteLine[n];
            float pAngle = 360 / n;

            for (int i = 0; i < n; i++)

            {

                float angle = pAngle / 2 + pAngle * i;

                positions[i] = transform.position +  new Vector3(r * Mathf.Cos(angle / 180 * Mathf.PI), 0 , r * Mathf.Sin(angle / 180 * Mathf.PI));

                spriteLineInstances[i] = KOFItem.InstantiateByPool(spriteLine, GameController.instance.transform, gameObject.layer);
                Frostmourne f = hero.weapon as Frostmourne;
                spriteLineInstances[i].SetLine(f.speller.transform.position, positions[i]);

            }
            return positions;
        }
        void StartSummonGhouls(Vector3[] positions)
        {
            foreach(Vector3 v in positions)
            {
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = v;
            }
        }
    }
}
