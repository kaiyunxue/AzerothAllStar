using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
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
        SceneManager.LoadScene(4, LoadSceneMode.Additive);
    }
}
