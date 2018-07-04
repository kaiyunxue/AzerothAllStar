using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(ParticleSystem))]
public class RFX4_ParticleCollisionHandler : MonoBehaviour
{
    public GameObject[] EffectsOnCollision;
    public float Offset = 0;
    public float DestroyTimeDelay = 5;
    public bool UseWorldSpacePosition;

    private ParticleSystem part;
#if !UNITY_5_5_OR_NEWER
    private ParticleCollisionEvent[] collisionEvents;
#else
    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
#endif
    private ParticleSystem ps;

    void Start()
    {
        part = GetComponent<ParticleSystem>();
#if !UNITY_5_5_OR_NEWER
        collisionEvents = new ParticleCollisionEvent[16];
#endif
    }
    void OnParticleCollision(GameObject other)
    {
#if !UNITY_5_5_OR_NEWER
        int safeLength = part.GetSafeCollisionEventSize();
        if (collisionEvents.Length < safeLength)
            collisionEvents = new ParticleCollisionEvent[safeLength];

        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);
#else
        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);
#endif
        int i = 0;
        while (i < numCollisionEvents)
        {
            foreach (var effect in EffectsOnCollision)
            {
                var instance = Instantiate(effect, collisionEvents[i].intersection + collisionEvents[i].normal * Offset, new Quaternion()) as GameObject;
                instance.transform.LookAt(collisionEvents[i].intersection + collisionEvents[i].normal);
                if(!UseWorldSpacePosition)instance.transform.parent = transform;
                Destroy(instance, DestroyTimeDelay);
            }
            i++;
        }
    }
}
