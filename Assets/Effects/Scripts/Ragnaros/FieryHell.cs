using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieryHell : SkillItemsBehaviourController
{
    [SerializeField]
    GameObject fireWave;
    [SerializeField]
    ParticleSystem wave;
    [SerializeField]
    Projector projector;
    [SerializeField]
    AnimationCurve projectorGrowCurve;

    public ParticleSytemPhysicalController damageController;
    List<GameObject> insideObject = new List<GameObject>();

    protected override void Awake()
    {
        base.Awake();
        damageController.collisionEvent = CollisionAction;
        ParticleSystem.CollisionModule collision = wave.collision;
        if (gameObject.layer == 8)
            collision.collidesWith = 1 << 9;
        else if (gameObject.layer == 9)
            collision.collidesWith = 1 << 8;
    }
    IEnumerator ProjectorAwake(float time)
    {
        yield return new WaitForEndOfFrame();
        if(projector.orthographicSize < 4)
        {
            projector.orthographicSize = projectorGrowCurve.Evaluate(time);
            time += Time.deltaTime;
            StartCoroutine(ProjectorAwake(time));
        }
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(ProjectorAwake(0));
        StartCoroutine(StartWave());
    }
    IEnumerator StartWave()
    {
        fireWave.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        fireWave.SetActive(false);
        insideObject.Clear();
        StartCoroutine(StartWave());
    }
    public void CollisionAction(GameObject other)
    {
        if (other.layer == gameObject.layer)
            return;
        if (other.layer == 10)
            return;
        if (other.GetComponent<State>() == null)
            return;
        if (insideObject.Contains(other))
            return;
        else
            insideObject.Add(other);
        RagnarosDamage dmg = new RagnarosDamage(20, DamageType.Fire, gameObject.layer);
        other.GetComponent<State>().TakeSkillContent(dmg);
    }
}
