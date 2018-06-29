using System;
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
        StartCoroutine(escape());
    }
    IEnumerator escape()
    {
        //SceneManager.GetSceneByName("firelandDemo").GetRootGameObjects()[0].GetComponent<FightingSceneManager>().WhenTurnedBackFromInstruction();
        Scene s = SceneManager.GetActiveScene();
        foreach (var go in s.GetRootGameObjects())
        {
            if(go.GetComponent<_SceneManager>() != null)
            {
                go.GetComponent<_SceneManager>().TurnFrom(m_Scene.Instructions);
                break;
            }
        }
        yield return new WaitForEndOfFrame();
        yield return SceneManager.UnloadSceneAsync(5);
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

    public override void TurnFrom(m_Scene scene)
    {
        switch(scene)
        {
            case m_Scene.FightScene:
                currentPage = Page.hero;
                herosPage.SetActive(false);
                heroInstruction.SetActive(true);
                heroButton.SetActive(true);
                heroSlash.SetActive(true);
                break;
                break;
            case m_Scene.MainMenu:
                currentPage = Page.heros;
                herosPage.SetActive(true);
                heroInstruction.SetActive(false);
                heroButton.SetActive(false);
                heroSlash.SetActive(false);
                break;

            case m_Scene.ChooseHero:

                break;
            default:
                Debug.Log("Turn from a Wrong Scene");
                break;
        }
    }
}
