using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstroctionPage : MonoBehaviour {
    public GameObject herosPage;
    public GameObject heroInstruction;
    public GameObject heroButton;
    public GameObject heroSlash;
    public HeroPage heroPage;
    public void Escape()
    {
        SceneManager.UnloadSceneAsync(5);
    }
    public void Turn2ChooseHero()
    {
        herosPage.SetActive(true);
        heroInstruction.SetActive(false);
        heroButton.SetActive(false);
        heroSlash.SetActive(false);
    }
    public void ChooseHero(HeroPacket hero)
    {
        heroPage.SetHero(hero);
        heroInstruction.SetActive(true);
        heroButton.SetActive(true);
        heroSlash.SetActive(true);
        herosPage.SetActive(false);
    }
}
