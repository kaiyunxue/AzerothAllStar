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
        if(h < health)
        {
            health = h;
            green.fillAmount = h / 0.25f;
            blue.fillAmount = (h - 0.25f) / 0.25f;
            violate.fillAmount = (h - 0.5f) / 0.25f;
            orange.fillAmount = (h - 0.75f) / 0.25f;
            StartCoroutine(SetRedBar(h));
        }
        else
        {
            health = h;
            green.fillAmount = h / 0.25f;
            blue.fillAmount = (h - 0.25f) / 0.25f;
            violate.fillAmount = (h - 0.5f) / 0.25f;
            orange.fillAmount = (h - 0.75f) / 0.25f;
        }

    }
    private void Update()
    {
        ShowHealth(heath);
    }
    IEnumerator SetRedBar(float h,  bool isFirst = true)
    {
        yield return null;
    }
    public virtual void ShowMana(int m)
    {

    }
    public virtual void ShowMana(float m)
    {

    }
}
