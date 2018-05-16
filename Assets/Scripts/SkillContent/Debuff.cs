using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debuff : SkillContent
{
    public BuffDelegate buffEffect;
    public Debuff(BuffDelegate effect)
    {
        this.buffEffect = effect;
    }
}
