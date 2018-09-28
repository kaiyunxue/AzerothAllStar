using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : KOFItem {
    [Header("CreatureVal")]
    [Header("CreatureCircle")]
    [SerializeField]
    bool isNeedAdjustCirclePosAndLocation = false;
    [SerializeField]
    Vector3 localCirclePos = Vector3.zero;
    [SerializeField]
    float circleScal = 1;

    protected override void Awake()
    {
        base.Awake();
        if (isNeedAdjustCirclePosAndLocation)
            CreatureCircle.Instacne.InstitateCircle(gameObject, localCirclePos, circleScal);
        else
            CreatureCircle.Instacne.InstitateCircle(gameObject);
    }
}
