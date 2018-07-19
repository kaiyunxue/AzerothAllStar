using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneMaterial : MonoBehaviour {
    private void Awake()
    {
        Material m = gameObject.GetComponent<Projector>().material;
        Material mInstance = Instantiate(m);
        gameObject.GetComponent<Projector>().material = mInstance;
    }
}
