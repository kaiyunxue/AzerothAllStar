using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class MaterialController : MonoBehaviour {
    private void Awake()
    {
        foreach(var thisRenderer in gameObject.transform.GetComponentsInChildren<Renderer>())
        {
            Material[] materials = thisRenderer.materials;
            int n = materials.Length;
            for(int i = 0; i < n; i++)
            {
                materials[i] = GameObject.Instantiate(materials[i]);
            }
        }
        foreach (var thisRenderer in gameObject.transform.GetComponentsInChildren<Renderer>())
        {
            Material[] materials = thisRenderer.materials;
            int n = materials.Length;
            for (int i = 0; i < n; i++)
            {
                materials[i] = GameObject.Instantiate(materials[i]);
            }
        }
    }
}
