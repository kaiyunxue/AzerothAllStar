
using UnityEngine;
using System.Collections;

public class RFX4_EffectSettingProjectile : SkillItemsBehaviourController
{
    public float FlyDistanceForProjectiles = 30;
    public float SpeedMultiplier = 1;
    public LayerMask CollidesWith = ~0;

    float startSpeed;
    const string particlesAdditionalName = "Distance";

    protected override void Awake()
    {
        var transformMotion = GetComponentInChildren<RFX4_TransformMotion>(true);
        if (transformMotion != null)
        {
            startSpeed = transformMotion.Speed;
        }
    }

    protected override void OnEnable()
    {
        var transformMotion = GetComponentInChildren<RFX4_TransformMotion>(true);
        if (transformMotion != null)
        {
            transformMotion.Distance = FlyDistanceForProjectiles;
            transformMotion.CollidesWith = CollidesWith;
            transformMotion.Speed = startSpeed * SpeedMultiplier;
        }
        var rayCastCollision = GetComponentInChildren<RFX4_RaycastCollision>(true);
        if (rayCastCollision != null) rayCastCollision.RaycastDistance = FlyDistanceForProjectiles;
        var particlesystems = GetComponentsInChildren<ParticleSystem>();
        foreach (var ps in particlesystems)
        {

            if (ps.name.Contains(particlesAdditionalName))
#if !UNITY_5_5_OR_NEWER
                ps.GetComponent<ParticleSystemRenderer>().lengthScale = FlyDistanceForProjectiles / ps.startSize;
#else
                ps.GetComponent<ParticleSystemRenderer>().lengthScale = FlyDistanceForProjectiles / ps.main.startSize.constantMax;
#endif
        }
    }
}
