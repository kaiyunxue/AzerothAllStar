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
        Debug.Log(SceneManager.GetSceneByName("firelandDemo").GetRootGameObjects()[0].name);
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
            if(go.GetComponent<FightingSceneManager>() != null)
            {
                go.GetComponent<FightingSceneManager>().WhenTurnedBackFromInstruction();
                break;
            }
        }
        Time.timeScale = 1;
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
}
