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
    protected virtual KOFItem getMaxHatredObject()
    {
        HerosRegistrar enemyRegister = null;
        if(gameObject.layer == 8)
        {
            enemyRegister = GameController.register.RightHero.HeroRegister;
        }
        else if(gameObject.layer == 9)
        {
            enemyRegister = GameController.register.LeftHero.HeroRegister;
        }
        else
        {
            Debug.LogError(gameObject.name + ": Can't find its layer");
        }
        float maxHatredVal = 0;
        KOFItem target = null;
        foreach(var v in enemyRegister.GetAllGameItems())
        {
            if(v.GetComponent<KOFItem>() == null)
            {
                Debug.LogError("代码写的不好，需要改------- 不是一个有效的kofitem");
            }
            else
            {
                float distance = Vector3.Distance(transform.position, v.transform.position);
                float hatredVal = v.GetComponent<KOFItem>().hatredCurve.Evaluate(distance);
                Debug.Log("name: " + v.name + " distance:" + distance + " hatred: " + hatredVal);
                if(hatredVal > maxHatredVal)
                {
                    maxHatredVal = hatredVal;
                    target = v.GetComponent<KOFItem>();
                }
            }
        }
        return target;
    }
}
