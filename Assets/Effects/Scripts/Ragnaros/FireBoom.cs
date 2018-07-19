using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBoom : SkillItemsBehaviourController
{
    new public RagnarosDamage damage;
    public FireTrap fireTrap;
    public FireTrap fireTrapInstance;
    public Vector3 forceDir;
    float t = 0;
    public bool isTriggerEnter = false;
    protected override void Awake()
    {
        base.Awake();
        damage = new RagnarosDamage(10, DamageType.Fire, gameObject.layer);
    }
    protected override void OnEnable()
    {
        t = 0;
        fireTrapInstance = null;
        isTriggerEnter = false;
        if (livingTime > 0)
        {
            StartCoroutine(Live());
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 9 && other.gameObject.GetComponent<State>() != null)
        {
            Debug.Log(other.name);
            //damage.RunContent(other.GetComponent<State>());
            other.GetComponent<State>().TakeSkillContent(damage);
            GameController.Register.FindHeroByLayer(gameObject.layer).state.Mana++;
            var xVal = gameObject.transform.position.x - other.transform.position.x;
            forceDir.x = -xVal * 100;
            forceDir.y = 300;
            forceDir.z = 0;
            var rig = other.GetComponent<Rigidbody>();
            rig.AddForce(forceDir);
            isTriggerEnter = true;
        }
    }
    public override IEnumerator Live()
    {
        yield return new WaitForSeconds(livingTime);
        StartCoroutine(DestorySelf());
    }
    protected override IEnumerator DestorySelf()
    {
        Destroy(gameObject);
        yield return null;
    }
    private void Update()
    {
        t += Time.deltaTime;
        if(t >= 1.7f)
        {
            if (!isTriggerEnter && fireTrapInstance == null)
            {
                Vector3 pos = transform.position;
                pos.y = 0.05f;
                fireTrapInstance = InstantiateByPool(fireTrap, pos, Quaternion.Euler(0, 0, 0), GameController.instance.transform, gameObject.layer, true);
            }
            //this.enabled = false;
        }
    }
}
