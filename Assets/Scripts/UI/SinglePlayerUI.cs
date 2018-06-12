using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SinglePlayerUI : MonoBehaviour {
    public AnimationCurve curve;
    Coroutine currentCoroutine;
    float t = 0;
    public void In()
    {
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(zoomIn());
    }
    public void Out()
    {
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(zoomOut());

    }
    IEnumerator zoomIn()
    {
        if (t >= 1)
        {
            t = 1;
            yield return null;
        }
        else
        {
            float tmp = curve.Evaluate(t);
            Graphic[] tmps = GetComponentsInChildren<Graphic>();
            foreach(var v in tmps)
            {
                var c = v.color;
                c.a = tmp;
                t += 0.005f;
                v.color = c;
            }
            yield return new WaitForFixedUpdate();
            currentCoroutine = StartCoroutine(zoomIn());
        }
    }
    IEnumerator zoomOut()
    {
        if (t <= 0)
        {
            t = 0;
            yield return null;
        }
        else
        {
            float tmp = curve.Evaluate(t);
            Graphic[] tmps = GetComponentsInChildren<Graphic>();
            foreach (var v in tmps)
            {
                var c = v.color;
                c.a = tmp;
                t -= 0.005f;
                v.color = c;
            }
            yield return new WaitForFixedUpdate();
            currentCoroutine = StartCoroutine(zoomIn());
        }
    }
}
