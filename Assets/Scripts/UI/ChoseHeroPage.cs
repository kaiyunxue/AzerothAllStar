using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoseHeroPage : MonoBehaviour {
    public void ReturnBack()
    {
        SceneManager.UnloadSceneAsync(4);
    }
}
