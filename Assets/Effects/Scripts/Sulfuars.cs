using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sulfuars : SkillItemsBehaviourController
{
    public ParticleSystem particles;
    [SerializeField]
    private float flameDamageVal;
    [SerializeField]
    private float phyDamageVal;
    private bool isPhyAttack;
    public GameObject colliderObject;
    public string colliderName;
    public bool isCollide;
    public GameObject Effect;

    public void FromMana2Attack()
    {
        Effect.SetActive(false);
        Effect.SetActive(true);
        FlameDamageVal += 10;
    }

    public float FlameDamageVal
    {
        get
        {
            return flameDamageVal;
        }

        set
        {
            ParticleSystem.EmissionModule e = particles.emission;
            flameDamageVal = value;
            e.rateOverTime = flameDamageVal * 2;
        }
    }

    public float PhyDamageVal
    {
        get
        {
            return phyDamageVal;
        }

        set
        {
            phyDamageVal = value;
        }
    }

    public void TurnOnPhyAttack()
    {
        StopAllCoroutines();
        isPhyAttack = true;
        StartCoroutine(TurnOnPhyAttack_(0.7f));

    }
    public bool IsPhyAttack()
    {
        return isPhyAttack;
    }

    IEnumerator TurnOnPhyAttack_(float time)
    {
        yield return new WaitForSeconds(time);
        isPhyAttack = false;
    }

    protected void Start()
    {
        isCollide = false;
        FlameDamageVal = 0;
        Effect.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
        if (other.gameObject.layer != 8)
        {
            isCollide = true;
            colliderObject = other.gameObject;
            colliderName = other.name;
            StartCoroutine(WaitingthenDelete());
            if(other.gameObject.layer == 9 && isPhyAttack)
            {
                FlameDamageVal += 2f;
                RagnarosDamage flameDamage = new RagnarosDamage(FlameDamageVal, DamageType.Fire, gameObject.layer);
                Damage phyDamage = new Damage(PhyDamageVal, DamageType.Physical);
                other.GetComponent<Rigidbody>().AddForce(-150,0,0);
                other.GetComponent<State>().TakeSkillContent(phyDamage);
                other.GetComponent<State>().TakeSkillContent(flameDamage);
            }
        }
    }
    IEnumerator WaitingthenDelete()
    {
        yield return new WaitForSeconds(0.1f);
        colliderObject = null;
        colliderName = null;
        isCollide = false;
    }
}

