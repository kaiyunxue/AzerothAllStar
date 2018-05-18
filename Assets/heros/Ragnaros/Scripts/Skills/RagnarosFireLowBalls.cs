using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagnarosFireLowBalls : HeroSkill, ISkill
{
    public AudioClip word;
    public GameObject leftHand;
    public FireBall fireBall;    
    public FireBall fireBallInstance;
    public float initialForce;
    float force;

    #region Only for test
    [Header("Damage")]
    public float initialDamage;
    public float damageVal;
    public DamageType damageType;
    #endregion 

    protected override void Awake()
    {
        damageVal = initialDamage;
        base.Awake();
        force = initialForce;
    }

    public override void StartSkill(Animator animator)
    {
        if(fireBallInstance == null)
        {
            damageVal = initialDamage;
            fireBallInstance = KOFItem.InstantiateByPool(fireBall,leftHand.transform,gameObject.layer);
            StartCoroutine(SkillUpdate(animator));
        }
    }
    public override void StopSkill(Animator animator)
    {
    }
    protected override IEnumerator SkillUpdate(Animator animator)
    {
        if (Input.GetKey(KeyCode.K) && force >= 50)
        {
            force -= 1;
            yield return new WaitForEndOfFrame();
            damageVal += 0.5f;
            StartCoroutine(SkillUpdate(animator));
        }
        else
        {
            if(force <= 150)
                hero.audioCtrler.PlaySound(word);
            fireBallInstance.GetComponent<FireBall>().damage = new RagnarosDamage((int)damageVal, damageType,gameObject.layer); 
            animator.SetBool("Fireball_low", false);
            yield return new WaitForSeconds(0.5f);
            fireBallInstance.transform.SetParent(GameController.instance.gameObject.transform);
            fireBallInstance.GetComponent<Rigidbody>().AddForce(-force, 0, 0);
            yield return new WaitForEndOfFrame();
            force = initialForce;
            fireBallInstance = null;
        }
    }

    public override bool IsReady()
    {
        if (!Lock)
            return false;
        if (hero.state.Stage != 0)
            return false;
        if (!GameController.LeftInputListener.GetSkill(formula))
            return false;
        return true;
    }
    public override bool TryStartSkill(Animator animator)
    {
        if (IsReady())
        {
            animator.SetBool("Fireball_low", true);
            return true;
        }
        else
            return false;
    }
}
