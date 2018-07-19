using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FireWall : SkillItemsBehaviourController
{
    static int maxInstanceNum;
    public GameObject firewall;
    float factor;
    public Vector3 speed;
    public RagnarosDamage damage;
    // Use this for initialization
    protected override void Awake()
    {
        factor = 1;
        livingTime = 7;
        firewall = gameObject;
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        StartFireWall();
    }
    public void StartFireWall()
    {
        firewall.SetActive(true);
        StartCoroutine(FireWallGo(0));
    }
    IEnumerator FireWallGo(float time)
    {
        if (time == 0)
            yield return new WaitForSeconds(0.3f);
        if(time <= 1f)
        {
            firewall.GetComponent<Renderer>().material.SetFloat("_Cutout", factor);
            factor -= Time.deltaTime;
        }
        if (time <= 6)
        {
            firewall.transform.position += Time.deltaTime * speed;
            time += Time.deltaTime;
            yield return null;
            StartCoroutine(FireWallGo(time));
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9 && other.GetComponent<State>() != null)
            other.GetComponent<State>().TakeSkillContent(damage);
    }
    public override int GetMaxInstance()
    {
        return maxInstanceNum;
    }
}


