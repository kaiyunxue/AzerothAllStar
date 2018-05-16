using UnityEngine;
using System.Collections;

public class FireAerolite : SkillItemsBehaviourController
{
    static int maxInstanceNum = 10;
    float startHeight;
    public FireElementController fe;
    public Projector projector;
    Material material;
    public bool isSeed = false;
    public GameObject fireExplosion;
    public GameObject fireball;
    public float z;
    public RagnarosDamage damage;

    bool isCollideEarth;    // Use this for initialization
    protected override void Awake()
    {
        isCollideEarth = false;
        damage = new RagnarosDamage(150, DamageType.Fire, gameObject.layer);
        base.Awake();
        isSeed = false;
        material = Instantiate(projector.material);
        projector.material = material;
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        material.SetFloat("_Cutoff", 1);
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        startHeight = transform.position.y;
        fireExplosion.SetActive(false);
        fireball.SetActive(true);
        z = 5;
        float i = Random.Range(0.0f, 9.9f);
    }

    // Update is called once per frame
    void Update()
    {
        if(!isCollideEarth)
            material.SetFloat("_Cutoff", (transform.position.y / startHeight));
    }
    private void OnTriggerEnter(Collider collision)
    {
        isCollideEarth = true;
        Debug.Log(collision.gameObject.name);
        fireball.SetActive(false);
        StartCoroutine(StampDisappear());
        fireExplosion.SetActive(true);
        if (isSeed)
        {
            Vector3 v = gameObject.transform.position;
            v.y = 1f;
            FireElementController e = FireElementController.InstantiateByPool(fe, v, Quaternion.Euler(0, 0, 0), GameController.instance.transform, gameObject.layer,  target);
            isSeed = false;
        }
        if (collision.gameObject.layer == 9 && collision.gameObject.GetComponent<State>() != null)
        {
            collision.gameObject.GetComponent<State>().TakeSkillContent(damage);
        }
    }
    IEnumerator StampDisappear()
    {
        float val = material.GetFloat("_Cutoff");
        val += 0.008f;
        material.SetFloat("_Cutoff", val);
        yield return new WaitForEndOfFrame();
        StartCoroutine(StampDisappear());
    }
    IEnumerator StopEmission(GameObject particle)
    {
        var systems = particle.GetComponentsInChildren<ParticleSystem>();
        fireExplosion.transform.SetParent(GameController.instance.transform);
        fireball.transform.SetParent(GameController.instance.transform);
        yield return new WaitForSeconds(1);
        foreach (ParticleSystem system in systems)
        {
            ParticleSystem.CollisionModule cm = system.collision;
            cm.enabled = true;
            system.Stop();
        }
        fireExplosion.transform.SetParent(transform);
        fireExplosion.transform.localPosition = Vector3.zero;
    }
    public override int GetMaxInstance()
    {
        return maxInstanceNum;
    }
    protected override IEnumerator DestorySelf()
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        isCollideEarth = false;
        return base.DestorySelf();
    }
}


