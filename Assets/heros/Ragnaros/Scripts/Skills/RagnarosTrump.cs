using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagnarosTrump : HeroSkill, ISkill
{
    public GameObject sulfuras;
    public FireWall fireWall;
    public TrumpStamp fireStamp;

    public override void StartSkill(Animator animator)
    {
        StartCoroutine(Trump());
        StartCoroutine(isAttackGround(0));
    }

    public override void StopSkill(Animator animator)
    {
        StartCdColding();
    }
    public override bool IsReady()
    {
        if (!Lock)
            return false;
        if (!GameController.LeftInputListener.GetSkill(formula))
            return false;
        return true;
    }
    public override bool TryStartSkill(Animator animator)
    {
        if (IsReady())
        {
            animator.SetTrigger("Trump");
            return true;
        }
        else
            return false;
    }
    IEnumerator Trump()
    {
        yield return new WaitForSeconds(0.9f);
        sulfuras.transform.localRotation = Quaternion.Euler(0, 70, 0);
        yield return null;
        yield return new WaitForSeconds(0.9f);
        sulfuras.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }
    IEnumerator isAttackGround(float time)
    {
        time += Time.deltaTime;
        if (!sulfuras.GetComponent<Sulfuars>().isCollide || time <= 1f)
        {
            yield return new WaitForEndOfFrame();
            StartCoroutine(isAttackGround(time));
        }
        else
        {
            yield return null;
            Vector3 pos = sulfuras.transform.localPosition ;
            pos.y = 0;
            KOFItem.InstantiateByPool(fireStamp,pos, Quaternion.Euler(0,90,0),GameController.instance.transform, gameObject.layer);
            GameObject[] traps = GameController.Register.LeftHero.HeroRegister.SearchCompontent("FireTrap");
            GameObject[] nearbytrips;
            int n_near;
            n_near = isTrapNearby(traps, out nearbytrips);
            if (n_near != 0)
            {
                FireWall instance =  KOFItem.InstantiateByPool(fireWall, pos, Quaternion.Euler(0, 90, 0), GameController.instance.transform,gameObject.layer);
                instance.damage = new RagnarosDamage(n_near * 50, DamageType.Fire, gameObject.layer);
                foreach (GameObject go in nearbytrips)
                {
                    go.GetComponent<FireTrap>().BeTrumped();
                }
            }
            //gameObject.GetComponent<RagnarosFireWall>().StartSkill(n_near);
        }
    }
    int isTrapNearby(GameObject[] traps, out GameObject[] nearbytraps)
    {
        int i = 0;
        List<GameObject> nearbytraplist = new List<GameObject>();
        if (traps == null)
        {
            nearbytraps = null;
            return 0;
        }
        foreach (GameObject t in traps)
        {
            if (t != null)
            {
                float dis = Vector3.Distance(sulfuras.transform.position, t.transform.position);
                //Debug.Log(dis);
                if (dis <= 1.5f && t.GetComponent<FireTrap>().IsWaiting())
                {
                    i++;
                    nearbytraplist.Add(t);
                }
            }
        }
        nearbytraps = nearbytraplist.ToArray();
        return i;
    }
}