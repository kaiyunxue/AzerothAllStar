using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System;

public class FightingSceneManager : _SceneManager
{
    public bool isOpen = false;
    public GameObject canvas;

    // Update is called once per frame
    private void Start()
    {
        canvas.SetActive(isOpen);
        if (isOpen)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
    void Update () {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            isOpen = !isOpen;
            canvas.SetActive(isOpen);
            if(isOpen)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
		
	}
    public void Turn2Main()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }
    public void Leave()
    {
        Application.Quit();
    }
    public void Turn2Heros()
    {
        SceneManager.LoadScene(4);
        Time.timeScale = 1;
    }
    public void Turn2Instruction()
    {
        StartCoroutine(turn2Instruction());
        canvas.SetActive(false);
        this.enabled = false;
    }
    IEnumerator turn2Instruction()
    {
        yield return SceneManager.LoadSceneAsync(ScenesName.Instruction, LoadSceneMode.Additive);
        SceneManager.GetSceneByName(ScenesName.Instruction).GetRootGameObjects()[0].GetComponent<_SceneManager>().TurnFrom(m_Scene.FightScene);
    }
    //public void WhenTurnedBackFromInstruction()
    //{
    //    Time.timeScale = 1;
    //    canvas.SetActive(true);
    //    this.enabled = true;
    //}

    public override void TurnFrom(m_Scene scene)
    {
        switch(scene)
        {
            case m_Scene.Instructions:
                canvas.SetActive(true);
                this.enabled = true;
                break;
            case m_Scene.ChooseHero:
                break;
            case m_Scene.MainMenu:
                break;
            default:
                Debug.LogError("Turn back from a wrong Scene.");
                break;

        }
    }
}
