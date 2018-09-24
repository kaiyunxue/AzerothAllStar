using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FireElement2 : CreatureBehavuourController
{
    static int maxInstanceNum = 20;
    public float AttackDis_Copy;
    public Animator AC;
    public float speed;
    public Transform hand;
    public GameObject effect;
    public HatredCurveTemplate template;
    override protected void OnEnable()
    {
        base.OnEnable();
        hatredCurve = template.mobsCurve;
        AttackDis_Copy = AttackDis;
        effect.SetActive(false);
        StartCoroutine(StartBehave());
    }
    private void Start()
    {
        StartCoroutine(switchTarget());
    }
    IEnumerator StartBehave()
    {
        yield return new WaitForSeconds(1.3f);
        StartCoroutine(watchdog());
    }
    public IEnumerator watchdog()
    {
        yield return null;
        if (Vector3.Distance(target.transform.position, transform.position) >= AttackDis_Copy)
        {
            AC.CrossFade("Run [11]", 0.1f);
            StartCoroutine(run(1.5f));
        }
        else
        {
            AC.CrossFade("ReadySpellOmni [16]", 0.1f);
            effect.SetActive(false);
            effect.SetActive(true);
            StartCoroutine(MagicAttack());
        }
    }
    IEnumerator run(float time)
    {
        yield return null;
        if (time >= 0 && (Vector3.Distance(target.transform.position, transform.position) >= AttackDis_Copy))
        {
            time -= Time.deltaTime;
            gameObject.transform.position += (target.transform.position - transform.position).normalized * Time.deltaTime * speed;
            gameObject.transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
            gameObject.transform.Rotate(0, 90, 0);
            StartCoroutine(run(time));
        }
        else
        {
            StartCoroutine(watchdog());
        }
    }
    IEnumerator MagicAttack()
    {
        yield return new WaitForSeconds(2.0f);
        StartCoroutine(watchdog());
    }
    private void LateUpdate()
    {
        Vector3 t = target.transform.position;
        t.y = gameObject.transform.position.y;
        gameObject.transform.LookAt(t);
        gameObject.transform.Rotate(new Vector3(0, 90, 0));
        effect.transform.position = hand.transform.position;
        Vector3 a; 
        a.x = target.transform.position.x;
        a.y = effect.transform.position.y;
        a.z = target.transform.position.z;
        effect.transform.LookAt(a);
    }
    public override int GetMaxInstance()
    {
        return maxInstanceNum;
    }
    public override void SetTarget(GameObject target)
    {
        base.SetTarget(target);
        //Vector3 targetPos = target.transform.position;
        //targetPos.y = transform.position.y;
        //transform.LookAt(targetPos);
    }
}
