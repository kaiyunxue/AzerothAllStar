using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class MaterialController : MonoBehaviour {
    Material[] material;
    private void Awake()
    {
        var renderer = GetComponent<Renderer>();
        material = renderer.materials;
    }
}
