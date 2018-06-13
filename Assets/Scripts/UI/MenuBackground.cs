using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBackground : MonoBehaviour {
    public AnimationCurve curve;
    Coroutine currentCoroutine;
    float t = 0;
    public void ZoomIn()
    {
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(zoomIn());
    }
    public void ZoomOut()
    {
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(zoomOut());

    }
    IEnumerator zoomIn()
    {
        if(t >= 1)
        {
            t = 1;
            yield return null;
        }
        else
        {
            float tmp = curve.Evaluate(t);
            transform.localScale = new Vector3(tmp, tmp, tmp);
            t += 0.03f;
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
            transform.localScale = new Vector3(tmp, tmp, tmp);
            t -= 0.03f;
            yield return new WaitForFixedUpdate();
            currentCoroutine = StartCoroutine(zoomOut());
        }
    }
}
