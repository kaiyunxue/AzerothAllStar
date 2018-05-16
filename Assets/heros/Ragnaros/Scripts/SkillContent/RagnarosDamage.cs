using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagnarosDamage : Damage
{
    int layer;
    public RagnarosDamage(float damage, DamageType damageType, int layer) : base(damage, damageType)
    {
        this.layer = layer;
        doSkillEffect = RunContent;
    }
    public void RunContent()
    {
        if (damage == 0)
            return;
        if(Random.Range(1,9) < 4)
            GameController.Register.FindHeroByLayer(layer).state.Mana += 0.3f;
    }
}
