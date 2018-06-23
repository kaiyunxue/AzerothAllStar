using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AttentionPAge : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(waitAndTurn());
	}
    IEnumerator waitAndTurn()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(1);
    }

}
