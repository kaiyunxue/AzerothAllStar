using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialController : MonoBehaviour
{
    public bool isChangeColor = false;
    public Color color;
    private void Awake()
    {
        foreach (var thisRenderer in gameObject.transform.GetComponentsInChildren<Renderer>())
        {
            Material[] materials = thisRenderer.materials;
            int n = materials.Length;
            for (int i = 0; i < n; i++)
            {
                if (isChangeColor)
                {
                    materials[i].SetColor("Color", color);
                    materials[i].SetColor("_MainColor", color);
                    materials[i].SetColor("_TintColor", color);
                    materials[i].SetColor("_Color", color);
                    materials[i].SetColor("_RimColor", color);
                }
                materials[i] = GameObject.Instantiate(materials[i]);
            }
        }
    }
    public void SetColor(Color color)
    {
        this.color = color;
        foreach (var thisRenderer in gameObject.transform.GetComponentsInChildren<Renderer>())
        {
            Material[] materials = thisRenderer.materials;
            int n = materials.Length;
            for (int i = 0; i < n; i++)
            {
                if (isChangeColor)
                {
                    materials[i].SetColor("Color", color);
                    materials[i].SetColor("_MainColor", color);
                    materials[i].SetColor("_TintColor", color);
                    materials[i].SetColor("_Color", color);
                    materials[i].SetColor("_RimColor", color);
                }
            }
        }
    }
}
