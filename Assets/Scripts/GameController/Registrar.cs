using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Registrar : MonoBehaviour {
    [SerializeField]
    private Hero leftHero;
    [SerializeField]
    private Hero rightHero;

    public Hero FindHeroByLayer(int layer)
    {
        if (layer == leftHero.gameObject.layer)
            return leftHero;
        if (layer == rightHero.gameObject.layer)
            return rightHero;
        throw new System.Exception("Cannot find hero!!!.");
    }
    public Hero LeftHero
    {
        get
        {
            return this.leftHero;
        }
        set
        {
            leftHero = value;
        }
    }

    public Hero RightHero
    {
        get
        {
            return this.rightHero;
        }
        set
        {
            rightHero = value;
        }
    }
}
