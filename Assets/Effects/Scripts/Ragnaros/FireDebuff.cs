using UnityEngine;
using System.Collections;

public class FireDebuff : SkillItemsBehaviourController
{
    public GameObject effect;
    Dot dot;
    protected override void Awake()
    {
        base.Awake();
        effect.SetActive(false);
        RagnarosDamage damage = new RagnarosDamage(1, DamageType.Fire, gameObject.layer);
        damage.doSkillEffect += runDot;
        dot = new Dot(damage, 5f, 0, null);
    }
    public IEnumerator GoFireDebuff(GameObject target)
    {
        transform.SetParent(GameController.instance.transform);
        Vector3 dir = (target.transform.position + new Vector3(0, 0.6f, 0) - gameObject.transform.position).normalized / 10;
        if (Vector3.Distance(target.transform.position + new Vector3(0, 0.6f, 0), gameObject.transform.position) > 0.1f)
        {

            yield return null;
            gameObject.transform.position += (dir + new Vector3(Random.Range(-0.05f, 0.05f), Random.Range(-0.05f, 0.05f), Random.Range(0.05f, 0.05f)));
            StartCoroutine(GoFireDebuff(target));
        }
        else
        {
            gameObject.transform.SetParent(target.transform);
            target.GetComponent<State>().TakeSkillContent(dot);
            yield return null;
        }
    }
    void runDot()
    {
        effect.SetActive(false);
        effect.SetActive(true);
    }
}

