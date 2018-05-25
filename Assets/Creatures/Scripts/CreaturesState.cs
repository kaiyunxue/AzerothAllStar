using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreaturesState : State {
    public MobStatusBox box;
    private void Awake()
    {
        box = GetComponentInChildren<MobStatusBox>();
    }
    protected override float SetHealth(float value)
    {
         box.ShowHealth(value / MaxHealth);
        return base.SetHealth(value);
    }
}
