using UnityEngine;
using System.Collections;

public class RFX4_StartDelay : MonoBehaviour {

    public GameObject ActivatedGameObject;
    public float Delay = 1;

	// Use this for initialization
	void OnEnable () {
        ActivatedGameObject.SetActive(false);
        Invoke("ActivateGO", Delay);
	}
	
	// Update is called once per frame
	void ActivateGO () {
        ActivatedGameObject.SetActive(true);
	}

    void OnDisable()
    {
        CancelInvoke("ActivateGO");
    }
}
