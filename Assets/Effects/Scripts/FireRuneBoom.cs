using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRuneBoom : SkillItemsBehaviourController
{
    static int maxInstanceNum = 70;
    public Material[] runeMaterials;
    public Projector[] projectors;
    Material[] runeMaterialsIntance = new Material[2];
    public GameObject effect;
    float value = 0;
    protected override void OnEnable()
    {
        base.OnEnable();
        runeMaterials[1].SetFloat("_Cutoff", 0);
    }
    protected override void Awake()
    {
        base.Awake();
        runeMaterialsIntance[0] = Instantiate(runeMaterials[0]);
        runeMaterialsIntance[1] = Instantiate(runeMaterials[1]);
        projectors[0].material = runeMaterialsIntance[0];
        projectors[1].material = runeMaterialsIntance[1];
    }
    public void StartSkill()
    {
        effect.SetActive(true);
        Vector3 pos = gameObject.transform.position;
        pos.y = 0.1f;
        StartCoroutine(Behave());
    }
    IEnumerator Behave()
    {
        yield return null;
        if(value <= 1)
        {
            runeMaterialsIntance[0].SetFloat("_Cutoff", value);
            runeMaterialsIntance[1].SetFloat("_Cutoff", value);
            value += Time.deltaTime;
            StartCoroutine(Behave());
        }
        else
        {
            yield return new WaitForSeconds(2);
            StartCoroutine(DestorySelf());
        }
    }
    public override int GetMaxInstance()
    {
        return maxInstanceNum;
    }
    protected override IEnumerator DestorySelf()
    {
        effect.SetActive(false);
        runeMaterialsIntance[0].SetFloat("_Cutoff", 0);
        runeMaterialsIntance[1].SetFloat("_Cutoff", 0);
        return base.DestorySelf();
    }
}
