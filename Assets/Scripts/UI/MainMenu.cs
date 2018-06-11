using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Component currentUI;
    public MenuBackground background;
    public SinglePlayerUI singleUI;
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
}
