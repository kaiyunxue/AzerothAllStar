using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreaturesState : State {
    protected override float SetHealth(float value)
    {
        GetComponentInChildren<MobStatusBox>().ShowHealth(value / MaxHealth);
        return base.SetHealth(value);
    }
}
