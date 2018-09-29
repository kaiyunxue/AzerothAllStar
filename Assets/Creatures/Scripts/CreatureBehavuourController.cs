using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CreaturesState))]
public class CreatureBehavuourController : Creature
{
    [Header("CreatureBehavuourControllerVal")]
    public float livingTime;
    public float attackDis;
    public GameObject target;
    public CreaturesState state;
    protected int leftMob = 0;
    protected int rightMob = 0;
    public FrontTest test;
    public bool isOnGround;
    protected override void Awake()
    {
        base.Awake();
        state = GetComponent<CreaturesState>();
        test = new FrontTest(this);
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
        base.OnEnable();
        state.StateInit();
        StartCoroutine(Live());
        test.StartForntTest();
        StartCoroutine(watchForDeath());
    }
    public virtual IEnumerator watchForDeath()
    {
        if (state.Health <= 0)
        {
            StartCoroutine(Die());
            yield return null;
        }
        else
        {
            yield return new WaitForEndOfFrame();
            StartCoroutine(watchForDeath());
        }
    }

    protected virtual IEnumerator isOnSky(float height = 0.2f)
    {
        Ray ray = new Ray(transform.position, -transform.up);
        if (!Physics.Raycast(ray, height))
        {
            isOnGround = false;
        }
        else
        {
            isOnGround = true;
        }
        yield return new WaitForEndOfFrame();
        StartCoroutine(isOnSky(height));
    }
    protected virtual IEnumerator switchTarget()
    {
        SetTarget(getMaxHatredObject().gameObject);
        yield return new WaitForSeconds(1);
        StartCoroutine(switchTarget());
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
