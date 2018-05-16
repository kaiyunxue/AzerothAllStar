using System;
using UnityEngine;
using System.Collections;

public class RFX4_EffectSettingPhysxForce : MonoBehaviour
{

    public float ForceMultiplier = 1;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if (Math.Abs(previousForceMultiplier - ForceMultiplier) > 0.001f)
        {
          var transformMotion = GetComponentInChildren<RFX4_TransformMotion>(true);
            if (transformMotion != null)
            {
                var instances = transformMotion.CollidedInstances;
                foreach (var instance in instances)
                {
                    var physxForceCurve = instance.GetComponent<RFX4_PhysicsForceCurves>();
                    if (physxForceCurve != null) physxForceCurve.forceAdditionalMultiplier = ForceMultiplier;
                }
            }
            var physxForceCurves = GetComponentsInChildren<RFX4_PhysicsForceCurves>();
            foreach (var physxForceCurve in physxForceCurves)
            {
                if (physxForceCurve != null) physxForceCurve.forceAdditionalMultiplier = ForceMultiplier;
            }
        }
    }
}
