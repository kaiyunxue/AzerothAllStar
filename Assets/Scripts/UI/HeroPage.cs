using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroPage : MonoBehaviour {
    public HeroPacket hero;
    public Transform heroPage;
    public RectTransform contentRect;
    private void Awake()
    {
        int n = hero.skillInstructions.Length;
        Vector2 tmp = contentRect.offsetMin;
        tmp.y = 30 - n * 310;
        contentRect.offsetMin = tmp;
        heroPage.SetParent(contentRect);
    }
}
