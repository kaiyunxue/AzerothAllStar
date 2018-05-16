using UnityEngine;
using System.Collections;

public class RFX4_ReplaceMaterialByTime : MonoBehaviour
{
    public Material ReplacementMaterial;
    public float TimeDelay = 1;
    public bool ChangeShadow = true;

    private bool isInitialized;
    private Material mat;
    MeshRenderer mshRend;


    void Start ()
	{
	    isInitialized = true;
        mshRend = GetComponent<MeshRenderer>();
        mat = mshRend.sharedMaterial;
        Invoke("ReplaceObject", TimeDelay);
	}

    void OnEnable()
    {
        if (isInitialized) {
            mshRend.sharedMaterial = mat;
            Invoke("ReplaceObject", TimeDelay);
        }
    }

    void ReplaceObject()
    {
        mshRend.sharedMaterial = ReplacementMaterial;
	}
}
