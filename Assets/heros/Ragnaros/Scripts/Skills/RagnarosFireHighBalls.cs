using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagnarosFireHighBalls : HeroSkill, ISkill
{
    public GameObject leftHand;
    public FireHighBall fireBall;
    List<FireHighBall> fireBallInstance = new List<FireHighBall>();
    public float initialForce;
    float Force_high = 1;

    #region Only for test
    [Header("Damage")]
    public float damageVal;
    public DamageType damageType;
    #endregion 

    public override void StartSkill(Animator animator)
    {
        if(fireBallInstance.Count == 0)
        {
            fireBall.speller = hero;
            fireBallInstance.Add(KOFItem.InstantiateByPool(fireBall, leftHand.transform, hero ,gameObject.layer));
            if (hero.state.Stage == 3)
            {
                fireBallInstance.Add(KOFItem.InstantiateByPool(fireBall, leftHand.transform, hero,gameObject.layer));
                fireBallInstance.Add(KOFItem.InstantiateByPool(fireBall, leftHand.transform, hero,gameObject.layer));
            }
            StartCoroutine(HighFireBalls(animator));
        }
    }
    IEnumerator HighFireBalls(Animator animator)
    {
        if (Input.GetKeyUp(KeyCode.K) || Force_high >= 500)
        {
            StartCdColding();
            hero.statusBox.cdBar.StartCooling(skillIcon, cd);
            foreach (var instance in fireBallInstance)
            {
                instance.PlaySoundWhenBeReleased();
                instance.transform.SetParent(GameController.instance.transform);
                instance.GetComponent<Rigidbody>().useGravity = true;
                instance.GetComponent<Rigidbody>().AddForce(-UnityEngine.Random.Range(2 * Force_high / 3, 4 * Force_high / 3), 100, 0);
                animator.SetBool("Fireball_high", false);
                instance.GetComponent<FireHighBall>().damage = new RagnarosDamage(damageVal, damageType, gameObject.layer);
            }
            yield return new WaitForEndOfFrame();
            Force_high = 10;
            fireBallInstance.Clear();
        }
        else
        {
            Force_high += 2;
            yield return null;
            StartCoroutine(HighFireBalls(animator));
        }
    }
    public override void StopSkill(Animator animator, bool isBreak = false)
    {
    }

    public override bool IsReady()
    {
        if (!Lock)
            return false;
        if (!GameController.LeftInputListener.GetSkill(formula))
            return false;
        return true;
    }
    public override bool TryStartSkill(Animator animator)
    {
        if (IsReady())
        {
            animator.SetBool("Fireball_high", true);
            StartSkill(animator);
            return true;
        }
        else
            return false;
    } 
}
    