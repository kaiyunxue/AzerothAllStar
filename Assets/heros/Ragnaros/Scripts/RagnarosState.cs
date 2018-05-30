using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagnarosState : HeroState
{
    public float extraMana;
    public Sulfuars sulfuars;
    private void Start()
    {
        Health = MaxHealth;
        Mana = mana;
    }
    protected override float SetHealth(float value)
    {
        var val = base.SetHealth(value);
        statusBox.ShowHealth(val / MaxHealth);
        //Debug.Log(health);
        if(Stage == 0 && health <= 250)
        {
            Stage++;
            hero.animator.SetTrigger("TurnToStandStage");
        }
        return val;
    }
    private void Update()
    {
        if(extraMana >= 1)
        {
            sulfuars.FromMana2Attack();
            extraMana--;
        }
    }
    protected override float SetMana(float value)
    {
        if (value > MaxMana)
        {
            extraMana += value - MaxMana;
            value = MaxMana;
        }
        else if (value < MaxMana && value >= 0)
            extraMana = 0;
        else if (value < 0)
            value = 0;
        float val = value;
        statusBox.ShowMana(val);
        return val;
    }
}
