using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardEffectController : MonoBehaviour {
    public Material material;
    /*
        _MotionTex1("Motion Texture 1", 2D) = "black" {}

        _MotionColor1("Motion Color 1", Color) = (1,1,1,0)

        _ColorParam1("Color change", vector) = (1,0,1,1)

        _DistortParam1("Distort1 power", float) = 0.015

        _MoveParam1("Move dir", vector) = (0,0,0,0)

        _RotateParam1("Rotate Center&Speed", vector)=(0,0,0,0)
        */

    [Header("Red")]
    public float distortParamR;
    public Vector4 colorParamR;
    public Vector4 moveParamR;
    public Vector4 rotatePAramR;

    [Header("Green")]
    public float distortParamG;
    public Vector4 colorParamG;
    public Vector4 moveParamG;
    public Vector4 rotatePAramG;

    [Header("Blue")]
    public float distortParamB;
    public Vector4 colorParamB;
    public Vector3 moveParamB;
    public Vector3 rotatePAramB;

    [Header("Alpha")]
    public float distortParamA;
    public Vector4 colorParamA;
    public Vector4 moveParamA;
    public Vector4 rotatePAramA;

    private void OnEnable()
    {

        GetComponent<Graphic>().material = Instantiate(material);
        colorParamR = material.GetVector("_ColorParam1");
        moveParamR = material.GetVector("_MoveParam1");
        rotatePAramR = material.GetVector("_RotateParam1");

        colorParamG = material.GetVector("_ColorParam2");
        moveParamG = material.GetVector("_MoveParam2");
        rotatePAramG = material.GetVector("_RotateParam2");

        colorParamB = material.GetVector("_ColorParam3");
        moveParamB = material.GetVector("_MoveParam3");
        rotatePAramB = material.GetVector("_RotateParam3");

        colorParamA = material.GetVector("_ColorParam4");
        moveParamA = material.GetVector("_MoveParam4");
        rotatePAramA = material.GetVector("_RotateParam4");
    }
    void Update()
    {
        material.SetVector("_ColorParam1", colorParamR);
        material.SetVector("_MoveParam1", moveParamR);
        material.SetVector(" _RotateParam1", rotatePAramR);

        material.SetVector("_ColorParam2", colorParamG);
        material.SetVector("_MoveParam2", moveParamG);
        material.SetVector("_RotateParam2", rotatePAramG);

        material.SetVector(" _ColorParam3", colorParamB);
        material.SetVector("_MoveParam3", moveParamB);
        material.SetVector(" _RotateParam3", rotatePAramB);

        material.SetVector("_ColorParam4", colorParamA);
        material.SetVector("_MoveParam4", moveParamA);
        material.SetVector("_RotateParam4", rotatePAramA);
    }
}
