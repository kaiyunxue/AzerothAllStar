using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

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
        SceneManager.LoadScene(5,LoadSceneMode.Additive);
        canvas.SetActive(false);
        this.enabled = false;
    }
    public void WhenTurnedBackFromInstruction()
    {
        canvas.SetActive(true);
        this.enabled = true;
    }
}
