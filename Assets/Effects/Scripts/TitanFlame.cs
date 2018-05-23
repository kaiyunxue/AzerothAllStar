using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitanFlame : SkillItemsBehaviourController
{
    public float rotateSpeed;
    public static int maxNum = 3;
    // Update is called once per frame
    protected override void OnEnable()
    {
        base.OnEnable();
        foreach (var p in GetComponentsInChildren<ParticleSystem>())
        {
            p.Play();
        }

    }
    public void Stop()
    {
        foreach(var p in GetComponentsInChildren<ParticleSystem>())
        {
            p.Stop();
        }
    }
	void Update () {
        transform.Rotate(transform.up, rotateSpeed);
	}
    public override int GetMaxInstance()
    {
        return maxNum;
    }
}
