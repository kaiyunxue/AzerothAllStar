using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : SkillContent
{
    public DamageDelegate doSkillEffect;
    protected float damage;
    DamageType damageType;

    public Damage(float damage, DamageType damageType)
    {
        this.damage = damage;
        this.damageType = damageType;
    }

    public float skillDamage
    {
        get
        {
            return damage;
        }
    }
}
