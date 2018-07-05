using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : _SceneManager
{
    public UnityEvent myEvent;
    public Component currentUI;
    public MenuBackground background;
    public SecondLayerMenu singleUI;
    public SecondLayerMenu authorUI;
    public TextEditor editor = new TextEditor();
    public MenuBar bar;
    private void Awake()
    {
        currentUI = bar;
    }
    public void WhenSinglePlayerButtomClicked()
    {
        bar.HideUI();
        background.ZoomIn();
        singleUI.gameObject.SetActive(true);
        singleUI.In();
        currentUI = singleUI;
    }
    public void WhenAuthorButtomClicked()
    {
        bar.HideUI();
        background.ZoomIn();
        authorUI.gameObject.SetActive(true);
        authorUI.In();
        currentUI = authorUI;
    }
    public void WhenInstructionButtonClicked()
    {
        StartCoroutine(turn2Instruction());
    }
    IEnumerator turn2Instruction()
    {
        yield return SceneManager.LoadSceneAsync(ScenesName.Instruction, LoadSceneMode.Additive);
        bar.gameObject.SetActive(false);
        SceneManager.GetSceneByName(ScenesName.Instruction).GetRootGameObjects()[0].GetComponent<_SceneManager>().TurnFrom(m_Scene.MainMenu);
    }
    public void In()
    {
        background.ZoomOut();
        bar.ShowUI();
        currentUI = this;
    }
    public void CopyToClipboard(string link)
    {
        editor.text = link;
        editor.OnFocus();
        editor.Copy();
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Turn2ChosingHero()
    {
        StartCoroutine(loadChosingHero());

    }
    public IEnumerator loadChosingHero()
    {
        yield return SceneManager.LoadSceneAsync(ScenesName.ChoseHero, LoadSceneMode.Additive);
        SceneManager.GetSceneByName("ChoseHero").GetRootGameObjects()[0].GetComponent<ChoseHeroPage>().returnEvent = myEvent;
    }

    public override void TurnFrom(m_Scene scene)
    {
        switch(scene)
        {
            case m_Scene.Instructions:
                bar.gameObject.SetActive(true);
                break;
        }
    }
}
