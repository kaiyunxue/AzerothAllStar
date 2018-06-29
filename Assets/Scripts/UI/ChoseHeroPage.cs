using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.UI;

public class ChoseHeroPage : MonoBehaviour {
    public UnityEvent returnEvent;
    public RawImage leftHero;
    public Button playBtn;
    public GameObject loadingPage;
    public void ReturnBack()
    {
        returnEvent.Invoke();
        SceneManager.UnloadSceneAsync(4);
    }
    public void ChoseFirstHero(HeroIcon icon)
    {
        leftHero.texture = icon.sideAvatar;
        playBtn.interactable = true;
        playBtn.GetComponentInChildren<Text>().text = "Fight!";
    }
    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            if(leftHero.texture.name == "UIMask")
            {
                SceneManager.UnloadSceneAsync(4);
                returnEvent.Invoke();
            }
        }
    }
    public void Turn2FightingScene()
    {
        StartCoroutine(turn2FightScene());
    }
    IEnumerator turn2FightScene()
    {
        loadingPage.SetActive(true);
        yield return new WaitForSeconds(3);
        yield return SceneManager.LoadSceneAsync(2);
    }
    private void Awake()
    {
        Debug.Log(SceneManager.GetSceneByName("ChoseHero").GetRootGameObjects()[0].name);
    }
}
