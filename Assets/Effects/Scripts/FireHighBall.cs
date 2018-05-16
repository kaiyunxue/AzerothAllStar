using UnityEngine;
using System.Collections;


public class FireHighBall : SkillItemsBehaviourController
{
    static int maxInstanceNum = 5;
    protected override void Awake()
    {
        base.Awake();
    }
    public FireTrap fireTrap;
    public GameObject FireBall;
    public GameObject FireExplosion;
    public RagnarosDamage damage;
    bool IsTrap;

    protected override void OnEnable()
    {
        IsTrap = false;
        FireExplosion.SetActive(false);
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        base.OnEnable();
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer != 8)
        {
            if (col.gameObject.layer == 9)
            {
                col.GetComponent<State>().TakeSkillContent(damage);
            }
            StopEmission(FireBall);
            FireExplosion.SetActive(true);
        }
        // FireBall.SetActive(false);
    }
    void StopEmission(GameObject particle)
    {
        var systems = particle.GetComponentsInChildren<ParticleSystem>();
        FireExplosion.transform.SetParent(GameController.instance.transform);
        FireBall.transform.SetParent(GameController.instance.transform);
        foreach (ParticleSystem system in systems)
        {
            ParticleSystem.CollisionModule cm = system.collision;
            cm.enabled = true;
            system.Stop();
        }
        if (!IsTrap)
        {
            StartCoroutine(DestroyParticle());
            Vector3 p = transform.localPosition;
            p = new Vector3(p.x, 0.1f, p.z);
            FireTrap go = InstantiateByPool(fireTrap,p, GameController.instance.transform, gameObject.layer);
            IsTrap = true;
        }
    }
    IEnumerator DestroyParticle()
    {
        yield return new WaitForSeconds(6);
        gameObject.SetActive(false);
        FireBall.transform.SetParent(transform);
        FireExplosion.transform.SetParent(transform);
        FireBall.transform.localPosition = Vector3.zero;
        FireExplosion.transform.localPosition = Vector3.zero;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        DestoryByPool(this);
    }
    public override int GetMaxInstance()
    {
        return maxInstanceNum;
    }
}


