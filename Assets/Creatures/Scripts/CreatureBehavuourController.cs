using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(State))]
public class CreatureBehavuourController : KOFItem
{
    public float livingTime;
    public float AttackDis;
    public GameObject target;
    public State state;
    protected override void Awake()
    {
        base.Awake();
        state = GetComponent<State>();
    }
    public void setlivingTime(float t)
    {
        livingTime = t;
    }
    protected virtual IEnumerator Live()
    {
        if (livingTime > 0)
        {
            yield return new WaitForSecondsRealtime(livingTime);
            StartCoroutine(Die());
        }
    }
    public virtual void SetTarget(GameObject target)
    {
        this.target = target;
    }
    protected override void OnEnable()
    {
        state.StateInit();
        StartCoroutine(Live());
    }
    public virtual IEnumerator Die()
    {
        yield return null;
        DestoryByPool(this);
    }
}
