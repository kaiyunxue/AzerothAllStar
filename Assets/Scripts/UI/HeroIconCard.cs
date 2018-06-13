using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroIconCard : MonoBehaviour {
    public HeroIcon icon;

    public RawImage avatar;
    public Image background;
    public Text heroName;
    private void Awake()
    {
        avatar.texture = icon.avatar;
        background.sprite = icon.background;
        background.material = icon.material;
        heroName.text = icon.heroName;
    }
}
