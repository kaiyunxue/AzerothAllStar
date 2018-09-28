using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : KOFItem {
    protected override void Awake()
    {
        base.Awake();
        CreatureCircle.Instacne.InstitateCircle(gameObject);
    }
}
