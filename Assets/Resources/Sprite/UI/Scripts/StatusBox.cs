using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public abstract class StatusBox : MonoBehaviour {
    float mana;
    float health = 1;
    public Image orange;
    public Image violate;
    public Image blue;
    public Image green;
    public Image[] reds;

    // public Image health_o;
    //public Image health_i;
    public float heath;
    public CDBar cdBar;
    public virtual void ShowHealth(float h)
    {
        health = h;
        green.fillAmount = h / 0.25f;
        blue.fillAmount = (h - 0.25f) / 0.25f;
        violate.fillAmount = (h - 0.5f) / 0.25f;
        orange.fillAmount = (h - 0.75f) / 0.25f;
        SetRedBars(h);

    }
    private void Update()
    {
        ShowHealth(heath);
    }
    void SetRedBars(float h)
    {
        StartCoroutine(SetRedBar(reds[3], (h - 0.75f) / 0.25f));
        StartCoroutine(SetRedBar(reds[2], (h - 0.5f) / 0.25f));
        StartCoroutine(SetRedBar(reds[1], (h - 0.25f) / 0.25f));
        StartCoroutine(SetRedBar(reds[0], (h - 0) / 0.25f));
    }
    IEnumerator SetRedBar(Image redBar, float v, bool isFirst = true)
    {
        if (v > 1)
            v = 1;
        if (v < 0)
            v = 0;
        if (isFirst)
            yield return new WaitForSeconds(0.5f);
        if (redBar.fillAmount > v)
        {
            redBar.fillAmount -= 0.01f;
            yield return new WaitForEndOfFrame();
            StartCoroutine(SetRedBar(redBar, v, false));
        }
        else
        {
            redBar.fillAmount = v;
            yield return null;
        }
    }
    public virtual void ShowMana(int m)
    {

    }
    public virtual void ShowMana(float m)
    {

    }
}
