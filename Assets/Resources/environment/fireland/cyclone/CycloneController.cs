using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycloneController : MonoBehaviour {
    // Use this for initialization
    Material[] materials;
	void Start () {
        materials = GetComponentInChildren<Renderer>().materials;
	}

    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.Rotate(Vector3.up, 8);
        Vector2 v = materials[0].mainTextureOffset;
        materials[0].mainTextureOffset += new Vector2(-0.01f, 0.005f);
        materials[0].mainTextureOffset += new Vector2(0f, -0.005f);
        materials[0].mainTextureOffset += new Vector2(0f, -0.002f);
    }
}
