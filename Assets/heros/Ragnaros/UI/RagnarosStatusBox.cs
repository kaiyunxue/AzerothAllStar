using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RagnarosStatusBox : StatusBox {
    public GameObject manaBar;
    public float Mana;
    Image[] manas;
    // Use this for initialization
    public override void ShowMana(float m)
    {
        m = Mathf.Floor(m);
        if (m > Mana)
        {
            for (int i = (int)Mana; i < m; i++)
            {
                manas[i].transform.GetChild(0).gameObject.SetActive(true);
                manas[i].transform.GetChild(1).gameObject.SetActive(true);
                manas[i].transform.GetChild(2).gameObject.SetActive(true);
                Mana = m;
            }
        }
        else if (m < Mana)
        {
            for (int i = (int)Mana - 1; i > m - 1; i--)
            {
                manas[i].transform.GetChild(0).gameObject.SetActive(false);
                manas[i].transform.GetChild(1).gameObject.SetActive(false);
                manas[i].transform.GetChild(2).gameObject.SetActive(false);
                Mana = m;
            }
        }
    }
    void Awake () {
        manas = manaBar.transform.GetComponentsInChildren<Image>();
    }
	
	// Update is called once per frame
	void Update () {
    }
    IEnumerator ShowUp(Image im)
    {
        Color c = im.color;
        c.a += 0.1f;
        im.color = c;
        ShowHealth(heath);
        if(c.a < 1)
        {
            yield return null;
            StartCoroutine(ShowUp(im));
        }
        else
        {
            c.a = 1;
            im.color = c;
            yield return null;
        }
    }
    IEnumerator Disappear(Image im)
    {
        Color c = im.color;
        c.a -= 0.1f;
        im.color = c;
        if (c.a > 0)
        {
            yield return null;
            StartCoroutine(Disappear(im));
        }
        else
        {
            c.a = 0;
            im.color = c;
            yield return null;
        }
    }

}
