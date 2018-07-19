using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrumpStamp1 : SkillItemsBehaviourController
{
    public RagnarosDamage fireDamage;
    public Damage phyDamage;

    protected override void OnEnable()
    {
        Sulfuars sul = (Sulfuars)GameController.Register.FindHeroByLayer(gameObject.layer).weapon;
        fireDamage = new RagnarosDamage(3 * sul.FlameDamageVal, DamageType.Fire, gameObject.layer);
        phyDamage = new Damage(3 * sul.PhyDamageVal, DamageType.Physical);
        gameObject.GetComponent<Collider>().enabled = true;
        StartCoroutine(ValidTrigger());
    }
    IEnumerator ValidTrigger()
    {
        yield return new WaitForSeconds(0.3f);
        gameObject.GetComponent<Collider>().enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9 && other.GetComponent<State>() != null)
        {
            other.GetComponent<State>().TakeSkillContent(fireDamage);
            other.GetComponent<State>().TakeSkillContent(phyDamage);
            if (other.GetComponent<Rigidbody>() != null)
                other.GetComponent<Rigidbody>().AddForce(0, -500, 0);
        }
    }
    public void LetStun(State state)
    {
    }
    public IEnumerator StopEffect(float time)
    {
        var tmp = gameObject.GetComponentInChildren<RFX4_ShaderFloatCurve>();
        yield return new WaitForSeconds(time * tmp.GraphTimeMultiplier);
        tmp.enabled = false;
    }
    public void StartEffect(float time)
    {
        var tmp = gameObject.GetComponentInChildren<RFX4_ShaderFloatCurve>();
        tmp.enabled = true;
        tmp.setStartTime(Time.time - time);
    }
}
