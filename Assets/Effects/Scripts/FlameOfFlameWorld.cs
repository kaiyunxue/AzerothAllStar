using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameOfFlameWorld : SkillItemsBehaviourController {
    static int maxInstanceNum = 42;
    public RagnarosDamage damage = new RagnarosDamage(150, DamageType.Fire, 8);
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            //damage.RunContent(other.GetComponent<State>());
            other.GetComponent<State>().TakeSkillContent(damage);
        }
    }
    public override int GetMaxInstance()
    {
        return maxInstanceNum;
    }
}
