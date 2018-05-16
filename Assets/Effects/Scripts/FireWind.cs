using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FireWind : SkillItemsBehaviourController
{
    static int maxInstanceNum = 1;
    Material W;
    Material w;
    public float f = 0;
    public Color C;
    public Color c;
    float t = 0;
    protected override void OnEnable()
    {
        base.OnEnable();
        W = transform.GetChild(0).GetComponent<MeshRenderer>().material;
        w = transform.GetChild(1).GetComponent<MeshRenderer>().material;
        W.SetColor("_TintColor", C);
        w.SetColor("_TintColor", c);
        StartCoroutine(Blow());
        StartCoroutine(Appear());
    }
    IEnumerator Blow()
    {
        target = GameController.Register.RightHero.gameObject;//!!!!!!!!!!!!!!!!!!!!
        target.transform.position += new Vector3(0.005f, 0, 0);//!!!!!Transform will be changed by the turn of hero. It will be revised in the future. 
        t += Time.deltaTime;
        f += 0.001f;
        W.SetTextureOffset("_MainTex", new Vector2(0, 10 * f));
        w.SetTextureOffset("_MainTex", new Vector2(0, 3 * f));
        if (t <= 10)
        {
            yield return null;
            StartCoroutine(Blow());
        }
        else
        {
            yield return null;
            StartCoroutine(Blow());
            StartCoroutine(Destory());
        }
    }
    IEnumerator Appear()
    {
        if (C.a <= 0.1f && c.a <= 0.5f)
        {
            C.a += 0.001f;
            c.a += 0.005f;
            W.SetColor("_TintColor", C);
            w.SetColor("_TintColor", c);
            yield return null;
            StartCoroutine(Appear());
        }
    }
    IEnumerator Destory()
    {
        if (C.a >= 0 && c.a >= 0)
        {
            C.a -= 0.001f;
            c.a -= 0.005f;
            W.SetColor("_TintColor", C);
            w.SetColor("_TintColor", c);
            yield return null;
        }
        else
        {
            yield return null;
            DestoryByPool(this);
        }
    }
    public override int GetMaxInstance()
    {
        return maxInstanceNum;
    }
}


