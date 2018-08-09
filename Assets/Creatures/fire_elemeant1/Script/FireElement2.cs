using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FireElement2 : CreatureBehavuourController
{
    static int maxInstanceNum = 20;
    public GameObject Target;
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
        Target = GameController.Register.RightHero.gameObject;
        AttackDis_Copy = AttackDis;
        effect.SetActive(false);
        StartCoroutine(StartBehave());
    }
    IEnumerator StartBehave()
    {
        yield return new WaitForSeconds(1.3f);
        StartCoroutine(watchdog());
    }
    public IEnumerator watchdog()
    {
        yield return null;
        if (Vector3.Distance(Target.transform.position, transform.position) >= AttackDis_Copy)
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
        if (time >= 0 && (Vector3.Distance(Target.transform.position, transform.position) >= AttackDis_Copy))
        {
            time -= Time.deltaTime;
            gameObject.transform.position += (Target.transform.position - transform.position).normalized * Time.deltaTime * speed;
            gameObject.transform.LookAt(new Vector3(Target.transform.position.x, transform.position.y, Target.transform.position.z));
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
        Vector3 t = Target.transform.position;
        t.y = gameObject.transform.position.y;
        gameObject.transform.LookAt(t);
        gameObject.transform.Rotate(new Vector3(0, 90, 0));
        effect.transform.position = hand.transform.position;
        Vector3 a; 
        a.x = Target.transform.position.x;
        a.y = effect.transform.position.y;
        a.z = Target.transform.position.z;
        effect.transform.LookAt(a);
    }
    public override int GetMaxInstance()
    {
        return maxInstanceNum;
    }
}
