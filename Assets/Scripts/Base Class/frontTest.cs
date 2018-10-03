using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontTest
{
    public int leftMobs;
    public int rightMobs;
    KOFItem t;
    Coroutine coroutine;
    public FrontTest(KOFItem t)
    {
        leftMobs = 0;
        rightMobs = 0;
        this.t = t;
    }
    public void StartForntTest()
    {
        leftMobs = 0;
        rightMobs = 0;
        coroutine = t.StartCoroutine(frontTest());
    }
    public void StopForntTest<T>()
    {
        t.StopCoroutine(coroutine);
    }
    public void Refresh()
    {
        leftMobs = 0;
        rightMobs = 0;
    }
    IEnumerator frontTest()
    {
        Transform transform = t.transform;
        RaycastHit h;
        if (Physics.Raycast(transform.position, transform.right, out h, 1f))
        {
            if (h.collider.gameObject != transform.gameObject  && h.collider.gameObject.layer == transform.gameObject.layer && (h.collider.GetComponent<CreatureBehavuourController>() != null || h.collider.GetComponent<Hero>() != null))
            {
                rightMobs++;
                Collider col = h.collider;
                if(col.GetComponent<CreatureBehavuourController>() != null)
                {
                    col.GetComponent<CreatureBehavuourController>().test.leftMobs++;
                }
                if (col.GetComponent<Hero>() != null)
                {
                    col.GetComponent<Hero>().test.leftMobs++;
                }
            }
        }
        foreach (var hit in Physics.RaycastAll(transform.position, transform.forward, 1f))
        {
            if (hit.collider.gameObject != transform.gameObject && hit.collider.gameObject.layer == transform.gameObject.layer && (hit.collider.GetComponent<CreatureBehavuourController>() != null || hit.collider.GetComponent<Hero>() != null))
            {
                int v = t.transform.position.z >= 0 ? 1 : -1;
                float val = v;
                int x = leftMobs - rightMobs;
                if(x >= 0)
                    val = (Mathf.Log10(x + 1) + 10) * val;
                else
                    val = (Mathf.Log10(-x + 1) + 10) * val;
                Vector3 dir = -t.transform.right;
                //Vector3 dir = hit.collider.transform.position - transform.position;
                //// Debug.Log(GetComponent<Rigidbody>());
                transform.GetComponent<Rigidbody>().AddForce(val * dir);
            }
        }
        yield return new WaitForEndOfFrame();
        coroutine = t.StartCoroutine(frontTest());
    }
}
