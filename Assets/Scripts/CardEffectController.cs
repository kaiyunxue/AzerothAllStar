using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardEffectController : MonoBehaviour {
    public Material material;

    [Header("Red")]
    public float distortParamR;
    public float colorParamR;
    public Color motionColorR;
    public Vector3 moveParamR;
    public Vector3 rotatePAramR;

    [Header("Green")]
    public float distortParamG;
    public float colorParamG;
    public Color motionColorG;
    public Vector3 moveParamG;
    public Vector3 rotatePAramG;

    [Header("Blue")]
    public float distortParamB;
    public float colorParamB;
    public Color motionColorB;
    public Vector3 moveParamB;
    public Vector3 rotatePAramB;

    [Header("Alpha")]
    public float distortParamA;
    public float colorParamA;
    public Color motionColorA;
    public Vector3 moveParamA;
    public Vector3 rotatePAramA;
    private void Awake()
    {
        material = GetComponent<Graphic>().material;
    }
}
