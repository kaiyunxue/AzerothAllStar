using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class State : MonoBehaviour
{
    [SerializeField]
    protected float health;
    [SerializeField]
    protected float mana;
    public float MaxHealth;
    public float MaxMana;
    public int Defense;
    public int Stage;

    void Awake()
    {
    }
    public float Health
    {
        get
        {
            return health;
        }

        set
        {
            health = value;
            value = SetHealth(value);
        }
    }
    public float Mana
    {
        get
        {
            return mana;
        }

        set
        {
            value = SetMana(value);
            mana = value;
        }
    }
    protected virtual float SetHealth(float value)
    {
        if (value > MaxHealth)
            value = MaxHealth;
        else if (value < 0)
            value = 0;
        return value;
    }
    protected virtual float SetMana(float value)
    {
        if (value > MaxMana)
            value = MaxMana;
        else if (value < 0)
            value = 0;
        return value;
    }
    public virtual void TakeSkillContent(Damage damage) { }
    public virtual void TakeSkillContent(Dot dot){}
    public virtual void TakeSkillContent(Debuff dot) { }
    public virtual void TakeSkillContent(Buff dot) { }
    public virtual void Stun(float time) { }
    public virtual void StateInit()
    {
        health = MaxHealth;
        mana = MaxMana;
    }
}


