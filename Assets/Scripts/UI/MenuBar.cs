using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuBar : MonoBehaviour
{
    Graphic[] graphics;
    float a;
    Coroutine currentCoroutine;
    private void Awake()
    {
        graphics = GetComponentsInChildren<Graphic>();
    }
    public void ShowUI()
    {
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(showUI());
    }
    public void HideUI()
    {
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(hideUI());
    }
    IEnumerator showUI()
    {
        if(a <= 1)
        {
            a = 1;
            currentCoroutine = null;
            yield return null;
        }
        else
        {
            a += 0.01f;
            foreach (var v in graphics)
            {
                Color c = v.color;
                c.a = a;
                v.color = c;
            }
            yield return new WaitForEndOfFrame();
            currentCoroutine = StartCoroutine(showUI());
        }
    }
    IEnumerator hideUI()
    {
        if (a >= 0)
        {
            a = 0;
            currentCoroutine = null;
            yield return null;
        }
        else
        {
            a += 0.01f;
            foreach (var v in graphics)
            {
                Color c = v.color;
                c.a = a;
                v.color = c;
            }
            yield return new WaitForEndOfFrame();
            currentCoroutine = StartCoroutine(hideUI());
        }
    }
}
