using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrumpStamp : SkillItemsBehaviourController
{
    static int maxInstanceNum = 2;
    public Debuff debuff;
    protected override void OnEnable()
    {
        base.OnEnable();
        debuff = new Debuff(LetStun);
        damage = new Damage(50, DamageType.Physical);
        StartCoroutine(ValidTrigger());
    }
    IEnumerator ValidTrigger()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<Collider>().enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            other.GetComponent<State>().TakeSkillContent(debuff);
            other.GetComponent<State>().TakeSkillContent(damage);
            if (other.GetComponent<Rigidbody>() != null)
                other.GetComponent<Rigidbody>().AddForce(0, -100, 0);
        }
    }
    public void LetStun(State state)
    {
        state.Stun(3);
    }
    public override int GetMaxInstance()
    {
        return maxInstanceNum;
    }
}
