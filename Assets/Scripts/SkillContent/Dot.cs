using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : SkillContent
{
    public DamageDelegate doSkillEffect;
    Damage perDamage;
    float frequency;
    float totalTime;

    public Dot(Damage damage, float frequency, float totalTime, DamageDelegate effect)
    {
        perDamage = damage;
        this.frequency = frequency;
        this.totalTime = totalTime;
        doSkillEffect = effect;
    }

    public Damage PerDamage
    {
        get
        {
            return perDamage;
        }
    }

    public float Frequency
    {
        get
        {
            return frequency;
        }
    }

    public float TotalTime
    {
        get
        {
            return totalTime;
        }
    }
}
