    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroPage : MonoBehaviour {
    public GameObject mainMenu;
    public HeroPacket hero;
    public Transform heroPage;
    public RectTransform contentRect;
    public HeroInstructor heroIstr;
    Vector3 oriTransform;
    public void SetHero(HeroPacket hero)
    {
        this.hero = hero;
    }
    private void OnEnable()
    {
        int n = hero.skillInstructions.Length;
        Vector2 tmp = contentRect.offsetMin;
        tmp.y = 30 - n * 310;
        contentRect.offsetMin = tmp;
        heroIstr.SetHero(hero);
        heroPage.SetParent(contentRect);
        oriTransform = GetComponent<ScrollRect>().content.position;
    }
    public void ReturnMenu()
    {
        mainMenu.SetActive(true);
        gameObject.SetActive(false);
    }
    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            ReturnMenu();
        }
    }
    private void OnDisable()
    {
        GetComponent<ScrollRect>().content.position = oriTransform;
        heroPage.SetParent(GetComponent<ScrollRect>().viewport);
    }
}
