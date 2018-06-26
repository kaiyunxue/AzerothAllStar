using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstroctionPage : _SceneManager
{
    enum Page
    {
        heros,
        hero,
        map
    }

    Page currentPage;
    public GameObject herosPage;
    public GameObject heroInstruction;
    public GameObject heroButton;
    public GameObject heroSlash;
    public HeroPage heroPage;
    private void Awake()
    {
        currentPage = Page.hero;
    }
    public void Escape()
    {
        SceneManager.UnloadSceneAsync(5);
        SceneManager.GetSceneAt(0).GetRootGameObjects()[0].GetComponent<FightingSceneManager>().WhenTurnedBackFromInstruction();
    }
    public void Turn2ChooseHero()
    {
        herosPage.SetActive(true);
        heroInstruction.SetActive(false);
        heroButton.SetActive(false);
        heroSlash.SetActive(false);
        currentPage = Page.heros;
    }
    public void ChooseHero(HeroPacket hero)
    {
        heroPage.SetHero(hero);
        heroInstruction.SetActive(true);
        heroButton.SetActive(true);
        heroSlash.SetActive(true);
        herosPage.SetActive(false);
        currentPage = Page.hero;
    }
    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            switch(currentPage)
            {
                case Page.heros:
                    Escape();
                    break;
                case Page.hero:
                    Turn2ChooseHero();
                    break;
            }
        }
    }
}
