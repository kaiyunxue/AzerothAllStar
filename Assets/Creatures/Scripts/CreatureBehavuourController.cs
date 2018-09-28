using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(State))]
public class CreatureBehavuourController : Creature
{
    public float livingTime;
    public float AttackDis;
    public GameObject target;
    public State state;
    protected int leftMob = 0;
    protected int rightMob = 0;
    public FrontTest test;
    protected override void Awake()
    {
        base.Awake();
        state = GetComponent<State>();
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
    }
    //protected IEnumerator FrontTest()
    //{
    //    RaycastHit h;
    //    if(Physics.Raycast(transform.position,transform.right, out h,1f))
    //    {
    //        if (h.collider.gameObject != gameObject && h.collider.gameObject.layer == gameObject.layer && (h.collider.GetComponent<CreatureBehavuourController>() != null || h.collider.GetComponent<Hero>() != null))
    //        {
                
    //        }
    //    }
    //    foreach (var hit in Physics.RaycastAll(transform.position, transform.forward ,1f))
    //    {
    //        if (hit.collider.gameObject != gameObject && hit.collider.gameObject.layer == gameObject.layer && (hit.collider.GetComponent<CreatureBehavuourController>() != null || hit.collider.GetComponent<Hero>() != null))
    //        {
    //            Vector3 dir = hit.collider.transform.position - transform.position;
    //           // Debug.Log(GetComponent<Rigidbody>());
    //            GetComponent<Rigidbody>().AddForce(dir);
    //        }
    //    }
    //    yield return new WaitForEndOfFrame();
    //    StartCoroutine(FrontTest());
    //}

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
