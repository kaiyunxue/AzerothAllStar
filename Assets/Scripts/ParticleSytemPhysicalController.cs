using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public delegate void CollisionEvent(GameObject other);

[RequireComponent(typeof(ParticleSystem))]
public class ParticleSytemPhysicalController : MonoBehaviour
{
    public CollisionEvent collisionEvent;
    private void OnParticleCollision(GameObject other)
    {
        collisionEvent(other);
    }
}
