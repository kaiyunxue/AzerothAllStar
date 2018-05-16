using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public abstract class StatusBox : MonoBehaviour {
    float mana;
    public Image health_o;
    public Image health_i;
    public float heath;
    public virtual void ShowHealth(float h)
    {
        if (h < health_o.fillAmount)
        {
            health_o.fillAmount = h;
            StartCoroutine(SetRedBar(h, true));
        }
        else
        {
            health_o.fillAmount = h;
            health_i.fillAmount = h;
        }
    }
    private void Update()
    {
        ShowHealth(heath);
    }
    IEnumerator SetRedBar(float h,bool isFirst)
    {
        if(isFirst)
        {
            yield return new WaitForSeconds(0.5f);
        }
        if (health_i.fillAmount >= h || health_o.fillAmount >= h)
        {
            Color c = health_i.color;
            c.a -= 0.01f;
            health_i.color = c;
            health_i.fillAmount -= 0.01f;
            yield return null;
            StartCoroutine(SetRedBar(h,false));
        }
        else
        {
            health_i.fillAmount = h;
            Color c = health_i.color;
            c.a = 1;
            health_i.color = c;
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
