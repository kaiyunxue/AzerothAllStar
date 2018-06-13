using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecondLayerMenu : MonoBehaviour {
    public AnimationCurve curve;
    public MainMenu mainMenu;
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
    public void ReturnMain()
    {
        Out();
        mainMenu.In();
    }
    IEnumerator zoomIn()
    {
        if (t >= 1)
        {
            t = 1;
            foreach (var b in GetComponentsInChildren<Button>())
            {
                b.interactable = true;
            }
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
            gameObject.SetActive(false);
            yield return null;
        }
        else
        {
            foreach(var b in GetComponentsInChildren<Button>())
            {
                b.interactable = false;
            }
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
            currentCoroutine = StartCoroutine(zoomOut());
        }
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            ReturnMain();
        }
    }
}
