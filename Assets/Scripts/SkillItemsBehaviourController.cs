using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillItemsBehaviourController : KOFItem {
    public KOFItem speller;
    public Damage damage;
    public GameObject target;
    public virtual void SetTarget(GameObject target)
    {
        this.target = target;
    }
    float time = 0;
    public float livingTime = 0;
    public void setlivingTime(float t)
    {
        livingTime = t;
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(Live());
    }
    public virtual IEnumerator Live()
    {
        if(livingTime > 0)
        {
            yield return new WaitForSeconds(livingTime);
            StartCoroutine(DestorySelf());
        }
    }
    protected virtual IEnumerator DestorySelf()
    {
        DestoryByPool(this);
        yield return null;
    }
}
