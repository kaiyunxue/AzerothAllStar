using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagnarosFireHighBalls : HeroSkill, ISkill
{
    public GameObject leftHand;
    public FireHighBall fireBall;
    FireHighBall fireBallInstance;
    public float initialForce;
    float Force_high = 1;

    #region Only for test
    [Header("Damage")]
    public float damageVal;
    public DamageType damageType;
    #endregion 

    public override void StartSkill(Animator animator)
    {
        if(fireBallInstance == null)
        {
            fireBallInstance = KOFItem.InstantiateByPool(fireBall, leftHand.transform, gameObject.layer);
            StartCoroutine(HighFireBalls(animator));
        }
    }
    IEnumerator HighFireBalls(Animator animator)
    {
        if (Input.GetKeyUp(KeyCode.K) || Force_high >= 500)
        {
            fireBallInstance.transform.SetParent(GameController.instance.transform);
            fireBallInstance.GetComponent<Rigidbody>().useGravity = true;
            fireBallInstance.GetComponent<Rigidbody>().AddForce(-Force_high, 100, 0);
            animator.SetBool("Fireball_high",false);
            fireBallInstance.GetComponent<FireHighBall>().damage = new RagnarosDamage(damageVal, damageType, gameObject.layer);
            yield return null;
            Force_high = 10;
            fireBallInstance = null;
        }
        else
        {
            Force_high += 2;
            yield return null;
            StartCoroutine(HighFireBalls(animator));
        }
    }
    public override void StopSkill(Animator animator)
    {
        StartCdColding();
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
            return true;
        }
        else
            return false;
    } 
}
    