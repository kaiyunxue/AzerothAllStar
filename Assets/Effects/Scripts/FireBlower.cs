using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBlower : SkillItemsBehaviourController
{
    static int maxInstanceNum = 2;
    public RagnarosDamage damage;
    public Vector3 forceDir;
    public Vector3 speed;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            var rig = other.GetComponent<Rigidbody>();
            rig.AddForce(forceDir);
            other.GetComponent<State>().TakeSkillContent(damage);
        }
    }
    private void Update()
    {
        gameObject.transform.position += speed;
    }
    public override int GetMaxInstance()
    {
        return maxInstanceNum;
    }
}
