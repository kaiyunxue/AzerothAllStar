using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    public string skillName;
    [SerializeField]
    protected float cd;
    protected float timer;
    protected bool Lock;

    public virtual bool IsReady()
    {
        return Lock;
    }
    protected virtual IEnumerator SkillUpdate(Animator animator)
    {
        yield return null;
    }
    protected virtual void Awake()
    {
        timer = cd;
        Lock = true;
        skillName = this.GetType().ToString();
    }
    protected void StartCdColding()
    {
        if(cd > 0)
        {
            StartCoroutine(CDColder());   
        }
    }
    IEnumerator CDColder()
    {
        Lock = false;
        yield return new WaitForSeconds(cd);
        Lock = true;
        yield return null;
    }
}
