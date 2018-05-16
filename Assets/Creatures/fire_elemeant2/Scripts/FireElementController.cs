using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityStandardAssets.Effects;
public class FireElementController : CreatureBehavuourController
{
    static int maxInstanceNum = 1;
    public AnimationCurve speedCurve;
    public Vector3 adjustVector;
    public float speed;
    public GameObject Explosion;
    public GameObject explosion;
    public float maxspeed;
    public Animator AC;
    protected override void Awake()
    {
        base.Awake();
        state = GetComponent<State>();
    }
    protected IEnumerator Behave()
    {
        yield return new WaitForSecondsRealtime(2);
        StartCoroutine(Walk());
        StartCoroutine(Monitor_Die());
        StartCoroutine(Monitor_Done());
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        state = gameObject.GetComponent<State>();
        AC = gameObject.GetComponent<Animator>();
        Vector3 pos;
        pos.x = target.transform.position.x;
        pos.z = target.transform.position.z;
        pos.y = transform.position.y;
        gameObject.transform.LookAt(pos);
        gameObject.transform.Rotate(new Vector3(0, 90, 0));
        StartCoroutine(Monitor_Die());
        StartCoroutine(Behave());
    }
    IEnumerator Monitor_Done()
    {
        System.Func<bool> isdone = new System.Func<bool>(IsDone);
        yield return new WaitUntil(isdone);
        StartCoroutine(Die());
    }
    IEnumerator Walk()
    {
        if (target == null)
        {
            StartCoroutine(Die());
        }
        if (Vector3.Distance(gameObject.transform.position, target.transform.position) >= 1f)
        {
            yield return new WaitForEndOfFrame();
            Vector3 v = target.transform.position;
            v.y = gameObject.transform.position.y;
            gameObject.transform.LookAt(v);
            gameObject.transform.Rotate(new Vector3(0, 90, 0));
            // CC.Move((Suf.transform.position - gameObject.transform.position).normalized * Time.deltaTime * speed);
            transform.position += (target.transform.position - gameObject.transform.position).normalized * Time.deltaTime * speed;
            //gameObject.transform.position += (Suf.transform.position - gameObject.transform.position).normalized * Time.deltaTime * speed;
            StartCoroutine(Walk());
        }
        else
        {
            GameObject go = Instantiate(Explosion, null);
            GameObject[] objects = GameController.Register.RightHero.HeroRegister.GetAllGameItems();
            int num = 0;
            foreach (GameObject gObject in objects)
            {
                if (gObject.GetComponent<State>() != null)
                {
                    num++;
                }
            }
            float damageVal = 1000 / num;
            if (damageVal > 250) damageVal = 250;
            RagnarosDamage damage = new RagnarosDamage(damageVal, DamageType.Fire, gameObject.layer);
            foreach (GameObject gObject in objects)
            {
                if (gObject.GetComponent<State>() != null)
                {
                    gObject.GetComponent<State>().TakeSkillContent(damage);
                }
            }
            go.transform.position = target.transform.position + new Vector3(0, 0.3f, 0);
            go.GetComponent<ParticleSystemMultiplier>().multiplier = 1f;
            DestoryByPool(this);
        }
    }
    IEnumerator Monitor_Die()
    {
        System.Func<bool> isdie = new System.Func<bool>(IsDie);
        yield return new WaitUntil(isdie);
        StartCoroutine(Die());
    }
    public override IEnumerator Die()
    {
        RagnarosSubmergeAttack skill = GameController.Register.LeftHero.skillManager.GetSkillByName("RagnarosSubmergeAttack") as RagnarosSubmergeAttack;
        skill.Float(GameController.Register.LeftHero.animator);
        AC.CrossFade("Death [12] 0", 0.1f);
        yield return new WaitForSeconds(2);
        GameObject[] objects = GameController.Register.RightHero.HeroRegister.GetAllGameItems();
        RagnarosDamage damage = new RagnarosDamage(50, DamageType.Fire, gameObject.layer);
        foreach (GameObject gObject in objects)
        {
            if (gObject.GetComponent<State>() != null)
            {
                gObject.GetComponent<State>().TakeSkillContent(damage);
            }
        }
        GameObject go_ = Instantiate(explosion, null);
        go_.transform.position = gameObject.transform.position - new Vector3(0, 0.7f, 0);
        //Destroy(gameObject);
        DestoryByPool(this);
    }
    bool IsDie()
    {
        bool isdie = false;
        isdie = (state.Health > 0) ? false : true;
        return isdie;
    }
    bool IsDone()
    {
        bool r;
        r = (target != null) ? false : true;
        return r;
    }
    private void Update()
    {
        speed = speedCurve.Evaluate(state.Health / state.MaxHealth);
    }
    public static FireElementController InstantiateByPool(FireElementController item, Vector3 worldPos, Quaternion worldRot, Transform parent, int layer, GameObject target)
    {
        FireElementController instance = InstantiateByPool(item);
        instance.gameObject.layer = layer;
        instance.transform.SetParent(parent, false);
        instance.transform.position = worldPos;
        instance.transform.rotation = worldRot;
        instance.target = target;
        instance.gameObject.SetActive(true);
        return instance;
    }
    public override int GetMaxInstance()
    {
        return maxInstanceNum;
    }
}
