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
        currentCoroutine = StartCoroutine(Out());
    }
    public void ZoomOut()
    {
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(zoomOut());

    }
    IEnumerator Out()
    {
        if (t >= 4)
        {
            t = 4;
            yield return null;
        }
        else
        {
            float tmp = curve.Evaluate(t);
            var c = GetComponent<Image>().color;
            c.a = tmp;
            t += 0.03f;
            GetComponent<Image>().color = c;
            yield return new WaitForFixedUpdate();
            currentCoroutine = StartCoroutine(Out());
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
            var c = GetComponent<Image>().color;
            c.a = tmp;
            t -= 0.03f;
            GetComponent<Image>().color = c;
            yield return new WaitForFixedUpdate();
            currentCoroutine = StartCoroutine(Out());
        }
    }
}
